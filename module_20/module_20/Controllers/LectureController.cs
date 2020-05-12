using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.DataAccess;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<Lecture>> Get()
        {
            return _db.Lectures.GetAll().ToList();
        }

        // GET: Lecture/5
        [HttpGet("{id}")]
        public ActionResult<Lecture> Get(int id)
        {
            var lecture = _db.Lectures.Get(id);
            return new ObjectResult(lecture);
        }

        // POST: Lecture
        [HttpPost]
        public ActionResult<Lecture> Post(Lecture lecture)
        {
            if (lecture == null)
                BadRequest();

            _db.Lectures.Create(lecture);
            _db.Save();

            return Ok(lecture);
        }

        // PUT: Lecture/5
        [HttpPut("{id}")]
        public ActionResult<Lecture> Put(Lecture lecture)
        {
            if (lecture == null)
                BadRequest();

            if (!_db.Lectures.Find(l => l.Id == lecture.Id).Any())
                return NotFound();

            _db.Lectures.Update(lecture);
            _db.Save();

            return Ok(lecture);
        }

        // DELETE: Lecture/5
        [HttpDelete("{id}")]
        public ActionResult<Lecture> Delete(int id)
        {
            _db.Lectures.Delete(id);
            _db.Save();
            return Ok();
        }
    }
}
