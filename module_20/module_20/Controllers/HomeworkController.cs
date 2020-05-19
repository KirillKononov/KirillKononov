using System.Collections.Generic;
using System.Linq;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<HomeworkDTO> Post(HomeworkDTO homework)
        {
            if (homework == null)
                BadRequest();

            _db.Homework.Create(homework);
            _db.Save();
            return Ok(homework);
        }

        // PUT: Homework/5
        [HttpPut]
        public ActionResult<HomeworkDTO> Put(HomeworkDTO homework)
        {
            if (homework == null)
                BadRequest();

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
    }
}
