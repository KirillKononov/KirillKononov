using System;
using System.Collections.Generic;
using System.Linq;
using BLL.BusinessLogic.Serializers;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.BusinessLogic.Report
{
    public class ReportService : IReportServicer
    {
        private readonly IUnitOfWork _db;
        private readonly ILogger _logger;

        public ReportService(IUnitOfWork uow, ILogger<ReportService> logger = null)
        {
            _db = uow;
            _logger = logger;
        }

        public string MakeStudentReport(string firstName, string lastName, 
            Func<IEnumerable<Attendance>, string> serializer = null)
        {
            var students = _db.Students.Find(s =>
                s.FirstName == firstName && s.LastName == lastName).ToList();

            if (students.Count == 0)
            {
                _logger.LogError($"Entered student {firstName} {lastName} doesn't exist");
                throw new ValidationException($"Entered student {firstName} {lastName} doesn't exist");
            }

            if (serializer == null)
            {
                var jsonSerializer = new JsonAttendanceSerializer();
                serializer = jsonSerializer.Serialize;
            }

            var attendance = from student in students
                from homework in student.StudentHomework
                select new Attendance()
                {
                    LectureName = _db.Lectures.Get(homework.LectureId).Name,
                    ProfessorName = $"{_db.Professors.Get(_db.Lectures.Get(homework.LectureId).ProfessorId).FirstName} " +
                                    $"{_db.Professors.Get(_db.Lectures.Get(homework.LectureId).ProfessorId).LastName}",
                    StudentName = $"{student.FirstName} {student.LastName}",
                    Presence = homework.Presence,
                    Mark = homework.Mark,
                    Date = homework.Date
                };
            return serializer(attendance);
        }

        public string MakeLectureReport(string lectureName, Func<IEnumerable<Attendance>, string> serializer = null)
        {
            var lectures = _db.Lectures.Find(l => l.Name == lectureName).ToList();

            if (lectures.Count == 0)
            {
                _logger.LogError($"Entered lecture {lectureName} doesn't exist");
                throw new ValidationException($"Entered lecture {lectureName} doesn't exist");
            }

            if (serializer == null)
            {
                var jsonSerializer = new JsonAttendanceSerializer();
                serializer = jsonSerializer.Serialize;
            }

            var attendance = from lecture in lectures
                from homework in lecture.LectureHomework
                select new Attendance()
                {
                    LectureName = lecture.Name,
                    ProfessorName = $"{_db.Professors.Get(lecture.ProfessorId).FirstName} " +
                                    $"{_db.Professors.Get(lecture.ProfessorId).LastName}",
                    StudentName = $"{_db.Students.Get(homework.StudentId).FirstName} " +
                                  $"{_db.Students.Get(homework.StudentId).LastName}",
                    Presence = homework.Presence,
                    Mark = homework.Mark,
                    Date = homework.Date
                };
            return serializer(attendance);
        }
    }
}
