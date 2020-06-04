using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.DataAccess;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.Repositories
{
    public class ProfessorRepository : IRepository<ProfessorDTO, Professor>
    {
        private readonly DataBaseContext _db;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProfessorRepository(DataBaseContext context, IMapper mapper, ILogger logger)
        {
            _db = context;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<ProfessorDTO> GetAll()
        {
            var professors = _db.Professors.ToList();

            if (!professors.Any())
            {
                _logger.LogError("There is no professors in data base");
                throw new ValidationException("There is no professors in data base");
            }

            return professors
                .Select(p => _mapper.Map<ProfessorDTO>(p));
        }

        public ProfessorDTO Get(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var professor = _db.Professors.Find(id);

            validator.EntityValidation(professor, _logger, nameof(professor));

            return _mapper.Map<ProfessorDTO>(professor);
        }

        public void Create(ProfessorDTO item)
        {
            var prof = new Professor()
            {
                FirstName = item.FirstName, 
                LastName = item.LastName
            };
            _db.Professors.Add(prof);
        }

        public void Update(ProfessorDTO item)
        {
            var professor = _db.Professors.Find(item.Id);

            var validator = new Validator();
            validator.EntityValidation(professor, _logger, nameof(professor));

            professor.FirstName = item.FirstName;
            professor.LastName = item.LastName;

            _db.Entry(professor).State = EntityState.Modified;
        }

        public IEnumerable<ProfessorDTO> Find(Func<Professor, bool> predicate)
        {
            var professors = _db.Professors.Where(predicate).ToList();

            return professors
                .Select(p => _mapper.Map<ProfessorDTO>(p));
        }

        public void Delete(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var professor = _db.Professors.Find(id);

            validator.EntityValidation(professor, _logger, nameof(professor));

            _db.Professors.Remove(professor);
        }
    }
}
