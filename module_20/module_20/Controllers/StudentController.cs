using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<StudentDTO>> Get()
        {
            return await _db.Students.GetAllAsync();
        }

        // GET: Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> Get(int? id)
        {
            if (id == null)
                BadRequest();

            var student = await _db.Students.GetAsync(id);
            return new ObjectResult(student);
        }

        // POST: Student
        [HttpPost]
        public async Task<ActionResult<StudentDTO>> Post(StudentPl studentPl)
        {
            if (studentPl == null)
                BadRequest();

            var student = createStudentDTO(studentPl);
            await _db.Students.CreateAsync(student);
            await _db.SaveAsync();
            return Ok(studentPl);
        }

        // PUT: Student
        [HttpPut]
        public async Task<ActionResult<StudentDTO>> Put(StudentPl studentPl)
        {
            if (studentPl == null)
                BadRequest();

            if (!_db.Students.Find(s => s.Id == studentPl.Id).Any())
                NotFound();

            var student = createStudentDTO(studentPl);
            await _db.Students.UpdateAsync(student);
            await _db.SaveAsync();
            return Ok(studentPl);
        }

        // DELETE: Student/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentDTO>> Delete(int? id)
        {
            if (id == null)
                BadRequest();

            var student = await _db.Students.GetAsync(id);
            await _db.Students.DeleteAsync(id);
            await _db.SaveAsync();
            return Ok(student);
        }
    }
}
