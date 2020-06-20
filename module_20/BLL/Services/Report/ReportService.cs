using System;
using System.Collections.Generic;
using System.Linq;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces.ServicesInterfaces;
using BLL.Services.Report.Serializers;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly IStudentService _studentService;
        private readonly ILectureService _lectureService;
        private readonly IProfessorService _professorService;
        private readonly ILogger _logger;

        public ReportService(IStudentService studentService,
            ILectureService lectureService, IProfessorService professorService, ILogger<ReportService> logger = null)
        {
            _studentService = studentService;
            _lectureService = lectureService;
            _professorService = professorService;
            _logger = logger;
        }

        public string MakeStudentReport(string firstName, string lastName, 
            Func<IEnumerable<Attendance>, string> serializer = null)
        {
            var students = _studentService.Find(s =>
                s.FirstName == firstName && s.LastName == lastName).ToList();

            if (students.Count == 0)
            {
                _logger.LogWarning($"Entered student {firstName} {lastName} doesn't exist");
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
                    LectureName = _lectureService.GetAsync(homework.LectureId).Result.Name,
                    ProfessorName = $"{_professorService.GetAsync(_lectureService.GetAsync(homework.LectureId).Result.ProfessorId).Result.FirstName} " +
                                    $"{_professorService.GetAsync(_lectureService.GetAsync(homework.LectureId).Result.ProfessorId).Result.LastName}",
                    StudentName = $"{student.FirstName} {student.LastName}",
                    StudentPresence = homework.StudentPresence,
                    HomeworkPresence = homework.HomeworkPresence,
                    Mark = homework.Mark,
                    Date = homework.Date
                };
            return serializer(attendance);
        }

        public string MakeLectureReport(string lectureName, Func<IEnumerable<Attendance>, string> serializer = null)
        {
            var lectures = _lectureService.Find(l => l.Name == lectureName).ToList();

            if (lectures.Count == 0)
            {
                _logger.LogWarning($"Entered lecture {lectureName} doesn't exist");
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
                    ProfessorName = $"{_professorService.GetAsync(lecture.ProfessorId).Result.FirstName} " +
                                    $"{_professorService.GetAsync(lecture.ProfessorId).Result.LastName}",
                    StudentName = $"{_studentService.GetAsync(homework.StudentId).Result.FirstName} " +
                                  $"{_studentService.GetAsync(homework.StudentId).Result.LastName}",
                    StudentPresence = homework.StudentPresence, 
                    HomeworkPresence = homework.HomeworkPresence,
                    Mark = homework.Mark,
                    Date = homework.Date
                };
            return serializer(attendance);
        }
    }
}
