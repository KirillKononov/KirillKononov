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
    public class HomeworkController : ControllerBase
    {
        private readonly IHomeworkService _db;
        private readonly IMapper _mapper;

        public HomeworkController(IHomeworkService homeworkService, IMapperPL mapper)
        {
            _db = homeworkService;
            _mapper = mapper.CreateMapper();
        }

        // GET: Homework
        [HttpGet]
        public async Task<IEnumerable<HomeworkDTO>> Get()
        {
            return await _db.GetAllAsync();
        }

        // GET: Homework/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HomeworkDTO>> Get(int? id)
        {
            if (id == null)
                return BadRequest();

            var homework = await _db.GetAsync(id);
            return Ok(homework);
        }

        // POST: Homework
        [HttpPost]
        public async Task<ActionResult<HomeworkDTO>> Post(HomeworkViewModel homeworkViewModel)
        {
            if (homeworkViewModel == null)
                return BadRequest();

            var homework = _mapper.Map<HomeworkDTO>(homeworkViewModel);
            await _db.CreateAsync(homework);
            return Ok(homeworkViewModel);
        }

        // PUT: Homework
        [HttpPut]
        public async Task<ActionResult<HomeworkDTO>> Put(HomeworkViewModel homeworkViewModel)
        {
            if (homeworkViewModel == null)
                return BadRequest();

            var homework = _mapper.Map<HomeworkDTO>(homeworkViewModel);
            if (!_db.Find(h => h.Id == homework.Id).Any())
                return NotFound();

            await _db.UpdateAsync(homework);
            return Ok(homeworkViewModel);
        }

        // DELETE: Homework/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HomeworkDTO>> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var homework = await _db.GetAsync(id);
            await _db.DeleteAsync(id);
            return Ok(homework);
        }
    }
}
