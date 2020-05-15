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

        public StudentRepository(DataBaseContext context, ILogger logger)
        {
            _db = context;
            _logger = logger;
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
                .Select(CreateStudentDTO);
        }

        public StudentDTO Get(int? id)
        {
            Validator.IdValidation(id, _logger);

            var student = _db.Students.Find(id);

            Validator.EntityValidation(student, _logger, nameof(student));

            return CreateStudentDTO(student);
        }

        public void Create(StudentDTO item)
        {
            var student = new Student()
            {
                FirstName = item.FirstName,
                LastName = item.LastName,
                AverageMark = item.AverageMark,
                MissedLectures = item.MissedLectures
            };
            _db.Students.Add(student);
        }

        public void Update(StudentDTO item)
        {
            var student = _db.Students.Find(item.Id);

            student.FirstName = item.FirstName;
            student.LastName = item.LastName;
            student.AverageMark = item.AverageMark;
            student.MissedLectures = item.MissedLectures;

            _db.Entry(student).State = EntityState.Modified;
        }

        public IEnumerable<StudentDTO> Find(Func<Student, bool> predicate)
        {
            var students = _db.Students.Where(predicate).ToList();
            return students
                .Select(CreateStudentDTO);
        }

        public void Delete(int? id)
        {
            Validator.IdValidation(id, _logger);

            var student = _db.Students.Find(id);

            Validator.EntityValidation(student, _logger, nameof(student));
            
            _db.Students.Remove(student);
        }

        private static StudentDTO CreateStudentDTO(Student student)
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Homework, HomeworkDTO>()).CreateMapper();
            return new StudentDTO()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                AverageMark = student.AverageMark,
                MissedLectures = student.MissedLectures,
                StudentHomeWorks = mapper.Map<IEnumerable<Homework>, List<HomeworkDTO>>(student.StudentHomeWorks)
            };
        }
    }
}
