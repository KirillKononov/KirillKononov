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
        public ActionResult<ProfessorDTO> Post(ProfessorPl profPl)
        {
            if (profPl == null)
                BadRequest();

            var prof = createProfessorDTO(profPl);
            _db.Professors.Create(prof);
            _db.Save();
            return Ok(prof);
        }

        // PUT: Professor
        [HttpPut]
        public ActionResult<ProfessorDTO> Put(ProfessorPl profPl)
        {
            if (profPl == null)
                BadRequest();

            if (!_db.Professors.Find(p => p.Id == profPl.Id).ToList().Any())
                NotFound();

            var prof = createProfessorDTO(profPl);
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

        private ProfessorDTO createProfessorDTO(ProfessorPl professorPl)
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<ProfessorPl, ProfessorDTO>()).CreateMapper();
            return mapper.Map<ProfessorPl, ProfessorDTO>(professorPl);
        }
    }
}
