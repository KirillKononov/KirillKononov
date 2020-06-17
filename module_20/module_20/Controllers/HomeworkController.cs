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
    public class HomeworkController : ControllerBase
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public HomeworkController(IUnitOfWork uow, IMapperPL mapper)
        {
            _db = uow;
            _mapper = mapper.CreateMapper();
        }

        // GET: Homework
        [HttpGet]
        public async Task<IEnumerable<HomeworkDTO>> Get()
        {
            return await _db.Homework.GetAllAsync();
        }

        // GET: Homework/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HomeworkDTO>> Get(int? id)
        {
            if (id == null)
                BadRequest();

            var homework = await _db.Homework.GetAsync(id);
            return new ObjectResult(homework);
        }

        // POST: Homework
        [HttpPost]
        public async Task<ActionResult<HomeworkDTO>> Post(HomeworkViewModel homeworkViewModel)
        {
            if (homeworkViewModel == null)
                BadRequest();

            var homework = _mapper.Map<HomeworkDTO>(homeworkViewModel);
            await _db.Homework.CreateAsync(homework);
            await _db.SaveAsync();
            return Ok(homeworkViewModel);
        }

        // PUT: Homework
        [HttpPut]
        public async Task<ActionResult<HomeworkDTO>> Put(HomeworkViewModel homeworkViewModel)
        {
            if (homeworkViewModel == null)
                BadRequest();

            var homework = _mapper.Map<HomeworkDTO>(homeworkViewModel);
            if (!_db.Homework.Find(h => h.Id == homework.Id).Any())
                NotFound();

            await _db.Homework.UpdateAsync(homework);
            await _db.SaveAsync();
            return Ok(homeworkViewModel);
        }

        // DELETE: Homework/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HomeworkDTO>> Delete(int? id)
        {
            if (id == null)
                BadRequest();

            var homework = await _db.Homework.GetAsync(id);
            await _db.Homework.DeleteAsync(id);
            await _db.SaveAsync();
            return Ok(homework);
        }
    }
}
