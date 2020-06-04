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
    class LectureRepository : IRepository<LectureDTO, Lecture>
    {
        private readonly DataBaseContext _db;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public LectureRepository(DataBaseContext context, IMapper mapper, ILogger logger)
        {
            _db = context;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<LectureDTO> GetAll()
        {
            var lectures = _db.Lectures.ToList();

            if (!lectures.Any())
            {
                _logger.LogError($"There are no lectures in database");
                throw new ValidationException($"There are no lectures in database");
            }

            return lectures
                .Select(l => _mapper.Map<LectureDTO>(l));
        }

        public LectureDTO Get(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var lecture = _db.Lectures.Find(id);

            validator.EntityValidation(lecture, _logger, nameof(lecture));

            return _mapper.Map<LectureDTO>(lecture);
        }

        public void Create(LectureDTO item)
        {
            var lecture = new Lecture()
            {
                Name =  item.Name,
                ProfessorId = item.ProfessorId
            };
            _db.Lectures.Add(lecture);
        }

        public void Update(LectureDTO item)
        {

            var lecture = _db.Lectures.Find(item.Id);

            var validator = new Validator();
            validator.EntityValidation(lecture, _logger, nameof(lecture));

            lecture.Name = item.Name;
            lecture.ProfessorId = item.ProfessorId;

            _db.Entry(lecture).State = EntityState.Modified;
        }

        public IEnumerable<LectureDTO> Find(Func<Lecture, bool> predicate)
        {
            var lectures = _db.Lectures
                .Where(predicate)
                .ToList();
            return lectures
                .Select(l => _mapper.Map<LectureDTO>(l));
        }

        public void Delete(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var lecture = _db.Lectures.Find(id);

            validator.EntityValidation(lecture, _logger, nameof(lecture));

            _db.Lectures.Remove(lecture);
        }
    }
}
