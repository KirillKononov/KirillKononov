using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Interfaces.ServicesInterfaces;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace BLL.Repositories
{
    public class ProfessorService : IProfessorService
    {
        private readonly IRepository<Professor> _professorRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProfessorService(IRepository<Professor> repository, IMapperBLL mapper, ILoggerFactory factory)
        {
            _professorRepository = repository;
            _logger = factory.CreateLogger("Professor Service");
            _mapper = mapper.CreateMapper();
        }

        public async Task<IEnumerable<ProfessorDTO>> GetAllAsync()
        {
            var professors = await _professorRepository.GetAllAsync();

            if (!professors.Any())
            {
                _logger.LogWarning("There is no professors in data base");
                throw new ValidationException("There is no professors in data base");
            }

            return professors
                .Select(p => _mapper.Map<ProfessorDTO>(p));
        }

        public async Task<ProfessorDTO> GetAsync(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var professor = await _professorRepository.GetAsync(id);

            validator.EntityValidation(professor, _logger, nameof(professor));

            return _mapper.Map<ProfessorDTO>(professor);
        }

        public async Task CreateAsync(ProfessorDTO item)
        {
            var prof = _mapper.Map<Professor>(item);
            await _professorRepository.CreateAsync(prof);
        }

        public async Task UpdateAsync(ProfessorDTO item)
        {
            var professor = await _professorRepository.GetAsync(item.Id);

            var validator = new Validator();
            validator.EntityValidation(professor, _logger, nameof(professor));

            professor.FirstName = item.FirstName;
            professor.LastName = item.LastName;
            _professorRepository.Update(professor);
        }

        public IEnumerable<ProfessorDTO> Find(Func<Professor, bool> predicate)
        {
            var professors =_professorRepository
                .Find(predicate)
                .ToList();
            return professors
                .Select(p => _mapper.Map<ProfessorDTO>(p));
        }

        public async Task DeleteAsync(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var professor = await _professorRepository.GetAsync(id);

            validator.EntityValidation(professor, _logger, nameof(professor));

            _professorRepository.Delete(professor);
        }
    }
}
