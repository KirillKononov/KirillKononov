using System;
using System.Collections.Generic;
using System.Net;
using BLL.DTO;
using BLL.Interfaces.ServicesInterfaces;
using BLL.Services.Report.Serializers;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using module_20.Controllers;
using module_20.Mapper;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ReportControllerTests
    {
        private string MakeReportData()
        {
            var attendance = new List<Attendance>()
            {
                new Attendance()
                {
                    LectureName = "Math",
                    ProfessorName = "Kirill Kononov",
                    StudentName = "Kirill Kononov",
                    HomeworkPresence = true,
                    StudentPresence = true,
                    Mark = 4,
                    Date = DateTime.Now
                }    
            };
            var serializer = new JsonAttendanceSerializer();
            return serializer.Serialize(attendance);
        }

        private ReportController ReportController { get; set; }
        private Mock<IReportService> Mock { get; set; }

        [SetUp]
        public void SetUp()
        {
            Mock = new Mock<IReportService>();
            Mock.Setup(service => service.MakeStudentReport(It.IsAny<string>(), 
                    It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeReportData());
            Mock.Setup(service => service.MakeLectureReport(It.IsAny<string>(), 
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeReportData());

            ReportController = new ReportController(Mock.Object);
        }

        [Test]
        public void GetStudentReport_ValidCall()
        {
           ReportController.GetStudentReport(ReportController.FileType.JSON, "Kirill", "Kononov");

            Mock.Verify(m => m.MakeStudentReport(It.IsAny<string>(), 
                It.IsAny<string>(),
                It.IsAny<Func<IEnumerable<Attendance>, string>>()));
        }
        
        [Test]
        public void GetStudentReport_BadRequestResult()
        {
            var response = ReportController.GetStudentReport(ReportController.FileType.JSON, null, "Kononov");
            var code = (StatusCodeResult) response;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
        
        [Test]
        public void GetLectureReport_ValidCall()
        {
            ReportController.GetLectureReport(ReportController.FileType.JSON, "Math");

            Mock.Verify(m => m.MakeLectureReport(It.IsAny<string>(),
                It.IsAny<Func<IEnumerable<Attendance>, string>>()));
        }
        
        [Test]
        public void GetLectureReport_BadRequestResult()
        {
            var response = ReportController.GetLectureReport(ReportController.FileType.JSON, "Math");
            var code = (StatusCodeResult) response;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
    }
}