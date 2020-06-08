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

        public async Task<IEnumerable<LectureDTO>> GetAllAsync()
        {
            var lectures = await _db.Lectures.ToListAsync();

            if (!lectures.Any())
            {
                _logger.LogError($"There are no lectures in database");
                throw new ValidationException($"There are no lectures in database");
            }

            return lectures
                .Select(l => _mapper.Map<LectureDTO>(l));
        }

        public async Task<LectureDTO> GetAsync(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var lecture = await _db.Lectures.FindAsync(id);

            validator.EntityValidation(lecture, _logger, nameof(lecture));

            return _mapper.Map<LectureDTO>(lecture);
        }

        public async Task CreateAsync(LectureDTO item)
        {
            var lecture = new Lecture()
            {
                Name =  item.Name,
                ProfessorId = item.ProfessorId
            };
            await _db.Lectures.AddAsync(lecture);
        }

        public async Task UpdateAsync(LectureDTO item)
        {

            var lecture = await _db.Lectures.FindAsync(item.Id);

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

        public async Task DeleteAsync(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var lecture = await _db.Lectures.FindAsync(id);

            validator.EntityValidation(lecture, _logger, nameof(lecture));

            _db.Lectures.Remove(lecture);
        }
    }
}
