using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Mapper;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace Tests.BLLUnitTests
{
    public class LectureServiceTests
    {
        private readonly LectureDTO _lectureDTO = new LectureDTO
        {
            Id = 1,
            Name = "Math",
            ProfessorId = 1,
            LectureHomework = null
        };

        private LectureService LectureService { get; set; }
        
        private Mock<IRepository<Lecture>> Mock { get; set; }
        
        [SetUp]
        public void SetUp()
        {
             Mock = new Mock<IRepository<Lecture>>();
             Mock.Setup(repo => repo.GetAllAsync()).Returns(GetAllTest());
             Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                 .Returns(GetTest());

             LectureService = new LectureService(Mock.Object, new MapperBll(), new NullLoggerFactory());
        }
        
        [Test]
        public void GetAllAsync_ValidCall()
        {
            var lectures = LectureService.GetAllAsync().Result.ToList();
        
            Mock.Verify(m => m.GetAllAsync());

            for (var i = 1; i < GetAllTest().Result.Count(); ++i)
            {
                Assert.AreEqual(GetAllTest().Result.ToList()[i].Id, lectures[i].Id); 
                Assert.AreEqual(GetAllTest().Result.ToList()[i].Name, lectures[i].Name);
                Assert.AreEqual(GetAllTest().Result.ToList()[i].ProfessorId, lectures[i].ProfessorId);
            }
        }
        
        [Test]
        public void GetAllAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAllAsync()).Returns(GetAllExceptionTest());

            Assert.ThrowsAsync<ValidationException>(async () => await LectureService.GetAllAsync());
        }

        [Test]
        public void GetAsync_ValidCall()
        {
            const int id = 1;
            var lecture = LectureService.GetAsync(id).Result;
        
            Mock.Verify(m => m.GetAsync(id));
            Assert.AreEqual(GetTest().Result.Id, lecture.Id); 
            Assert.AreEqual(GetTest().Result.Name, lecture.Name);
            Assert.AreEqual(GetTest().Result.ProfessorId, lecture.ProfessorId);
        }
        
        [Test]
        public void GetAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());
            
            Assert.ThrowsAsync<ValidationException>(async () => await LectureService.GetAsync(null));
            Assert.ThrowsAsync<ValidationException>(async () => await LectureService.GetAsync(It.IsAny<int>()));
        }
        
        [Test]
        public async Task UpdateAsync_ValidCall()
        {
            await LectureService.UpdateAsync(_lectureDTO);
            
            Mock.Verify(m => m.Update(It.IsAny<Lecture>()));
        }

        [Test]
        public void UpdateAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());
            
            Assert.ThrowsAsync<ValidationException>(async () => await LectureService
                .UpdateAsync(_lectureDTO));
        }

        [Test]
        public async Task DeleteAsync_ValidCall()
        {
            await LectureService.DeleteAsync(It.IsAny<int>());
            
            Mock.Verify(m => m.Delete(It.IsAny<Lecture>()));
        }

        [Test]
        public void DeleteAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());
            
            Assert.ThrowsAsync<ValidationException>(async () => await LectureService.DeleteAsync(null));
            Assert.ThrowsAsync<ValidationException>(async () => await LectureService
                .DeleteAsync(It.IsAny<int>()));
        }
        
        private static async Task<IEnumerable<Lecture>> GetAllTest()
        {
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    Name = "Math",
                    ProfessorId = 1,
                    LectureHomework = null
                },
                new Lecture { 
                    Id = 2,
                    Name = "Robotics",
                    ProfessorId = 1,
                    LectureHomework = null
                }
            };
            return lectures;
        }
        
        private static async Task<IEnumerable<Lecture>> GetAllExceptionTest()
        {
            var lectures = new List<Lecture>();
            return lectures;
        }
        
        private static async Task<Lecture> GetExceptionTest()
        {
            return null;
        }
        
        private static async Task<Lecture> GetTest()
        {
            var lecture = new Lecture
            {
                Id = 1,
                Name = "Math",
                ProfessorId = 1,
                LectureHomework = null
            };
            return lecture;
        }
    }
}