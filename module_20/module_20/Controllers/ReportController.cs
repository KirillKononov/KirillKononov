using System;
using System.Text;
using BLL.Interfaces;
using BLL.Interfaces.ServicesInterfaces;
using BLL.Services.Report.Serializers;
using Microsoft.AspNetCore.Mvc;

namespace module_20.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public enum FileType
        {
            JSON,
            XML
        }

        // GET : Report/Student
        [HttpGet("Student")]
        public IActionResult GetStudentReport(FileType type, string firstName, string lastName)
        {
            ISerializer serializer = null;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                return BadRequest();

            switch (type)
            {
                case FileType.JSON:
                {
                    serializer = new JsonAttendanceSerializer();
                    break;
                }
                case FileType.XML:
                {
                    serializer = new XmlAttendanceSerializer();
                    break;
                }
                default: 
                    return BadRequest();
            }

            var content = _reportService.MakeStudentReport(firstName, lastName, serializer.Serialize);
            return File(Encoding.UTF8.GetBytes(content),
                System.Net.Mime.MediaTypeNames.Application.Json, 
                $"{DateTime.Now.ToShortDateString()} - {firstName} {lastName} Attendance {type}.txt");
        }

        // GET : Report/Lecture
        [HttpGet("Lecture")]
        public IActionResult GetLectureReport(FileType type, string lectureName)
        {
            ISerializer serializer = null;

            if (string.IsNullOrEmpty(lectureName))
                return BadRequest();

            switch (type)
            {
                case FileType.JSON:
                {
                    serializer = new JsonAttendanceSerializer();
                    break;
                }
                case FileType.XML:
                {
                    serializer = new XmlAttendanceSerializer();
                    break;
                }
                default:
                    return BadRequest();
            }

            var content = _reportService.MakeLectureReport(lectureName, serializer.Serialize);
            return File(Encoding.UTF8.GetBytes(content),
                System.Net.Mime.MediaTypeNames.Application.Json,
                $"{DateTime.Now.ToShortDateString()} - {lectureName} Attendance {type}.txt");
        }
    }
}
