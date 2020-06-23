using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Mapper;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Extensions.Logging.Abstractions;
using module_20.Controllers;
using module_20.Mapper;
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
        
        private readonly StudentDTO _studentDTOSemen = new StudentDTO
        {
            Id = 2,
            FirstName = "Semen",
            LastName = "Petrov",
            AverageMark = 4,
            MissedLectures = 2,
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
        
        private IEnumerable<Student> FindTest()
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
                }
            };
            return users;
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
             Mock.Setup(repo => repo.Update(It.IsAny<Student>()));
             Mock.Setup(repo => repo.CreateAsync(It.IsAny<Student>()));
             Mock.Setup(repo => repo.Delete(It.IsAny<int>()));
             Mock.Setup(repo => repo.Find(It.IsAny<Func<Student, bool>>()))
                 .Returns(FindTest());
             
             StudentService = new StudentService(Mock.Object, new MapperBll(), new NullLoggerFactory());
        }
        
        [Test]
        public void GetAllAsync_ValidCall()
        {
            var students = StudentService.GetAllAsync().Result.ToList();
        
            Mock.Verify(m => m.GetAllAsync());
            Assert.AreEqual(_studentDTOKirill.Id, students[0].Id); 
            Assert.AreEqual(_studentDTOKirill.FirstName, students[0].FirstName);
            Assert.AreEqual(_studentDTOKirill.LastName, students[0].LastName);
            Assert.AreEqual(_studentDTOKirill.MissedLectures, students[0].MissedLectures);
            Assert.AreEqual(_studentDTOKirill.AverageMark, students[0].AverageMark);
            
            Assert.AreEqual(_studentDTOSemen.Id, students[1].Id); 
            Assert.AreEqual(_studentDTOSemen.FirstName, students[1].FirstName);
            Assert.AreEqual(_studentDTOSemen.LastName, students[1].LastName);
            Assert.AreEqual(_studentDTOSemen.MissedLectures, students[1].MissedLectures);
            Assert.AreEqual(_studentDTOSemen.AverageMark, students[1].AverageMark);
        }

        [Test]
        public void GetAsync_ValidCall()
        {
            const int id = 1;
            var student = StudentService.GetAsync(id).Result;
        
            Mock.Verify(m => m.GetAsync(id));
            Assert.AreEqual(_studentDTOKirill.Id, student.Id); 
            Assert.AreEqual(_studentDTOKirill.FirstName, student.FirstName);
            Assert.AreEqual(_studentDTOKirill.LastName, student.LastName);
            Assert.AreEqual(_studentDTOKirill.MissedLectures, student.MissedLectures);
            Assert.AreEqual(_studentDTOKirill.AverageMark, student.AverageMark);
        }
        
        [Test]
        public async Task UpdateAsync_ValidCall()
        {
            await StudentService.UpdateAsync(_studentDTOKirill);
            
            Mock.Verify(m => m.Update(Mock.Object.GetAsync(_studentDTOKirill.Id).Result));
        }

        [Test]
        public void Find_ValidCall()
        {
            var students = StudentService.Find(s => s.Id == 1).ToList();
            
            Assert.AreEqual(_studentDTOKirill.Id, students[0].Id); 
            Assert.AreEqual(_studentDTOKirill.FirstName, students[0].FirstName);
            Assert.AreEqual(_studentDTOKirill.LastName, students[0].LastName);
            Assert.AreEqual(_studentDTOKirill.MissedLectures, students[0].MissedLectures);
            Assert.AreEqual(_studentDTOKirill.AverageMark, students[0].AverageMark);
        }
         
        [Test]
        public void DeleteAsync_ValidCall()
        {
            const int id = 1;
            StudentService.DeleteAsync(id);
            
            Mock.Verify(m => m.Delete(id));
        }
    }
}