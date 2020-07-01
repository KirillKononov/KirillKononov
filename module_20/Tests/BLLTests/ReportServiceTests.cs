using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces.ServicesInterfaces;
using BLL.Services.Report;
using DAL.Entities;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace Tests.BLLTests
{
    public class ReportServiceTests
    {
        private IEnumerable<StudentDTO> FindStudents()
        {
            var students = new List<StudentDTO>()
            {
                new StudentDTO
                {
                    Id = 1,
                    FirstName = "Kirill",
                    LastName = "Kononov",
                    AverageMark = (float) 4.67,
                    MissedLectures = 0,
                    StudentHomework = new List<HomeworkDTO>()
                    {
                        new HomeworkDTO()
                        {
                            Id = 1,
                            StudentId = 1,
                            LectureId = 1,
                            StudentPresence = true,
                            HomeworkPresence = true,
                            Mark = 4,
                            Date = DateTime.Now
                        }
                    }
                }
            };
            return students;
        }
        
        private async Task<StudentDTO> GetAsyncStudent()
        {
            var student = new StudentDTO
                {
                    Id = 1,
                    FirstName = "Kirill",
                    LastName = "Kononov",
                    AverageMark = (float) 4.67,
                    MissedLectures = 0,
                    StudentHomework = null
                };
            return student;
        }
        
        private IEnumerable<StudentDTO> FindStudentsValidationExceptionTest()
        {
            var students = new List<StudentDTO>();
            return students;
        }
        
        private IEnumerable<LectureDTO> FindLectures()
        {
            var lectures = new List<LectureDTO>()
            {
                new LectureDTO
                {
                    Id = 1,
                    Name = "Math",
                    ProfessorId = 1,
                    LectureHomework = new List<HomeworkDTO>()
                    {
                        new HomeworkDTO()
                        {
                            Id = 1,
                            StudentId = 1,
                            LectureId = 1,
                            StudentPresence = true,
                            HomeworkPresence = true,
                            Mark = 4,
                            Date = DateTime.Now
                        }
                    }
                }
            };
            return lectures;
        }
        
        private async Task<LectureDTO> GetAsyncLecture()
        {
            var lecture = new LectureDTO
            {
                Id = 1,
                Name = "Math",
                ProfessorId = 1,
                LectureHomework = null
            };
            return lecture;
        }
        
        private IEnumerable<LectureDTO> FindLecturesValidationExceptionTest()
        {
            var lectures = new List<LectureDTO>();
            return lectures;
        }
        
        private async Task<ProfessorDTO> GetAsyncProfessor()
        {
            var professor = new ProfessorDTO
            {
                Id = 1,
                FirstName = "Kirill",
                LastName = "Kononov",
                Lectures = null
            };
            return professor;
        }
        
        private ReportService ReportService { get; set; }
        private Mock<IStudentService> StudentServiceMock { get; set; }
        private Mock<ILectureService> LectureServiceMock { get; set; }
        private Mock<IProfessorService> ProfessorServiceMock { get; set; }

        [SetUp]
        public void SetUp()
        {
            StudentServiceMock = new Mock<IStudentService>();
            LectureServiceMock = new Mock<ILectureService>();
            ProfessorServiceMock = new Mock<IProfessorService>();
            StudentServiceMock.Setup(service => service.Find(It.IsAny<Func<Student, bool>>()))
                .Returns(FindStudents());
            StudentServiceMock.Setup(service => service.GetAsync(It.IsAny<int>()))
                .Returns(GetAsyncStudent());
            LectureServiceMock.Setup(service => service.Find(It.IsAny<Func<Lecture, bool>>()))
                .Returns(FindLectures());
            LectureServiceMock.Setup(service => service.GetAsync(It.IsAny<int>()))
                .Returns(GetAsyncLecture());
            ProfessorServiceMock.Setup(service => service.GetAsync(It.IsAny<int>()))
                .Returns(GetAsyncProfessor());

            ReportService = new ReportService(StudentServiceMock.Object, 
                LectureServiceMock.Object,
                ProfessorServiceMock.Object,
                new NullLoggerFactory());
        }

        [Test]
        public void MakeStudentReport_ValidCall()
        {
            ReportService.MakeStudentReport("Kirill", "Kononov");
            
            StudentServiceMock.Verify(s => s.Find(It.IsAny<Func<Student, bool>>()));
            LectureServiceMock.Verify(l => l.GetAsync(It.IsAny<int>()));
            ProfessorServiceMock.Verify(p => p.GetAsync(It.IsAny<int>()));
        }
        
        [Test]
        public void MakeStudentReport_ThrowsValidationException()
        {
            StudentServiceMock.Setup(service => service.Find(It.IsAny<Func<Student, bool>>()))
                .Returns(FindStudentsValidationExceptionTest());

            Assert.Throws<ValidationException>(() => ReportService.MakeStudentReport("Kirill", "Kononov"));
        }
        
        [Test]
        public void MakeLectureReport_ValidCall()
        {
            ReportService.MakeLectureReport("Math");
            
            LectureServiceMock.Verify(s => s.Find(It.IsAny<Func<Lecture, bool>>()));
            StudentServiceMock.Verify(l => l.GetAsync(It.IsAny<int>()));
            ProfessorServiceMock.Verify(p => p.GetAsync(It.IsAny<int>()));
        }
        
        [Test]
        public void MakeLectureReport_ThrowsValidationException()
        {
            LectureServiceMock.Setup(service => service.Find(It.IsAny<Func<Lecture, bool>>()))
                .Returns(FindLecturesValidationExceptionTest());

            Assert.Throws<ValidationException>(() => ReportService.MakeLectureReport("Math"));
        }
    }
}