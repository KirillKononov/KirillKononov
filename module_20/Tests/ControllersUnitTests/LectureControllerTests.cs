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

namespace Tests.ControllersUnitTests
{
    public class LectureControllerTests
    {
        private LectureController LectureController { get; set; }
        
        private Mock<ILectureService> Mock { get; set; }

        [SetUp]
        public void SetUp()
        {
            Mock = new Mock<ILectureService>();
            Mock.Setup(service => service.GetAsync(It.IsAny<int>()))
                .Returns(GetTest());
            Mock.Setup(service => service.CreateAsync(It.IsAny<LectureDTO>()))
                .Returns(ViewModel());
            Mock.Setup(service => service.UpdateAsync(It.IsAny<LectureDTO>()))
                .Returns(ViewModel());
            Mock.Setup(service => service.Find(It.IsAny<Func<Lecture, bool>>()))
                .Returns(PutFindTest());
            Mock.Setup(service => service.DeleteAsync(It.IsAny<int>()))
                .Returns(ViewModel());

            LectureController = new LectureController(Mock.Object, new MapperPL());
        }

        [Test]
        public async Task GetLecture_ValidCall()
        {
            var response = await LectureController.Get(1);
            var code = ((ObjectResult) response.Result).StatusCode;
            
            Assert.AreEqual((int) HttpStatusCode.OK, code);
        }

        [Test]
        public async Task GetLecture_BadRequest()
        {
            var response = await LectureController.Get(null);
            var code = (StatusCodeResult) response.Result;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
        
        [Test]
        public async Task PostLecture_ValidCall()
        {
            var response = await LectureController.Post(ViewModel().Result);
            var code = ((ObjectResult) response.Result).StatusCode;
            
            Assert.AreEqual((int) HttpStatusCode.OK, code);
        }
        
        [Test]
        public async Task PostLecture_BadRequest()
        {
            var response = await LectureController.Post(null);
            var code = (StatusCodeResult) response.Result;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
        
        [Test]
        public async Task PutLecture_ValidCall()
        {
            var response = await LectureController.Put(ViewModel().Result);
            var code = ((ObjectResult) response.Result).StatusCode;
            
            Assert.AreEqual((int) HttpStatusCode.OK, code);
        }
        
        [Test]
        public async Task PutLecture_BadRequest()
        {
            var response = await LectureController.Put(null);
            var code = (StatusCodeResult) response.Result;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
        
        [Test]
        public async Task PutLecture_NotFound()
        {
            Mock.Setup(service => service.Find(It.IsAny<Func<Lecture, bool>>()))
                .Returns(PutNotFoundTest());
            
            var response = await LectureController.Put(ViewModel().Result);
            var code = (StatusCodeResult) response.Result;
            
            Assert.AreEqual((int) HttpStatusCode.NotFound, code.StatusCode);
        }
        
        [Test]
        public async Task DeleteLecture_ValidCall()
        {
            var response = await LectureController.Delete(1);
            var code = ((ObjectResult) response.Result).StatusCode;
            
            Assert.AreEqual((int) HttpStatusCode.OK, code);
        }
        
        [Test]
        public async Task DeleteLecture_BadRequest()
        {
            var response = await LectureController.Delete(null);
            var code = (StatusCodeResult) response.Result;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
        
        private static async Task<LectureDTO> GetTest()
        {
            var lecture = new LectureDTO()
            {
                Id = 1,
                Name = "Math",
                ProfessorId = 1,
                LectureHomework = null
            };
            return lecture;
        }
        
        private static async Task<LectureViewModel> ViewModel()
        {
            var lecture = new LectureViewModel
            {
                Id = 1,
                Name = "Math",
                ProfessorId = 1,
            };
            return lecture;
        }

        private static IEnumerable<LectureDTO> PutNotFoundTest()
        {
            var lecture = new List<LectureDTO>();
            return lecture;
        }
        
        private static IEnumerable<LectureDTO> PutFindTest()
        {
            var lecture = new List<LectureDTO>()
            {
                new LectureDTO()
                {
                    Id = 1,
                    Name = "Math",
                    ProfessorId = 1,
                    LectureHomework = null
                }
            };
            return lecture;
        }
    }
}