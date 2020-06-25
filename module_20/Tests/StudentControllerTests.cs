using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces.ServicesInterfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using module_20.Controllers;
using module_20.DTO;
using module_20.Mapper;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    class StudentControllerTests
    {
        private async Task<StudentDTO> GetTest()
        {
            var student = new StudentDTO
            {
                Id = 1,
                FirstName = "Kirill",
                LastName = "Kononov",
                AverageMark = (float)4.67,
                MissedLectures = 0,
                StudentHomework = null
            };
            return student;
        }
        
        private async Task<StudentViewModel> ViewModel()
        {
            var student = new StudentViewModel
            {
                Id = 1,
                FirstName = "Kirill",
                LastName = "Kononov",
            };
            return student;
        }

        private IEnumerable<StudentDTO> PutNotFoundTest()
        {
            var student = new List<StudentDTO>();
            return student;
        }
        
        private IEnumerable<StudentDTO> PutTest()
        {
            var student = new List<StudentDTO>()
            {
                new StudentDTO()
                {
                    Id = 1,
                    FirstName = "Kirill",
                    LastName = "Kononov",
                    AverageMark = (float)4.67,
                    MissedLectures = 0,
                    StudentHomework = null
                }
            };
            return student;
        }

        private StudentController StudentController { get; set; }
        private Mock<IStudentService> Mock { get; set; }

        [SetUp]
        public void SetUp()
        {
            Mock = new Mock<IStudentService>();
            Mock.Setup(service => service.GetAsync(It.IsAny<int>()))
                .Returns(GetTest());
            Mock.Setup(service => service.CreateAsync(It.IsAny<StudentDTO>()))
                .Returns(ViewModel());
            Mock.Setup(service => service.UpdateAsync(It.IsAny<StudentDTO>()))
                .Returns(ViewModel());
            Mock.Setup(service => service.Find(It.IsAny<Func<Student, bool>>()))
                .Returns(PutTest());
            Mock.Setup(service => service.DeleteAsync(It.IsAny<int>()))
                .Returns(ViewModel());

            StudentController = new StudentController(Mock.Object, new MapperPL());
        }

        [Test]
        public async Task GetStudent_ValidCall()
        {
            var response = await StudentController.Get(1);
            var code = ((ObjectResult) response.Result).StatusCode;
            
            Assert.AreEqual((int) HttpStatusCode.OK, code);
        }

        [Test]
        public async Task GetStudent_BadRequest()
        {
            var response = await StudentController.Get(null);
            var code = response.Result as StatusCodeResult;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
        
        [Test]
        public async Task PostStudent_ValidCall()
        {
            var response = await StudentController.Post(ViewModel().Result);
            var code = ((ObjectResult) response.Result).StatusCode;
            
            Assert.AreEqual((int) HttpStatusCode.OK, code);
        }
        
        [Test]
        public async Task PostStudent_BadRequest()
        {
            var response = await StudentController.Post(null);
            var code = response.Result as StatusCodeResult;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
        
        [Test]
        public async Task PutStudent_ValidCall()
        {
            var response = await StudentController.Put(ViewModel().Result);
            var code = ((ObjectResult) response.Result).StatusCode;
            
            Assert.AreEqual((int) HttpStatusCode.OK, code);
        }
        
        [Test]
        public async Task PutStudent_BadRequest()
        {
            var response = await StudentController.Put(null);
            var code = response.Result as StatusCodeResult;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
        
        [Test]
        public async Task PutStudent_NotFound()
        {
            Mock.Setup(service => service.Find(It.IsAny<Func<Student, bool>>()))
                .Returns(PutNotFoundTest());
            
            var response = await StudentController.Put(ViewModel().Result);
            var code = response.Result as StatusCodeResult;
            
            Assert.AreEqual((int) HttpStatusCode.NotFound, code.StatusCode);
        }
        
        [Test]
        public async Task DeleteStudent_ValidCall()
        {
            var response = await StudentController.Delete(1);
            var code = ((ObjectResult) response.Result).StatusCode;
            
            Assert.AreEqual((int) HttpStatusCode.OK, code);
        }
        
        [Test]
        public async Task DeleteStudent_BadRequest()
        {
            var response = await StudentController.Delete(null);
            var code = response.Result as StatusCodeResult;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
    }
}
