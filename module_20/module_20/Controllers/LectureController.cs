using System.Collections.Generic;
using System.Linq;
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
        public ActionResult<IEnumerable<LectureDTO>> Get()
        {
            return _db.Lectures.GetAll().ToList();
        }

        // GET: Lecture/5
        [HttpGet("{id}")]
        public ActionResult<LectureDTO> Get(int? id)
        {
            if (id == null)
                BadRequest();

            var lecture = _db.Lectures.Get(id);
            return new ObjectResult(lecture);
        }

        // POST: Lecture
        [HttpPost]
        public ActionResult<Lecture> Post(LecturePl lecturePl)
        {
            if (lecturePl == null)
                BadRequest();

            var lecture = CreateLectureDTO(lecturePl);
            _db.Lectures.Create(lecture);
            _db.Save();
            return Ok(lecture);
        }

        // PUT: Lecture
        [HttpPut]
        public ActionResult<LectureDTO> Put(LecturePl lecturePl)
        {
            if (lecturePl == null)
                BadRequest();

            if (!_db.Lectures.Find(l => l.Id == lecturePl.Id).ToList().Any()) 
                NotFound();

            var lecture = CreateLectureDTO(lecturePl);
            _db.Lectures.Update(lecture);
            _db.Save();

            return Ok(lecture);
        }

        // DELETE: Lecture/5
        [HttpDelete("{id}")]
        public ActionResult<Lecture> Delete(int? id)
        {
            if (id == null)
                BadRequest();

            var lecture = _db.Lectures.Get(id);
            _db.Lectures.Delete(id);
            _db.Save();
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
