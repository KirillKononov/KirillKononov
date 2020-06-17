using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using module_20.DTO;
using module_20.Interfaces;

namespace module_20.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper; 

        public ProfessorController(IUnitOfWork uow, IMapperPL mapper)
        {
            _db = uow;
            _mapper = mapper.CreateMapper();
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
        public async Task<ActionResult<ProfessorDTO>> Post(ProfessorViewModel profViewModel)
        {
            if (profViewModel == null)
                BadRequest();

            var prof = _mapper.Map<ProfessorDTO>(profViewModel);
            await _db.Professors.CreateAsync(prof);
            await _db.SaveAsync();
            return Ok(profViewModel);
        }

        // PUT: Professor
        [HttpPut]
        public async Task<ActionResult<ProfessorDTO>> Put(ProfessorViewModel profViewModel)
        {
            if (profViewModel == null)
                BadRequest();

            if (!_db.Professors.Find(p => p.Id == profViewModel.Id).ToList().Any())
                NotFound();

            var prof = _mapper.Map<ProfessorDTO>(profViewModel);
            await _db.Professors.UpdateAsync(prof);
            await _db.SaveAsync();
            return Ok(profViewModel);
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
    }
}
