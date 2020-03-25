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
    class ProfessorRepository : IRepository<Professor>
    {
        private readonly DataBaseContext _db;
        private readonly ILogger _logger;
        public ProfessorRepository(DataBaseContext context, ILogger logger)
        {
            _db = context;
            _logger = logger;
        }

        public IEnumerable<Professor> GetAll()
        {
            if (!_db.Professors.Any())
            {
                _logger.LogError("There is no professors in data base");
                throw new ValidationException("There is no professors in data base");
            }

            return _db.Professors;
        }

        public Professor Get(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Professor's id hasn't entered");
                throw new ValidationException("Professor's id hasn't entered");
            }

            if (_db.Professors.Find(id) == null)
            {
                _logger.LogError("There is no professor in data base with this id");
                throw new ValidationException("There is no professor in data base with this id");
            }

            return _db.Professors.Find(id);
        }

        public void Create(Professor item)
        {
            _db.Professors.Add(item);
        }

        public void Update(Professor item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Professor> Find(Func<Professor, bool> predicate)
        {
            return _db.Professors.Where(predicate).ToList();
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Professor's id hasn't entered");
                throw new ValidationException("Professor's id hasn't entered");
            }

            var professor = _db.Professors.Find(id);

            if (professor != null)
                _db.Professors.Remove(professor);
        }
    }
}
