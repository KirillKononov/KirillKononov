using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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

        public ProfessorRepository(DataBaseContext context, ILogger logger)
        {
            _db = context;
            _logger = logger;
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
                .Select(CreateProfessorDto);
        }

        public ProfessorDTO Get(int? id)
        {
            Validator.IdValidation(id, _logger);

            var professor = _db.Professors.Find(id);

            Validator.EntityValidation(professor, _logger, nameof(professor));

            return CreateProfessorDto(professor);
        }

        public void Create(ProfessorDTO item)
        {
            var prof = new Professor() {FirstName = item.FirstName, LastName = item.LastName};
            _db.Professors.Add(prof);
        }

        public void Update(ProfessorDTO item)
        {
            var professor = _db.Professors.Find(item.Id);

            Validator.EntityValidation(professor, _logger, nameof(professor));

            professor.FirstName = item.FirstName;
            professor.LastName = item.LastName;
            _db.Entry(professor).State = EntityState.Modified;
        }

        public IEnumerable<ProfessorDTO> Find(Func<Professor, bool> predicate)
        {
            var professors = _db.Professors.Where(predicate).ToList();

            return professors
                .Select(CreateProfessorDto);
        }

        public void Delete(int? id)
        {
            Validator.IdValidation(id, _logger);

            var professor = _db.Professors.Find(id);

            Validator.EntityValidation(professor, _logger, nameof(professor));

            _db.Professors.Remove(professor);
        }

        private ProfessorDTO CreateProfessorDto(Professor professor)
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Lecture, LectureDTO>()).CreateMapper();
            return new ProfessorDTO()
            {
                Id = professor.Id,
                FirstName = professor.FirstName,
                LastName = professor.LastName,
                Lectures = mapper.Map<IEnumerable<Lecture>, List<LectureDTO>>(professor.Lectures)
            };
        }
    }
}
