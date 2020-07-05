using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using NUnit.Framework;
using module_20;
using Newtonsoft.Json;

namespace Tests.ControllersIntegrationTests
{
    [TestFixture]
    public class StudentControllerTests
    {
        private readonly HttpClient _client;

        public StudentControllerTests()
        {
            var appFactory = new CustomWebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }

        [Test]
        public async Task GetById_ValidCall()
        {
            const int id = 1;
            var response = await _client.GetAsync($"Student/{id}");
            var jsonResult = await response.Content.ReadAsStringAsync();
            var studentFromJson = JsonConvert.DeserializeObject<StudentDTO>(jsonResult);
            
            Assert.AreEqual(_studentList[0].Id, studentFromJson.Id);
            Assert.AreEqual(_studentList[0].FirstName, studentFromJson.FirstName);
            Assert.AreEqual(_studentList[0].LastName, studentFromJson.LastName);
            Assert.AreEqual(_studentList[0].AverageMark, studentFromJson.AverageMark);
            Assert.AreEqual(_studentList[0].MissedLectures, studentFromJson.MissedLectures);
            Assert.AreEqual(3, studentFromJson.StudentHomework.Count);
        }
        
        [Test]
        public async Task GetAll_ValidCall()
        {
            var response = await _client.GetAsync("Student");
            var jsonResult = await response.Content.ReadAsStringAsync();
            var studentFromJson = JsonConvert.DeserializeObject<List<StudentDTO>>(jsonResult);

            for (var i = 0; i < studentFromJson.Count - 1; i++)
            {
                Assert.AreEqual(_studentList[i].Id, studentFromJson[i].Id);
                Assert.AreEqual(_studentList[i].FirstName, studentFromJson[i].FirstName);
                Assert.AreEqual(_studentList[i].LastName, studentFromJson[i].LastName);
                Assert.AreEqual(_studentList[i].AverageMark, studentFromJson[i].AverageMark);
                Assert.AreEqual(_studentList[i].MissedLectures, studentFromJson[i].MissedLectures);
                Assert.AreEqual(3, studentFromJson[i].StudentHomework.Count);
            }
        }

        [Test]
        public async Task Post_ValidCall()
        {
            const string student = "{\"id\": 0, \"firstName\": \"Dima\", \"lastName\": \"Menyaev\"}";
            var response = await _client.PostAsync("Student", 
                new StringContent(student, Encoding.UTF8, "application/json"));
            var jsonResult = await response.Content.ReadAsStringAsync();
            var studentFromJson = JsonConvert.DeserializeObject<StudentDTO>(jsonResult);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Dima", studentFromJson.FirstName);
            Assert.AreEqual("Menyaev", studentFromJson.LastName);
            Assert.AreEqual(0, studentFromJson.AverageMark);
            Assert.AreEqual(0, studentFromJson.MissedLectures);
            Assert.AreEqual(null, studentFromJson.StudentHomework);
        }
        
        [Test]
        public async Task Put_ValidCall()
        {
            const string student = "{\"id\": 2, \"firstName\": \"Ivan\", \"lastName\": \"Petrov\"}";
            var response = await _client.PutAsync("Student", 
                new StringContent(student, Encoding.UTF8, "application/json"));
            var jsonResult = await response.Content.ReadAsStringAsync();
            var studentFromJson = JsonConvert.DeserializeObject<StudentDTO>(jsonResult);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(2, studentFromJson.Id);
            Assert.AreEqual("Ivan", studentFromJson.FirstName);
            Assert.AreEqual("Petrov", studentFromJson.LastName);
            Assert.AreEqual(0, studentFromJson.AverageMark);
            Assert.AreEqual(0, studentFromJson.MissedLectures);
            Assert.AreEqual(null, studentFromJson.StudentHomework);
        }
        
        [Test]
        public async Task Delete_ValidCall()
        {
            const int id = 5;
            var response = await _client.DeleteAsync($"Student/{id}");
            var jsonResult = await response.Content.ReadAsStringAsync();
            var studentFromJson = JsonConvert.DeserializeObject<StudentDTO>(jsonResult);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(_studentList[4].Id, studentFromJson.Id);
            Assert.AreEqual(_studentList[4].FirstName, studentFromJson.FirstName);
            Assert.AreEqual(_studentList[4].LastName, studentFromJson.LastName);
            Assert.AreEqual(_studentList[4].AverageMark, studentFromJson.AverageMark);
            Assert.AreEqual(_studentList[4].MissedLectures, studentFromJson.MissedLectures);
            Assert.AreEqual(3, studentFromJson.StudentHomework.Count);
        }
        
        private readonly List<StudentDTO> _studentList = new List<StudentDTO>()
        {
            new StudentDTO
            {
                Id = 1,
                FirstName = "Kirill",
                LastName = "Kononov",
                AverageMark = (float) 4.67,
                MissedLectures = 0,
            },

            new StudentDTO 
            { 
                Id = 2, 
                FirstName = "Ivan",
                LastName = "Ivanov",
                AverageMark = (float) 0,
                MissedLectures = 3
            },
        
            new StudentDTO { 
                Id = 3,
                FirstName = "Semen",
                LastName = "Petrov",
                AverageMark = (float) 4.0, 
                MissedLectures = 0
            },
        
            new StudentDTO { 
                Id = 4, 
                FirstName = "Dennis", 
                LastName = "Gavrilov",
                AverageMark = (float) 1.67, 
                MissedLectures = 2
            },
        
            new StudentDTO { 
                Id = 5,
                FirstName = "Anton", 
                LastName = "Antipov",
                AverageMark = (float) 3.67, 
                MissedLectures = 0
            }
        };
    }
}