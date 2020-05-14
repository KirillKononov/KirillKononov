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
    public class ProfessorController : ControllerBase
    {
        private readonly IUnitOfWork _db;

        public ProfessorController(IUnitOfWork uow)
        {
            _db = uow;
        }

        // GET: Professor
        [HttpGet]
        public ActionResult<IEnumerable<ProfessorDTO>> Get()
        {
            return _db.Professors.GetAll().ToList();
        }

        // GET: Professor/5
        [HttpGet("{id}")]
        public ActionResult<ProfessorDTO> Get(int? id)
        {
            if (id == null)
                BadRequest();

            var prof = _db.Professors.Get(id);
            return new ObjectResult(prof);
        }

        // POST: Professor
        [HttpPost]
        public ActionResult<ProfessorDTO> Post(ProfessorDTO prof)
        {
            if (prof == null)
                BadRequest();
            
            _db.Professors.Create(prof);
            _db.Save();
            return Ok(prof);
        }

        // PUT: Professor/5
        [HttpPut]
        public ActionResult<ProfessorDTO> Put(ProfessorDTO prof)
        {
            if (prof == null)
                BadRequest();

            if (!_db.Professors.Find(p => p.Id == prof.Id).ToList().Any())
                NotFound();

            _db.Professors.Update(prof);
            _db.Save();
            return Ok(prof);
        }

        // DELETE: Professor/5
        [HttpDelete("{id}")]
        public ActionResult<ProfessorDTO> Delete(int? id)
        {
            if (id == null)
                BadRequest();

            var prof = _db.Professors.Get(id);
            _db.Professors.Delete(id);
            _db.Save();
            return Ok(prof);
        }
    }
}
