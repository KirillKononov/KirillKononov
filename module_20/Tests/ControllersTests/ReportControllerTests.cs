using System;
using System.Collections.Generic;
using System.Net;
using BLL.DTO;
using BLL.Interfaces.ServicesInterfaces;
using BLL.Services.Report.Serializers;
using Microsoft.AspNetCore.Mvc;
using module_20.Controllers;
using Moq;
using NUnit.Framework;

namespace Tests.ControllersTests
{
    [TestFixture]
    public class ReportControllerTests
    {
        private ReportController ReportController { get; set; }
        
        private Mock<IReportService> Mock { get; set; }

        [SetUp]
        public void SetUp()
        {
            Mock = new Mock<IReportService>();
            Mock.Setup(service => service.MakeStudentReport(It.IsAny<string>(), 
                    It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeJsonReportData());
            Mock.Setup(service => service.MakeLectureReport(It.IsAny<string>(), 
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeJsonReportData());

            ReportController = new ReportController(Mock.Object);
        }

        [Test]
        public void GetStudentJsonReport_ValidCall()
        {
           var response = ReportController.GetStudentReport(ReportController.FileType.JSON, 
               "Kirill", "Kononov");

            Mock.Verify(m => m.MakeStudentReport(It.IsAny<string>(), 
                It.IsAny<string>(),
                It.IsAny<Func<IEnumerable<Attendance>, string>>()));
            Assert.IsInstanceOf<FileContentResult>(response);
        }
        
        [Test]
        public void GetStudentJsonReport_BadRequestResult()
        {
            var response = ReportController.GetStudentReport(ReportController.FileType.JSON,
                null, "Kononov");
            var code = (StatusCodeResult) response;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
        
        [Test]
        public void GetLectureJsonReport_ValidCall()
        {
            var response = ReportController.GetLectureReport(ReportController.FileType.JSON,
                "Math");

            Mock.Verify(m => m.MakeLectureReport(It.IsAny<string>(),
                It.IsAny<Func<IEnumerable<Attendance>, string>>()));
            Assert.IsInstanceOf<FileContentResult>(response);
        }
        
        [Test]
        public void GetLectureJsonReport_BadRequestResult()
        {
            var response = ReportController.GetLectureReport(ReportController.FileType.JSON,
                null);
            var code = (StatusCodeResult) response;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
        
        [Test]
        public void GetStudentXmlReport_ValidCall()
        {
            Mock.Setup(service => service.MakeStudentReport(It.IsAny<string>(), 
                    It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeXmlReportData);
            Mock.Setup(service => service.MakeLectureReport(It.IsAny<string>(), 
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeXmlReportData);
            
            var response = ReportController.GetStudentReport(ReportController.FileType.XML, 
                "Kirill", "Kononov");

            Mock.Verify(m => m.MakeStudentReport(It.IsAny<string>(), 
                It.IsAny<string>(),
                It.IsAny<Func<IEnumerable<Attendance>, string>>()));
            Assert.IsInstanceOf<FileContentResult>(response);
        }
        
        [Test]
        public void GetStudentXmlReport_BadRequestResult()
        {
            Mock.Setup(service => service.MakeStudentReport(It.IsAny<string>(), 
                    It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeXmlReportData);
            Mock.Setup(service => service.MakeLectureReport(It.IsAny<string>(), 
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeXmlReportData);
            
            var response = ReportController.GetStudentReport(ReportController.FileType.XML,
                null, "Kononov");
            var code = (StatusCodeResult) response;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
        
        [Test]
        public void GetLectureXmlReport_ValidCall()
        {
            Mock.Setup(service => service.MakeStudentReport(It.IsAny<string>(), 
                    It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeXmlReportData);
            Mock.Setup(service => service.MakeLectureReport(It.IsAny<string>(), 
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeXmlReportData);
            
            var response = ReportController.GetLectureReport(ReportController.FileType.XML,
                "Math");

            Mock.Verify(m => m.MakeLectureReport(It.IsAny<string>(),
                It.IsAny<Func<IEnumerable<Attendance>, string>>()));
            Assert.IsInstanceOf<FileContentResult>(response);
        }
        
        [Test]
        public void GetLectureXmlReport_BadRequestResult()
        {
            Mock.Setup(service => service.MakeStudentReport(It.IsAny<string>(), 
                    It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeXmlReportData);
            Mock.Setup(service => service.MakeLectureReport(It.IsAny<string>(), 
                    It.IsAny<Func<IEnumerable<Attendance>, string>>()))
                .Returns(MakeXmlReportData);
            
            var response = ReportController.GetLectureReport(ReportController.FileType.XML,
                null);
            var code = (StatusCodeResult) response;
            
            Assert.AreEqual((int) HttpStatusCode.BadRequest, code.StatusCode);
        }
        
        private static string MakeJsonReportData()
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
        
        private static string MakeXmlReportData()
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
            var serializer = new XmlAttendanceSerializer();
            return serializer.Serialize(attendance);
        }
    }
}