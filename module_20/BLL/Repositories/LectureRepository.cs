using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.DataAccess;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace BLL.Repositories
{
    class LectureRepository : IRepository<LectureDTO>
    {
        private readonly DataBaseContext _db;
        private readonly ILogger _logger;

        public LectureRepository(DataBaseContext context, ILogger logger)
        {
            _db = context;
            _logger = logger;
        }

        public IEnumerable<LectureDTO> GetAll()
        {
            if (!_db.Lectures.Any())
            {
                _logger.LogError("There is no lectures in data base");
                throw new ValidationException("There is no lectures in data base");
            }

            var mapper = new MapperConfiguration(cfg => 
                cfg.CreateMap<Lecture, LectureDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Lecture>, List<LectureDTO>>(_db.Lectures);
        }

        public LectureDTO Get(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Lecture's id hasn't entered");
                throw new ValidationException("Lecture's id hasn't entered");
            }

            var lecture = _db.Lectures.Find(id);

            if (lecture == null)
            {
                _logger.LogError("There is no lecture in data base with this id");
                throw new ValidationException("There is no lecture in data base with this id");
            }

            return new LectureDTO() {Id = lecture.Id, Name = lecture.Name, ProfessorId = lecture.ProfessorId};
        }

        public void Create(LectureDTO item)
        {
            var lecture = new Lecture() {Name =  item.Name, ProfessorId = item.ProfessorId};
            _db.Lectures.Add(lecture);
        }

        public void Update(LectureDTO item)
        {

            var lecture = _db.Lectures.Find(item.Id);

            if (lecture == null)
            {
                _logger.LogError("There is no lecture in data base with this id");
                throw new ValidationException("There is no lecture in data base with this id");
            }

            lecture.Name = item.Name;
            lecture.ProfessorId = item.ProfessorId;
            _db.Entry(lecture).State = EntityState.Modified;
        }

        public IEnumerable<LectureDTO> Find(Func<LectureDTO, bool> predicate)
        {
            var lectures = _db.Lectures
                .Where(predicate)
                .ToList();
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Lecture, LectureDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Lecture>, List<LectureDTO>>(lectures);
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Lecture's id hasn't entered");
                throw new ValidationException("Lecture's id hasn't entered");
            }

            var lecture = _db.Lectures.Find(id);

            if (lecture == null)
            {
                _logger.LogError("There is no lecture in data base with this id");
                throw new ValidationException("There is no lecture in data base with this id");
            }
            
            _db.Lectures.Remove(lecture);
        }
    }
}
