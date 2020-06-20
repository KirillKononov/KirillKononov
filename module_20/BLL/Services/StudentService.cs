using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Interfaces.ServicesInterfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public StudentService(IRepository<Student> repository, IMapperBLL mapper, ILoggerFactory factory)
        {
            _studentRepository = repository;
            _logger = factory.CreateLogger("Student Service");
            _mapper = mapper.CreateMapper();
        }

        public async Task<IEnumerable<StudentDTO>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            if (!students.Any())
            {
                _logger.LogWarning("There is no students in data base");
                throw new ValidationException("There is no students in data base");
            }

            return students
                .Select(s => _mapper.Map<StudentDTO>(s));
        }

        public async Task<StudentDTO> GetAsync(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var student = await _studentRepository.GetAsync(id);

            validator.EntityValidation(student, _logger, nameof(student));

            return _mapper.Map<StudentDTO>(student);
        }

        public async Task CreateAsync(StudentDTO item)
        {
            var student = _mapper.Map<Student>(item);
            await _studentRepository.CreateAsync(student);
        }

        public async Task UpdateAsync(StudentDTO item)
        {
            var student = await _studentRepository.GetAsync(item.Id);

            var validator = new Validator();
            validator.EntityValidation(student, _logger, nameof(student));

            student.FirstName = item.FirstName;
            student.LastName = item.LastName;
            _studentRepository.Update(student);
        }

        public IEnumerable<StudentDTO> Find(Func<Student, bool> predicate)
        {
            var students = _studentRepository
                .Find(predicate)
                .ToList();
            return students
                .Select(s => _mapper.Map<StudentDTO>(s));
        }

        public async Task DeleteAsync(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var student = await _studentRepository.GetAsync(id);

            validator.EntityValidation(student, _logger, nameof(student));
            
            _studentRepository.Delete(student);
        }
    }
}
