using System;
using System.Collections;
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

        public async Task<IEnumerable<ProfessorDTO>> GetAllAsync()
        {
            var professors = await _db.Professors.ToListAsync();

            if (!professors.Any())
            {
                _logger.LogError("There is no professors in data base");
                throw new ValidationException("There is no professors in data base");
            }

            return professors
                .Select(p => _mapper.Map<ProfessorDTO>(p));
        }

        public async Task<ProfessorDTO> GetAsync(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var professor = await _db.Professors.FindAsync(id);

            validator.EntityValidation(professor, _logger, nameof(professor));

            return _mapper.Map<ProfessorDTO>(professor);
        }

        public async Task CreateAsync(ProfessorDTO item)
        {
            var prof = new Professor()
            {
                FirstName = item.FirstName, 
                LastName = item.LastName
            };
            await _db.Professors.AddAsync(prof);
        }

        public async Task UpdateAsync(ProfessorDTO item)
        {
            var professor = await _db.Professors.FindAsync(item.Id);

            var validator = new Validator();
            validator.EntityValidation(professor, _logger, nameof(professor));

            professor.FirstName = item.FirstName;
            professor.LastName = item.LastName;

            _db.Entry(professor).State = EntityState.Modified;
        }

        public IEnumerable<ProfessorDTO> Find(Func<Professor, bool> predicate)
        {
            var professors =_db.Professors
                .Where(predicate)
                .ToList();
            return professors
                .Select(p => _mapper.Map<ProfessorDTO>(p));
        }

        public async Task DeleteAsync(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var professor = await _db.Professors.FindAsync(id);

            validator.EntityValidation(professor, _logger, nameof(professor));

            _db.Professors.Remove(professor);
        }
    }
}
