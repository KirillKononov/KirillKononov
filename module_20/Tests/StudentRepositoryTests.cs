using System.Linq;
using BLL.DTO;
using NUnit.Framework;
using DAL.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace Tests
{
    [TestFixture]
    public class StudentRepositoryTests
    {
        // private StudentService _studentService;
        //
        // private readonly StudentDTO _studentDto = new StudentDTO
        // {
        //     Id = 1,
        //     FirstName = "Kirill",
        //     LastName = "Kononov",
        //     AverageMark = (float) 4.67,
        //     MissedLectures = 0,
        //     StudentHomework = null
        // };
        //
        // [SetUp]
        // public void SetUp()
        // {
        //     var options = new DbContextOptionsBuilder<DataBaseContext>()
        //         .UseInMemoryDatabase(databaseName: "StudentRepositoryTest")
        //         .Options;
        //     var context = new DataBaseContext(options);
        //     _studentService = new StudentService(context, new MapperBll().CreateMapper(), new NullLoggerFactory());
        // }
        //
        // [Test]
        // public void When_GetAllCalled_CorrectAnswerReturned()
        // {
        //     var students = _studentService.GetAllAsync().Result.ToList();
        //
        //     Assert.AreEqual(_studentDto.Id, students[0].FirstName); 
        //     Assert.AreEqual(_studentDto.LastName, students[0].LastName);
        //     Assert.AreEqual(_studentDto.MissedLectures, students[0].MissedLectures);
        //     Assert.AreEqual(_studentDto.AverageMark, students[0].AverageMark);
        // }
    }
}