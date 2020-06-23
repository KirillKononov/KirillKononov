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
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class LectureService : ILectureService
    {
        private readonly IRepository<Lecture> _lectureRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public LectureService(IRepository<Lecture> repository, IMapperBLL mapper, ILoggerFactory factory)
        {
            _lectureRepository = repository;
            _logger = factory.CreateLogger("Lecture Service");
            _mapper = mapper.CreateMapper();
        }

        public async Task<IEnumerable<LectureDTO>> GetAllAsync()
        {
            var lectures = await _lectureRepository.GetAllAsync();

            if (!lectures.Any())
            {
                _logger.LogWarning($"There are no lectures in database");
                throw new ValidationException($"There are no lectures in database");
            }

            return lectures
                .Select(l => _mapper.Map<LectureDTO>(l));
        }

        public async Task<LectureDTO> GetAsync(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var lecture = await _lectureRepository.GetAsync(id);

            validator.EntityValidation(lecture, _logger, nameof(lecture));

            return _mapper.Map<LectureDTO>(lecture);
        }

        public async Task CreateAsync(LectureDTO item)
        {
            var lecture = _mapper.Map<Lecture>(item);
            await _lectureRepository.CreateAsync(lecture);
        }

        public async Task UpdateAsync(LectureDTO item)
        {
            var lecture = await _lectureRepository.GetAsync(item.Id);

            var validator = new Validator();
            validator.EntityValidation(lecture, _logger, nameof(lecture));

            lecture.Name = item.Name;
            lecture.ProfessorId = item.ProfessorId;
            _lectureRepository.Update(lecture);
        }

        public IEnumerable<LectureDTO> Find(Func<Lecture, bool> predicate)
        {
            var lectures = _lectureRepository
                .Find(predicate)
                .ToList();
            return lectures
                .Select(l => _mapper.Map<LectureDTO>(l));
        }

        public async Task DeleteAsync(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var lecture = await _lectureRepository.GetAsync(id);
            validator.EntityValidation(lecture, _logger, nameof(lecture));

            _lectureRepository.Delete(id);
        }
    }
}
