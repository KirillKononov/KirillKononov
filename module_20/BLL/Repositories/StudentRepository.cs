using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.DataAccess;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.Repositories
{
    class StudentRepository : IRepository<Student>
    {
        private readonly DataBaseContext _db;
        private readonly ILogger _logger;

        public StudentRepository(DataBaseContext context, ILogger logger)
        {
            _db = context;
            _logger = logger;
        }

        public IEnumerable<Student> GetAll()
        {
            if (!_db.Students.Any())
            {
                _logger.LogError("There is no students in data base");
                throw new ValidationException("There is no students in data base");
            }

            return _db.Students;
        }

        public Student Get(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Student's id hasn't entered");
                throw new ValidationException("Student's id hasn't entered");
            }

            if (_db.Students.Find(id) == null)
            {
                _logger.LogError("There is no student in data base with this id");
                throw new ValidationException("There is no student in data base with this id");
            }

            return _db.Students.Find(id);
        }

        public void Create(Student item)
        {
            _db.Students.Add(item);
        }

        public void Update(Student item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Student> Find(Func<Student, bool> predicate)
        {
            return _db.Students.Where(predicate).ToList();
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Student's id hasn't entered");
                throw new ValidationException("Student's id hasn't entered");
            }

            var student = _db.Students.Find(id);

            if (student != null)
                _db.Students.Remove(student);
        }
    }
}
