using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.DataAccess;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.Repositories
{
    class StudentRepository : IRepository<StudentDTO, Student>
    {
        private readonly DataBaseContext _db;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public StudentRepository(DataBaseContext context, IMapper mapper, ILoggerFactory factory)
        {
            _db = context;
            _logger = factory.CreateLogger("Student Repository");
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllAsync()
        {
            var students = await _db.Students.ToListAsync();

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

            var student = await _db.Students.FindAsync(id);

            validator.EntityValidation(student, _logger, nameof(student));

            return _mapper.Map<StudentDTO>(student);
        }

        public async Task CreateAsync(StudentDTO item)
        {
            var student = _mapper.Map<Student>(item);
            await _db.Students.AddAsync(student);
        }

        public async Task UpdateAsync(StudentDTO item)
        {
            var student = await _db.Students.FindAsync(item.Id);

            var validator = new Validator();
            validator.EntityValidation(student, _logger, nameof(student));

            student.FirstName = item.FirstName;
            student.LastName = item.LastName;
            _db.Entry(student).State = EntityState.Modified;
        }

        public IEnumerable<StudentDTO> Find(Func<Student, bool> predicate)
        {
            var students = _db.Students
                .Where(predicate)
                .ToList();
            return students
                .Select(s => _mapper.Map<StudentDTO>(s));
        }

        public async Task DeleteAsync(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var student = await _db.Students.FindAsync(id);

            validator.EntityValidation(student, _logger, nameof(student));
            
            _db.Students.Remove(student);
        }
    }
}
