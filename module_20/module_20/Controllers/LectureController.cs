using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces.ServicesInterfaces;
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
        private readonly ILectureService _db;
        private readonly IMapper _mapper; 

        public LectureController(ILectureService lectureService, IMapperPL mapper)
        {
            _db = lectureService;
            _mapper = mapper.CreateMapper();
        }

        // GET: Lecture
        [HttpGet]
        public async Task<IEnumerable<LectureDTO>> Get()
        {
            return await _db.GetAllAsync();
        }

        // GET: Lecture/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LectureDTO>> Get(int? id)
        {
            if (id == null)
                BadRequest();

            var lecture = await _db.GetAsync(id);
            return new ObjectResult(lecture);
        }

        // POST: Lecture
        [HttpPost]
        public async Task<ActionResult<Lecture>> Post(LectureViewModel lectureViewModel)
        {
            if (lectureViewModel == null)
                BadRequest();

            var lecture =_mapper.Map<LectureDTO>(lectureViewModel);
            await _db.CreateAsync(lecture);
            return Ok(lectureViewModel);
        }

        // PUT: Lecture
        [HttpPut]
        public async Task<ActionResult<LectureDTO>> Put(LectureViewModel lectureViewModel)
        {
            if (lectureViewModel == null)
                BadRequest();

            if (!_db.Find(l => l.Id == lectureViewModel.Id).Any()) 
                NotFound();

            var lecture = _mapper.Map<LectureDTO>(lectureViewModel);
            await _db.UpdateAsync(lecture);

            return Ok(lectureViewModel);
        }

        // DELETE: Lecture/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lecture>> Delete(int? id)
        {
            if (id == null)
                BadRequest();

            var lecture = await _db.GetAsync(id);
            await _db.DeleteAsync(id);
            return Ok(lecture);
        }
    }
}
