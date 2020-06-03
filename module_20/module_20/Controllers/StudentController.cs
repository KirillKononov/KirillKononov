using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using module_20.DTO;

namespace module_20.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _db;

        public StudentController(IUnitOfWork uow)
        {
            _db = uow;
        }

        // GET: Student
        [HttpGet]
        public ActionResult<IEnumerable<StudentDTO>> Get()
        {
            return _db.Students.GetAll().ToList();
        }

        // GET: Student/5
        [HttpGet("{id}")]
        public ActionResult<StudentDTO> Get(int? id)
        {
            if (id == null)
                BadRequest();

            var student = _db.Students.Get(id);
            return new ObjectResult(student);
        }

        // POST: Student
        [HttpPost]
        public ActionResult<StudentDTO> Post(StudentPl studentPl)
        {
            if (studentPl == null)
                BadRequest();

            var student = createStudentDTO(studentPl);
            _db.Students.Create(student);
            _db.Save();
            return Ok(student);
        }

        // PUT: Student
        [HttpPut]
        public ActionResult<StudentDTO> Put(StudentPl studentPl)
        {
            if (studentPl == null)
                BadRequest();

            if (!_db.Students.Find(s => s.Id == studentPl.Id).Any())
                NotFound();

            var student = createStudentDTO(studentPl);
            _db.Students.Update(student);
            _db.Save();
            return Ok(student);
        }

        // DELETE: Student/5
        [HttpDelete("{id}")]
        public ActionResult<StudentDTO> Delete(int? id)
        {
            if (id == null)
                BadRequest();

            var student = _db.Students.Get(id);
            _db.Students.Delete(id);
            _db.Save();
            return Ok(student);
        }

        private StudentDTO createStudentDTO(StudentPl studentPl)
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<StudentPl, StudentDTO>()).CreateMapper();
            return mapper.Map<StudentPl, StudentDTO>(studentPl);
        }
    }
}
