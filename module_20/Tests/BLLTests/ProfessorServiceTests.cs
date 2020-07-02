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

namespace Tests.BLLTests
{
    public class ProfessorServiceTests
    {
        private readonly ProfessorDTO _professorDTO = new ProfessorDTO
        {
            Id = 1,
            FirstName = "Kirill",
            LastName = "Kononov",
            Lectures = null
        };

        private ProfessorService ProfessorService { get; set; }
        
        private Mock<IRepository<Professor>> Mock { get; set; }
        
        [SetUp]
        public void SetUp()
        {
             Mock = new Mock<IRepository<Professor>>();
             Mock.Setup(repo => repo.GetAllAsync()).Returns(GetAllTest());
             Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                 .Returns(GetTest());

             ProfessorService = new ProfessorService(Mock.Object, new MapperBll(), new NullLoggerFactory());
        }
        
        [Test]
        public void GetAllAsync_ValidCall()
        {
            var professors = ProfessorService.GetAllAsync().Result.ToList();
        
            Mock.Verify(m => m.GetAllAsync());

            for (var i = 1; i < GetAllTest().Result.Count(); ++i)
            {
                Assert.AreEqual(GetAllTest().Result.ToList()[i].Id, professors[i].Id); 
                Assert.AreEqual(GetAllTest().Result.ToList()[i].FirstName, professors[i].FirstName);
                Assert.AreEqual(GetAllTest().Result.ToList()[i].LastName, professors[i].LastName);
            }
        }
        
        [Test]
        public void GetAllAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAllAsync()).Returns(GetAllExceptionTest());

            Assert.ThrowsAsync<ValidationException>(async () => await ProfessorService.GetAllAsync());
        }

        [Test]
        public void GetAsync_ValidCall()
        {
            const int id = 1;
            var professor = ProfessorService.GetAsync(id).Result;
        
            Mock.Verify(m => m.GetAsync(id));
            Assert.AreEqual(GetTest().Result.Id, professor.Id); 
            Assert.AreEqual(GetTest().Result.FirstName, professor.FirstName);
            Assert.AreEqual(GetTest().Result.LastName, professor.LastName);
        }
        
        [Test]
        public void GetAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());
            
            Assert.ThrowsAsync<ValidationException>(async () => await ProfessorService.GetAsync(null));
            Assert.ThrowsAsync<ValidationException>(async () => await ProfessorService.GetAsync(It.IsAny<int>()));
        }
        
        [Test]
        public async Task UpdateAsync_ValidCall()
        {
            await ProfessorService.UpdateAsync(_professorDTO);
            
            Mock.Verify(m => m.Update(It.IsAny<Professor>()));
        }

        [Test]
        public void UpdateAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());
            
            Assert.ThrowsAsync<ValidationException>(async () => await ProfessorService
                .UpdateAsync(_professorDTO));
        }

        [Test]
        public async Task DeleteAsync_ValidCall()
        {
            await ProfessorService.DeleteAsync(It.IsAny<int>());
            
            Mock.Verify(m => m.Delete(It.IsAny<Professor>()));
        }

        [Test]
        public void DeleteAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(GetExceptionTest());
            
            Assert.ThrowsAsync<ValidationException>(async () => await ProfessorService.DeleteAsync(null));
            Assert.ThrowsAsync<ValidationException>(async () => await ProfessorService
                .DeleteAsync(It.IsAny<int>()));
        }
        
        private static async Task<IEnumerable<Professor>> GetAllTest()
        {
            var professors = new List<Professor>
            {
                new Professor
                {
                    Id = 1,
                    FirstName = "Kirill",
                    LastName = "Kononov",
                    Lectures = null
                },
                new Professor { 
                    Id = 2,
                    FirstName = "Semen",
                    LastName = "Petrov",
                    Lectures = null
                }
            };
            return professors;
        }
        
        private static async Task<IEnumerable<Professor>> GetAllExceptionTest()
        {
            var professors = new List<Professor>();
            return professors;
        }
        
        private static async Task<Professor> GetExceptionTest()
        {
            return null;
        }
        
        private static async Task<Professor> GetTest()
        {
            var professor = new Professor
            {
                Id = 1,
                FirstName = "Kirill",
                LastName = "Kononov",
                Lectures = null
            };
            return professor;
        }
    }
}
