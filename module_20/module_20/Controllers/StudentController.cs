using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<StudentDTO> Post(StudentDTO student)
        {
            if (student == null)
                BadRequest();

            _db.Students.Create(student);
            _db.Save();
            return Ok(student);
        }

        // PUT: Student/5
        [HttpPut]
        public ActionResult<StudentDTO> Put(StudentDTO student)
        {
            if (student == null)
                BadRequest();

            if (!_db.Students.Find(s => s.Id == student.Id).Any())
                NotFound();

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
    }
}
