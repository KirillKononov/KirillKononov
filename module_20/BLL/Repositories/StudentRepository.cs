using System;
using System.Collections.Generic;
using System.Linq;
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

        public StudentRepository(DataBaseContext context, IMapper mapper, ILogger logger)
        {
            _db = context;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<StudentDTO> GetAll()
        {
            var students = _db.Students.ToList();
            if (!students.Any())
            {
                _logger.LogError("There is no students in data base");
                throw new ValidationException("There is no students in data base");
            }

            return students
                .Select(s => _mapper.Map<StudentDTO>(s));
        }

        public StudentDTO Get(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var student = _db.Students.Find(id);

            validator.EntityValidation(student, _logger, nameof(student));

            return _mapper.Map<StudentDTO>(student);
        }

        public void Create(StudentDTO item)
        {
            var student = new Student()
            {
                FirstName = item.FirstName,
                LastName = item.LastName,
            };

            _db.Students.Add(student);
        }

        public void Update(StudentDTO item)
        {
            var student = _db.Students.Find(item.Id);

            var validator = new Validator();
            validator.EntityValidation(student, _logger, nameof(student));

            student.FirstName = item.FirstName;
            student.LastName = item.LastName;

            _db.Entry(student).State = EntityState.Modified;
        }

        public IEnumerable<StudentDTO> Find(Func<Student, bool> predicate)
        {
            var students = _db.Students.Where(predicate).ToList();
            return students
                .Select(s => _mapper.Map<StudentDTO>(s));
        }

        public void Delete(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var student = _db.Students.Find(id);

            validator.EntityValidation(student, _logger, nameof(student));
            
            _db.Students.Remove(student);
        }
    }
}
