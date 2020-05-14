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

            var mapper = new MapperConfiguration(cfg => 
                cfg.CreateMap<Lecture, LectureDTO>()).CreateMapper();
            return professors
                .Select(prof => 
                    new ProfessorDTO()
                    {
                        Id = prof.Id, 
                        FirstName = prof.FirstName, 
                        LastName = prof.LastName, 
                        Lectures = mapper.Map<IEnumerable<Lecture>, List<LectureDTO>>(prof.Lectures)
                    })
                .ToList();
        }

        public ProfessorDTO Get(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Professor's id hasn't entered");
                throw new ValidationException("Professor's id hasn't entered");
            }

            var prof = _db.Professors.Find(id);

            if (prof == null)
            {
                _logger.LogError("There is no professor in data base with this id");
                throw new ValidationException("There is no professor in data base with this id");
            }

            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Lecture, LectureDTO>()).CreateMapper();
            return new ProfessorDTO()
            {
                Id = prof.Id,
                FirstName = prof.FirstName, 
                LastName = prof.LastName,
                Lectures = mapper.Map<IEnumerable<Lecture>, List<LectureDTO>>(_db.Lectures
                    .Where(l => l.ProfessorId == prof.Id).ToList())
            };
        }

        public void Create(ProfessorDTO item)
        {
            var prof = new Professor() {FirstName = item.FirstName, LastName = item.LastName};
            _db.Professors.Add(prof);
        }

        public void Update(ProfessorDTO item)
        {
            var prof = _db.Professors.Find(item.Id);

            if (prof == null)
            {
                _logger.LogError("There is no professor in data base with this id");
                throw new ValidationException("There is no professor in data base with this id");
            }

            prof.FirstName = item.FirstName;
            prof.LastName = item.LastName;
            _db.Entry(prof).State = EntityState.Modified;
        }

        public IEnumerable<ProfessorDTO> Find(Func<Professor, bool> predicate)
        {
            var professors = _db.Professors.Where(predicate).ToList();
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Lecture, LectureDTO>()).CreateMapper();
            return professors
                .Select(prof =>
                    new ProfessorDTO()
                    {
                        Id = prof.Id,
                        FirstName = prof.FirstName,
                        LastName = prof.LastName,
                        Lectures = mapper.Map<IEnumerable<Lecture>, List<LectureDTO>>(prof.Lectures)
                    })
                .ToList();
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Professor's id hasn't entered");
                throw new ValidationException("Professor's id hasn't entered");
            }

            var prof = _db.Professors.Find(id);

            if (prof == null)
            {
                _logger.LogError("There is no professor in data base with this id");
                throw new ValidationException("There is no professor in data base with this id");
            }

            _db.Professors.Remove(prof);
        }
    }
}
