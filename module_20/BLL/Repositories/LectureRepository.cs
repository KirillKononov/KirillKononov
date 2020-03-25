using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.DataAccess;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.Repositories
{
    class LectureRepository : IRepository<Lecture>
    {
        private readonly DataBaseContext _db;
        private readonly ILogger _logger;

        public LectureRepository(DataBaseContext context, ILogger logger)
        {
            _db = context;
            _logger = logger;
        }

        public IEnumerable<Lecture> GetAll()
        {
            if (!_db.Lectures.Any())
            {
                _logger.LogError("There is no lectures in data base");
                throw new ValidationException("There is no lectures in data base");
            }

            return _db.Lectures;
        }

        public Lecture Get(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Lecture's id hasn't entered");
                throw new ValidationException("Lecture's id hasn't entered");
            }

            if (_db.Lectures.Find(id) == null)
            {
                _logger.LogError("There is no lecture in data base with this id");
                throw new ValidationException("There is no lecture in data base with this id");
            }

            return _db.Lectures.Find(id);
        }

        public void Create(Lecture item)
        {
            _db.Lectures.Add(item);
        }

        public void Update(Lecture item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Lecture> Find(Func<Lecture, bool> predicate)
        {
            return _db.Lectures
                .Include(l => l.Professor)
                .Where(predicate)
                .ToList();
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Lecture's id hasn't entered");
                throw new ValidationException("Lecture's id hasn't entered");
            }

            var lecture = _db.Lectures.Find(id);

            if (lecture != null)
                _db.Lectures.Remove(lecture);
        }
    }
}
