using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using module_20.DTO;

namespace module_20.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LectureController : ControllerBase
    {
        private readonly IUnitOfWork _db;

        public LectureController(IUnitOfWork uow)
        {
            _db = uow;
        }

        // GET: Lecture
        [HttpGet]
        public async Task<IEnumerable<LectureDTO>> Get()
        {
            return await _db.Lectures.GetAllAsync();
        }

        // GET: Lecture/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LectureDTO>> Get(int? id)
        {
            if (id == null)
                BadRequest();

            var lecture = await _db.Lectures.GetAsync(id);
            return new ObjectResult(lecture);
        }

        // POST: Lecture
        [HttpPost]
        public async Task<ActionResult<Lecture>> Post(LecturePl lecturePl)
        {
            if (lecturePl == null)
                BadRequest();

            var lecture = CreateLectureDTO(lecturePl);
            await _db.Lectures.CreateAsync(lecture);
            await _db.SaveAsync();
            return Ok(lecture);
        }

        // PUT: Lecture
        [HttpPut]
        public async Task<ActionResult<LectureDTO>> Put(LecturePl lecturePl)
        {
            if (lecturePl == null)
                BadRequest();

            if (!_db.Lectures.Find(l => l.Id == lecturePl.Id).Any()) 
                NotFound();

            var lecture = CreateLectureDTO(lecturePl);
            await _db.Lectures.UpdateAsync(lecture);
            await _db.SaveAsync();

            return Ok(lecture);
        }

        // DELETE: Lecture/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lecture>> Delete(int? id)
        {
            if (id == null)
                BadRequest();

            var lecture = await _db.Lectures.GetAsync(id);
            await _db.Lectures.DeleteAsync(id);
            await _db.SaveAsync();
            return Ok(lecture);
        }

        private LectureDTO CreateLectureDTO(LecturePl lecturePl)
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<LecturePl, LectureDTO>()).CreateMapper();
            return mapper.Map<LecturePl, LectureDTO>(lecturePl);
        }
    }
}
