using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Mapper;
using BLL.Services;
using BLL.Services.StudentHomeworkUpdater;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace Tests.BLLTests
{
    [TestFixture]
    public class StudentHomeworkUpdaterTests
    {
        private readonly Homework _homework = new Homework
        {
            Id = 1,
            StudentId = 1,
            LectureId = 1,
            StudentPresence = true,
            HomeworkPresence = true,
            Mark = 5,
            Date = new DateTime(2020,01,24)
        };
        
        private async Task<Student> GetStudent()
        {
            var student = new Student
            {
                Id = 1,
                FirstName = "Kirill",
                LastName = "Kononov",
                AverageMark = 0,
                MissedLectures = 4,
                StudentHomework = new List<Homework>()
                {
                    new Homework()
                    {
                        Id = 1,
                        StudentId = 1,
                        LectureId = 1,
                        StudentPresence = false,
                        HomeworkPresence = false,
                        Mark = 0,
                        Date = new DateTime(2020,01,24)
                    }
                }
            };
            return student;
        }
        
        private StudentHomeworkUpdater StudentHomeworkUpdater { get; set; }
        private Mock<IRepository<Student>> StudentRepoMock { get; set; }
        private Mock<IMessageSender> MessageSenderMock { get; set; }

        [SetUp]
        public void SetUp()
        {
            MessageSenderMock = new Mock<IMessageSender>();
            StudentRepoMock = new Mock<IRepository<Student>>();
            StudentRepoMock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetStudent());

            StudentHomeworkUpdater = new StudentHomeworkUpdater(StudentRepoMock.Object, m => MessageSenderMock.Object, 
                new NullLoggerFactory());
        }

        [Test]
        public void UpdateAsync_ValidCall()
        {
            StudentHomeworkUpdater.UpdateAsync(_homework, StudentHomeworkUpdater.UpdateType.AddHomework);
            
            StudentRepoMock.Verify(m => m.GetAsync(It.IsAny<int>()));
            StudentRepoMock.Verify(m => m.Update(It.IsAny<Student>()));
            MessageSenderMock.Verify(m => m.Send(It.IsAny<Student>(), null));
        }
    }
}