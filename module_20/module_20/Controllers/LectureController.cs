using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using module_20.DTO;
using module_20.Interfaces;

namespace module_20.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LectureController : ControllerBase
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper; 

        public LectureController(IUnitOfWork uow, IMapperPL mapper)
        {
            _db = uow;
            _mapper = mapper.CreateMapper();
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
        public async Task<ActionResult<Lecture>> Post(LectureViewModel lectureViewModel)
        {
            if (lectureViewModel == null)
                BadRequest();

            var lecture =_mapper.Map<LectureDTO>(lectureViewModel);
            await _db.Lectures.CreateAsync(lecture);
            await _db.SaveAsync();
            return Ok(lectureViewModel);
        }

        // PUT: Lecture
        [HttpPut]
        public async Task<ActionResult<LectureDTO>> Put(LectureViewModel lectureViewModel)
        {
            if (lectureViewModel == null)
                BadRequest();

            if (!_db.Lectures.Find(l => l.Id == lectureViewModel.Id).Any()) 
                NotFound();

            var lecture = _mapper.Map<LectureDTO>(lectureViewModel);
            await _db.Lectures.UpdateAsync(lecture);
            await _db.SaveAsync();

            return Ok(lectureViewModel);
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
    }
}
