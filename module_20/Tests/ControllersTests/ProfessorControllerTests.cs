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

namespace Tests.ControllersTests
{
    [TestFixture]
    public class ProfessorControllerTests
    {
        private ProfessorController ProfessorController { get; set; }
        
        private Mock<IProfessorService> Mock { get; set; }

        [SetUp]
        public void SetUp()
        {
            Mock = new Mock<IProfessorService>();
            Mock.Setup(service => service.GetAsync(It.IsAny<int>()))
                .Returns(GetTest());
            Mock.Setup(service => service.CreateAsync(It.IsAny<ProfessorDTO>()))
                .Returns(ViewModel());
            Mock.Setup(service => service.UpdateAsync(It.IsAny<ProfessorDTO>()))
                .Returns(ViewModel());
            Mock.Setup(service => service.Find(It.IsAny<Func<Professor, bool>>()))
                .Returns(PutFindTest());
            Mock.Setup(service => service.DeleteAsync(It.IsAny<int>()))
                .Returns(ViewModel());

            ProfessorController = new ProfessorController(Mock.Object, new MapperPL());
        }

        [Test]
        public async Task GetProfessor_ValidCall()
        {
            var response = await ProfessorController.Get(1);
            var code = ((ObjectResult) response.Result).StatusCode;
            
            Assert.AreEqual((int) HttpStatusCode.OK, code);
        }

        [Test]
        public async Task GetProfessor_BadRequest()
        {
            var response = await ProfessorController.Get(null);
            var code = (StatusCodeResult) response.Result;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
        
        [Test]
        public async Task PostProfessor_ValidCall()
        {
            var response = await ProfessorController.Post(ViewModel().Result);
            var code = ((ObjectResult) response.Result).StatusCode;
            
            Assert.AreEqual((int) HttpStatusCode.OK, code);
        }
        
        [Test]
        public async Task PostProfessor_BadRequest()
        {
            var response = await ProfessorController.Post(null);
            var code = (StatusCodeResult) response.Result;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
        
        [Test]
        public async Task PutProfessor_ValidCall()
        {
            var response = await ProfessorController.Put(ViewModel().Result);
            var code = ((ObjectResult) response.Result).StatusCode;
            
            Assert.AreEqual((int) HttpStatusCode.OK, code);
        }
        
        [Test]
        public async Task PutProfessor_BadRequest()
        {
            var response = await ProfessorController.Put(null);
            var code = (StatusCodeResult) response.Result;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
        
        [Test]
        public async Task PutProfessor_NotFound()
        {
            Mock.Setup(service => service.Find(It.IsAny<Func<Professor, bool>>()))
                .Returns(PutNotFoundTest());
            
            var response = await ProfessorController.Put(ViewModel().Result);
            var code = (StatusCodeResult) response.Result;
            
            Assert.AreEqual((int) HttpStatusCode.NotFound, code.StatusCode);
        }
        
        [Test]
        public async Task DeleteProfessor_ValidCall()
        {
            var response = await ProfessorController.Delete(1);
            var code = ((ObjectResult) response.Result).StatusCode;
            
            Assert.AreEqual((int) HttpStatusCode.OK, code);
        }
        
        [Test]
        public async Task DeleteProfessor_BadRequest()
        {
            var response = await ProfessorController.Delete(null);
            var code = (StatusCodeResult) response.Result;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
        
        private static async Task<ProfessorDTO> GetTest()
        {
            var professor = new ProfessorDTO()
            {
                Id = 1,
                FirstName = "Kirill",
                LastName = "Kononov",
                Lectures = null
            };
            return professor;
        }
        
        private static async Task<ProfessorViewModel> ViewModel()
        {
            var professor = new ProfessorViewModel
            {
                Id = 1,
                FirstName = "Kirill",
                LastName = "Kononov",
            };
            return professor;
        }

        private static IEnumerable<ProfessorDTO> PutNotFoundTest()
        {
            var professor = new List<ProfessorDTO>();
            return professor;
        }
        
        private static IEnumerable<ProfessorDTO> PutFindTest()
        {
            var professor = new List<ProfessorDTO>()
            {
                new ProfessorDTO()
                {
                    Id = 1,
                    FirstName = "Kirill",
                    LastName = "Kononov",
                    Lectures = null
                }
            };
            return professor;
        }
    }
}