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
    public class ProfessorController : ControllerBase
    {
        private readonly IUnitOfWork _db;

        public ProfessorController(IUnitOfWork uow)
        {
            _db = uow;
        }

        // GET: Professor
        [HttpGet]
        public async Task<IEnumerable<ProfessorDTO>> Get()
        {
            return await _db.Professors.GetAllAsync();
        }

        // GET: Professor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfessorDTO>> Get(int? id)
        {
            if (id == null)
                BadRequest();

            var prof = await _db.Professors.GetAsync(id);
            return new ObjectResult(prof);
        }

        // POST: Professor
        [HttpPost]
        public async Task<ActionResult<ProfessorDTO>> Post(ProfessorPl profPl)
        {
            if (profPl == null)
                BadRequest();

            var prof = createProfessorDTO(profPl);
            await _db.Professors.CreateAsync(prof);
            await _db.SaveAsync();
            return Ok(prof);
        }

        // PUT: Professor
        [HttpPut]
        public async Task<ActionResult<ProfessorDTO>> Put(ProfessorPl profPl)
        {
            if (profPl == null)
                BadRequest();

            if (!_db.Professors.Find(p => p.Id == profPl.Id).ToList().Any())
                NotFound();

            var prof = createProfessorDTO(profPl);
            await _db.Professors.UpdateAsync(prof);
            await _db.SaveAsync();
            return Ok(prof);
        }

        // DELETE: Professor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProfessorDTO>> Delete(int? id)
        {
            if (id == null)
                BadRequest();

            var prof = await _db.Professors.GetAsync(id);
            await _db.Professors.DeleteAsync(id);
            await _db.SaveAsync();
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
