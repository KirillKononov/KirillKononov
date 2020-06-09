using System.Collections;
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
    public class HomeworkController : ControllerBase
    {
        private readonly IUnitOfWork _db;

        public HomeworkController(IUnitOfWork uow)
        {
            _db = uow;
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
        public async Task<ActionResult<HomeworkDTO>> Post(HomeworkPl homeworkPl)
        {
            if (homeworkPl == null)
                BadRequest();

            var homework = CreateHomeworkDTO(homeworkPl);
            await _db.Homework.CreateAsync(homework);
            await _db.SaveAsync();
            return Ok(homeworkPl);
        }

        // PUT: Homework
        [HttpPut]
        public async Task<ActionResult<HomeworkDTO>> Put(HomeworkPl homeworkPl)
        {
            if (homeworkPl == null)
                BadRequest();

            var homework = CreateHomeworkDTO(homeworkPl);
            if (!_db.Homework.Find(h => h.Id == homework.Id).Any())
                NotFound();

            await _db.Homework.UpdateAsync(homework);
            await _db.SaveAsync();
            return Ok(homeworkPl);
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

        private HomeworkDTO CreateHomeworkDTO(HomeworkPl homeworkPl)
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<HomeworkPl, HomeworkDTO>()).CreateMapper();
            return mapper.Map<HomeworkPl, HomeworkDTO>(homeworkPl);
        }
    }
}
