using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Infrastructure;
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
    public class HomeworkServiceTests
    {
        private readonly HomeworkDTO _homeworkDTO = new HomeworkDTO
        {
            Id = 1,
            StudentId = 1,
            LectureId = 1,
            StudentPresence = true,
            HomeworkPresence = true,
            Mark = 5,
            Date = new DateTime(2020,01,24)
        };
        
        private readonly HomeworkDTO _homeworkDTOForPresenceException = new HomeworkDTO
        {
            Id = 1,
            StudentId = 1,
            LectureId = 1,
            StudentPresence = true,
            HomeworkPresence = true,
            Mark = 6,
            Date = new DateTime(2020,01,24)
        };
        
        private readonly HomeworkDTO _homeworkDTOForNotPresenceException = new HomeworkDTO
        {
            Id = 1,
            StudentId = 1,
            LectureId = 1,
            StudentPresence = false,
            HomeworkPresence = false,
            Mark = 1,
            Date = new DateTime(2020,01,24)
        };

        private async Task<IEnumerable<Homework>> GetAllTest()
        {
            var homework = new List<Homework>
            {
                new Homework
                {
                    Id = 1,
                    StudentId = 1,
                    LectureId = 1,
                    StudentPresence = true,
                    HomeworkPresence = true,
                    Mark = 5,
                    Date = new DateTime(2020,01,24)
                },
                new Homework { 
                    Id = 2,
                    StudentId = 2,
                    LectureId = 2,
                    StudentPresence = false,
                    HomeworkPresence = false,
                    Mark = 0,
                    Date = new DateTime(2020,01,24)
                }
            };
            return homework;
        }
        
        private async Task<IEnumerable<Homework>> GetAllExceptionTest()
        {
            var homework = new List<Homework>();
            return homework;
        }
        
        private async Task<Homework> GetExceptionTest()
        {
            return null;
        }
        
        private async Task<Homework> GetTest()
        {
            var homework = new Homework
                {
                    Id = 1,
                    StudentId = 1,
                    LectureId = 1,
                    StudentPresence = true,
                    HomeworkPresence = true,
                    Mark = 5,
                    Date = new DateTime(2020,01,24)
                };
            return homework;
        }
        
        private HomeworkService HomeworkService { get; set; }
        private Mock<IRepository<Homework>> RepositoryMock { get; set; }
        private Mock<IStudentHomeworkUpdater> StudentHomeworkUpdaterMock { get; set; }
        
        [SetUp]
        public void SetUp()
        {
             RepositoryMock = new Mock<IRepository<Homework>>();
             StudentHomeworkUpdaterMock = new Mock<IStudentHomeworkUpdater>();
             RepositoryMock.Setup(repo => repo.GetAllAsync()).Returns(GetAllTest());
             RepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                 .Returns(GetTest());

             HomeworkService = new HomeworkService(RepositoryMock.Object, StudentHomeworkUpdaterMock.Object,
                 new MapperBll(), new NullLoggerFactory());
        }
        
        [Test]
        public void GetAllAsync_ValidCall()
        {
            var lectures = HomeworkService.GetAllAsync().Result.ToList();
        
            RepositoryMock.Verify(m => m.GetAllAsync());

            for (var i = 1; i < GetAllTest().Result.Count(); ++i)
            {
                Assert.AreEqual(GetAllTest().Result.ToList()[i].Id, lectures[i].Id); 
                Assert.AreEqual(GetAllTest().Result.ToList()[i].StudentId, lectures[i].StudentId);
                Assert.AreEqual(GetAllTest().Result.ToList()[i].LectureId, lectures[i].LectureId);
                Assert.AreEqual(GetAllTest().Result.ToList()[i].StudentPresence, lectures[i].StudentPresence);
                Assert.AreEqual(GetAllTest().Result.ToList()[i].HomeworkPresence, lectures[i].HomeworkPresence);
                Assert.AreEqual(GetAllTest().Result.ToList()[i].Mark, lectures[i].Mark);
                Assert.AreEqual(GetAllTest().Result.ToList()[i].Date, lectures[i].Date);
            }
        }
        
        [Test]
        public void GetAllAsync_ThrowsValidationException()
        {
            RepositoryMock.Setup(repo => repo.GetAllAsync()).Returns(GetAllExceptionTest());

            Assert.ThrowsAsync<ValidationException>(async () => await HomeworkService.GetAllAsync());
        }

        [Test]
        public void GetAsync_ValidCall()
        {
            const int id = 1;
            var lecture = HomeworkService.GetAsync(id).Result;
        
            RepositoryMock.Verify(m => m.GetAsync(id));
            
            Assert.AreEqual(GetTest().Result.Id, lecture.Id); 
            Assert.AreEqual(GetTest().Result.StudentId, lecture.StudentId); 
            Assert.AreEqual(GetTest().Result.LectureId, lecture.LectureId); 
            Assert.AreEqual(GetTest().Result.StudentPresence, lecture.StudentPresence); 
            Assert.AreEqual(GetTest().Result.HomeworkPresence, lecture.HomeworkPresence); 
            Assert.AreEqual(GetTest().Result.Mark, lecture.Mark); 
            Assert.AreEqual(GetTest().Result.Date, lecture.Date); 

        }
        
        [Test]
        public void GetAsync_ThrowsValidationException()
        {
            RepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());
            
            Assert.ThrowsAsync<ValidationException>(async () => await HomeworkService.GetAsync(null));
            Assert.ThrowsAsync<ValidationException>(async () => await HomeworkService.GetAsync(It.IsAny<int>()));
        }
        
        [Test]
        public async Task CreateAsync_ValidCall()
        {
            await HomeworkService.CreateAsync(_homeworkDTO);
            
            RepositoryMock.Verify(m => m.CreateAsync(It.IsAny<Homework>()));
            StudentHomeworkUpdaterMock.Verify(s => s.UpdateAsync(It.IsAny<Homework>(), 
                StudentHomeworkUpdater.UpdateType.AddHomework, It.IsAny<bool>()));
        }
        
        [Test]
        public void CreateAsync_ThrowsValidationException()
        {
            Assert.ThrowsAsync<ValidationException>(async () => await HomeworkService
                .CreateAsync(_homeworkDTOForPresenceException));
            Assert.ThrowsAsync<ValidationException>(async () => await HomeworkService
                .CreateAsync(_homeworkDTOForNotPresenceException));
        }

        [Test]
        public async Task UpdateAsync_ValidCall_WhenUpdateStudentWithOneId()
        {
            await HomeworkService.UpdateAsync(_homeworkDTO);
            
            RepositoryMock.Verify(m => m.Update(It.IsAny<Homework>()));
            StudentHomeworkUpdaterMock.Verify(s => s.UpdateAsync(It.IsAny<Homework>(), 
                StudentHomeworkUpdater.UpdateType.UpdateHomework, It.IsAny<bool>()));
        }
        
        [Test]
        public async Task UpdateAsync_ValidCall_WhenUpdateStudentWithDifferentId()
        {
            await HomeworkService.UpdateAsync(new HomeworkDTO() {
                Id = 1,
                StudentId = 2,
                LectureId = 1,
                StudentPresence = true,
                HomeworkPresence = true,
                Mark = 5,
                Date = new DateTime(2020,01,24)});
            
            RepositoryMock.Verify(m => m.Update(It.IsAny<Homework>()));
            StudentHomeworkUpdaterMock.Verify(s => s.UpdateAsync(It.IsAny<Homework>(), 
                StudentHomeworkUpdater.UpdateType.RemoveHomeworkWhileUpdate, It.IsAny<bool>()));
            StudentHomeworkUpdaterMock.Verify(s => s.UpdateAsync(It.IsAny<Homework>(), 
                StudentHomeworkUpdater.UpdateType.AddHomework, It.IsAny<bool>()));
        }
        
        [Test]
        public void UpdateAsync_ThrowsValidationException()
        {
            RepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());
            
            Assert.ThrowsAsync<ValidationException>(async () => await HomeworkService
                .UpdateAsync(_homeworkDTO));
            Assert.ThrowsAsync<ValidationException>(async () => await HomeworkService
                .UpdateAsync(_homeworkDTOForPresenceException));
            Assert.ThrowsAsync<ValidationException>(async () => await HomeworkService
                .UpdateAsync(_homeworkDTOForNotPresenceException));
        }

        [Test]
        public async Task DeleteAsync_ValidCall()
        {
            await HomeworkService.DeleteAsync(It.IsAny<int>());
            
            RepositoryMock.Verify(m => m.Delete(It.IsAny<Homework>()));
            StudentHomeworkUpdaterMock.Verify(s => s.UpdateAsync(It.IsAny<Homework>(), 
                StudentHomeworkUpdater.UpdateType.RemoveHomework, It.IsAny<bool>()));
        }

        [Test]
        public void DeleteAsync_ThrowsValidationException()
        {
            RepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());
            
            Assert.ThrowsAsync<ValidationException>(async () => await HomeworkService.DeleteAsync(null));
            Assert.ThrowsAsync<ValidationException>(async () => await HomeworkService
                .DeleteAsync(It.IsAny<int>()));
        }
    }
}