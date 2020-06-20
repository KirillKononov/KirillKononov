using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;
using module_20.DTO;
using module_20.Interfaces;

namespace module_20.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _db;
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService, IMapperPL mapper)
        {
            _db = studentService;
            _mapper = mapper.CreateMapper();
        }

        // GET: Student
        [HttpGet]
        public async Task<IEnumerable<StudentDTO>> Get()
        {
            return await _db.GetAllAsync();
        }

        // GET: Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> Get(int? id)
        {
            if (id == null)
                BadRequest();

            var student = await _db.GetAsync(id);
            return new ObjectResult(student);
        }

        // POST: Student
        [HttpPost]
        public async Task<ActionResult<StudentDTO>> Post(StudentViewModel studentViewModel)
        {
            if (studentViewModel == null)
                BadRequest();

            var student = _mapper.Map<StudentDTO>(studentViewModel);
            await _db.CreateAsync(student);
            return Ok(studentViewModel);
        }

        // PUT: Student
        [HttpPut]
        public async Task<ActionResult<StudentDTO>> Put(StudentViewModel studentViewModel)
        {
            if (studentViewModel == null)
                BadRequest();

            if (!_db.Find(s => s.Id == studentViewModel.Id).Any())
                NotFound();

            var student = _mapper.Map<StudentDTO>(studentViewModel);
            await _db.UpdateAsync(student);
            return Ok(studentViewModel);
        }

        // DELETE: Student/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentDTO>> Delete(int? id)
        {
            if (id == null)
                BadRequest();

            var student = await _db.GetAsync(id);
            await _db.DeleteAsync(id);
            return Ok(student);
        }
    }
}
