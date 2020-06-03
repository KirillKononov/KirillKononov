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
    public class HomeworkController : ControllerBase
    {
        private readonly IUnitOfWork _db;

        public HomeworkController(IUnitOfWork uow)
        {
            _db = uow;
        }

        // GET: Homework
        [HttpGet]
        public ActionResult<IEnumerable<HomeworkDTO>> Get()
        {
            return _db.Homework.GetAll().ToList();
        }

        // GET: Homework/5
        [HttpGet("{id}")]
        public ActionResult<HomeworkDTO> Get(int? id)
        {
            if (id == null)
                BadRequest();

            var homework = _db.Homework.Get(id);
            return new ObjectResult(homework);
        }

        // POST: Homework
        [HttpPost]
        public ActionResult<HomeworkDTO> Post(HomeworkPl homeworkPl)
        {
            if (homeworkPl == null)
                BadRequest();

            var homework = CreateHomeworkDTO(homeworkPl);
            _db.Homework.Create(homework);
            _db.Save();
            return Ok(homework);
        }

        // PUT: Homework
        [HttpPut]
        public ActionResult<HomeworkDTO> Put(HomeworkPl homeworkPl)
        {
            if (homeworkPl == null)
                BadRequest();

            var homework = CreateHomeworkDTO(homeworkPl);
            if (!_db.Homework.Find(h => h.Id == homework.Id).Any())
                NotFound();

            _db.Homework.Update(homework);
            _db.Save();
            return Ok(homework);
        }

        // DELETE: Homework/5
        [HttpDelete("{id}")]
        public ActionResult<HomeworkDTO> Delete(int? id)
        {
            if (id == null)
                BadRequest();

            var homework = _db.Homework.Get(id);
            _db.Homework.Delete(id);
            _db.Save();
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
