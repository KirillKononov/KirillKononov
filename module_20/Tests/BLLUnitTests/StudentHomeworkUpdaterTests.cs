using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Services.StudentHomeworkUpdater;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace Tests.BLLUnitTests
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
        
        private StudentHomeworkUpdater StudentHomeworkUpdater { get; set; }
        
        private Mock<IRepository<Student>> StudentRepoMock { get; set; }
        
        private Mock<Func<string, IMessageSender>> MessageServiceAccessorMock { get; set; }
        
        private Mock<IMessageSender> MessageSender { get; set; }

        [SetUp]
        public void SetUp()
        {
            MessageServiceAccessorMock = new Mock<Func<string, IMessageSender>>();
            MessageSender = new Mock<IMessageSender>();
            MessageServiceAccessorMock.Setup(_ => _.Invoke(It.IsAny<string>()))
                .Returns(MessageSender.Object);
            
            StudentRepoMock = new Mock<IRepository<Student>>();
            StudentRepoMock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetStudentWithSendingMessages());
            
            StudentHomeworkUpdater = new StudentHomeworkUpdater(StudentRepoMock.Object, 
                MessageServiceAccessorMock.Object, 
                new NullLoggerFactory());
        }

        [Test]
        public async Task UpdateAsync_ThreeMessagesWillBeSent_ValidCall()
        {
            await StudentHomeworkUpdater.UpdateAsync(_homework, StudentHomeworkUpdater.UpdateType.AddHomework);
            
            StudentRepoMock.Verify(m => m.GetAsync(It.IsAny<int>()));
            StudentRepoMock.Verify(m => m.Update(It.IsAny<Student>()));
            MessageServiceAccessorMock.Verify(m => m.Invoke(It.IsAny<string>())
                .Send(It.IsAny<Student>(), new NullLoggerFactory().CreateLogger("")), Times.Exactly(3));
        }
        
        [Test]
        public async Task UpdateAsync_NoMessagesWillBeSent_ValidCall()
        {
            StudentRepoMock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetStudentWithoutSendingMessages());
            await StudentHomeworkUpdater.UpdateAsync(_homework, StudentHomeworkUpdater.UpdateType.AddHomework);
            
            StudentRepoMock.Verify(m => m.GetAsync(It.IsAny<int>()));
            StudentRepoMock.Verify(m => m.Update(It.IsAny<Student>()));
            MessageServiceAccessorMock.Verify(m => m.Invoke(It.IsAny<string>())
                .Send(It.IsAny<Student>(), new NullLoggerFactory().CreateLogger("")), Times.Exactly(0));
        }
        
        [Test]
        public void UpdateAsync_ThrowsValidationException()
        {
            StudentRepoMock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());
            
            Assert.ThrowsAsync<ValidationException>(async () => await StudentHomeworkUpdater
                .UpdateAsync(_homework, StudentHomeworkUpdater.UpdateType.AddHomework));
        }
        
        private static async Task<Student> GetStudentWithSendingMessages()
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
        
        private static async Task<Student> GetStudentWithoutSendingMessages()
        {
            var student = new Student
            {
                Id = 1,
                FirstName = "Kirill",
                LastName = "Kononov",
                AverageMark = 5,
                MissedLectures = 0,
                StudentHomework = new List<Homework>()
                {
                    new Homework()
                    {
                        Id = 1,
                        StudentId = 1,
                        LectureId = 1,
                        StudentPresence = true,
                        HomeworkPresence = true,
                        Mark = 5,
                        Date = new DateTime(2020,01,24)
                    },
                    new Homework()
                    {
                        Id = 2,
                        StudentId = 1,
                        LectureId = 1,
                        StudentPresence = true,
                        HomeworkPresence = true,
                        Mark = 5,
                        Date = new DateTime(2020,02,24)
                    },
                    new Homework()
                    {
                        Id = 3,
                        StudentId = 1,
                        LectureId = 1,
                        StudentPresence = true,
                        HomeworkPresence = true,
                        Mark = 5,
                        Date = new DateTime(2020,03,24)
                    }
                }
            };
            return student;
        }
        
        private static async Task<Student> GetExceptionTest()
        {
            return null;
        }
    }
}