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

namespace Tests
{
    [TestFixture]
    public class Test
    {
        private readonly StudentDTO _studentDTOKirill = new StudentDTO
        {
            Id = 1,
            FirstName = "Kirill",
            LastName = "Kononov",
            AverageMark = (float) 4.67,
            MissedLectures = 0,
            StudentHomework = null
        };

        private async Task<IEnumerable<Student>> GetAllTest()
        {
            var users = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    FirstName = "Kirill",
                    LastName = "Kononov",
                    AverageMark = (float) 4.67,
                    MissedLectures = 0,
                    StudentHomework = null
                },
                new Student { Id = 2,
                    FirstName = "Semen",
                    LastName = "Petrov",
                    AverageMark = 4,
                    MissedLectures = 2,
                    StudentHomework = null }
            };
            return users;
        }
        
        private async Task<IEnumerable<Student>> GetAllExceptionTest()
        {
            var users = new List<Student>();
            return users;
        }
        
        private async Task<Student> GetExceptionTest()
        {
            var user = new Student();
            return user;
        }
        
        private async Task<Student> GetTest()
        {
            var user = new Student
                {
                    Id = 1,
                    FirstName = "Kirill",
                    LastName = "Kononov",
                    AverageMark = (float) 4.67,
                    MissedLectures = 0,
                    StudentHomework = null
                };
            return user;
        }
        
        private StudentService StudentService { get; set; }
        private Mock<IRepository<Student>> Mock { get; set; }
        
        [SetUp]
        public void SetUp()
        {
             Mock = new Mock<IRepository<Student>>();
             Mock.Setup(repo => repo.GetAllAsync()).Returns(GetAllTest());
             Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                 .Returns(GetTest());

             StudentService = new StudentService(Mock.Object, new MapperBll(), new NullLoggerFactory());
        }
        
        [Test]
        public void GetAllAsync_ValidCall()
        {
            var students = StudentService.GetAllAsync().Result.ToList();
        
            Mock.Verify(m => m.GetAllAsync());

            for (var i = 1; i < GetAllTest().Result.Count(); ++i)
            {
                Assert.AreEqual(GetAllTest().Result.ToList()[i].Id, students[i].Id); 
                Assert.AreEqual(GetAllTest().Result.ToList()[i].FirstName, students[i].FirstName);
                Assert.AreEqual(GetAllTest().Result.ToList()[i].LastName, students[i].LastName);
                Assert.AreEqual(GetAllTest().Result.ToList()[i].MissedLectures, students[i].MissedLectures);
                Assert.AreEqual(GetAllTest().Result.ToList()[i].AverageMark, students[i].AverageMark);
            }
        }
        
        [Test]
        public void GetAllAsync_ThrowsValidationException()
        {
            Mock.Setup(repo => repo.GetAllAsync()).Returns(GetAllExceptionTest());

            Assert.ThrowsAsync<ValidationException>(async () => await StudentService.GetAllAsync());
        }

        [Test]
        public void GetAsync_ValidCall()
        {
            const int id = 1;
            var student = StudentService.GetAsync(id).Result;
        
            Mock.Verify(m => m.GetAsync(id));
            Assert.AreEqual(GetTest().Result.Id, student.Id); 
            Assert.AreEqual(GetTest().Result.FirstName, student.FirstName);
            Assert.AreEqual(GetTest().Result.LastName, student.LastName);
            Assert.AreEqual(GetTest().Result.MissedLectures, student.MissedLectures);
            Assert.AreEqual(GetTest().Result.AverageMark, student.AverageMark);
        }
        
        [Test]
        public void GetAsync_ThrowsValidationException()
        {
            //Mock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
            //    .Returns(GetExceptionTest());
            
            Assert.ThrowsAsync<ValidationException>(async () => await StudentService.GetAsync(null));
            //Assert.ThrowsAsync<ValidationException>(async () => await StudentService.GetAsync(1));
        }
        
        [Test]
        public async Task UpdateAsync_ValidCall()
        {
            await StudentService.UpdateAsync(_studentDTOKirill);
            
            Mock.Verify(m => m.Update(It.IsAny<Student>()));
        }

        //[Test]
        //public void UpdateAsync_ThrowsValidationException()
        //{
        //    Assert.ThrowsAsync<ValidationException>(async () => await StudentService.UpdateAsync(null));
        //}

        [Test]
        public async Task DeleteAsync_ValidCall()
        {
            const int id = 1;
            await StudentService.DeleteAsync(id);
            
            Mock.Verify(m => m.Delete(It.IsAny<Student>()));
        }

        [Test]
        public void DeleteAsync_ThrowsValidationException()
        {
            Assert.ThrowsAsync<ValidationException>(async () => await StudentService.DeleteAsync(null));
        }
    }
}