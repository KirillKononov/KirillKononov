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
    public class HomeworkService : IHomeworkService
    {
        private readonly IRepository<Homework> _homeworkRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public HomeworkService(IRepository<Homework> homeworkRepository, IRepository<Student> studentRepository,
            IMapperBLL mapper, ILoggerFactory factory)
        {
            _homeworkRepository = homeworkRepository;
            _studentRepository = studentRepository;
            _logger = factory.CreateLogger("Homework Service");
            _mapper = mapper.CreateMapper();
        }

        public async Task<IEnumerable<HomeworkDTO>> GetAllAsync()
        {
            var homework = await _homeworkRepository.GetAllAsync();

            if (!homework.ToList().Any())
            {
                _logger.LogWarning("There is no homework in data base");
                throw new ValidationException("There is no homework in data base");
            }

            return homework
                .Select(h => _mapper.Map<HomeworkDTO>(h));
        }

        public async Task<HomeworkDTO> GetAsync(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var homework = await _homeworkRepository.GetAsync(id);

            validator.EntityValidation(homework, _logger, nameof(homework));

            return _mapper.Map<HomeworkDTO>(homework);
        }

        public async Task CreateAsync(HomeworkDTO item)
        {
            if (item.StudentPresence && item.HomeworkPresence && item.Mark < 1)
            {
                _logger.LogWarning($"This mark {item.Mark} is inappropriate. Must be at least 1 and at most 5");
                throw new ValidationException($"This mark {item.Mark} is inappropriate. Must be at least 1 and at most 5");
            }

            if ((!item.StudentPresence || !item.HomeworkPresence) && item.Mark > 0)
            {
                _logger.LogWarning($"This mark {item.Mark} is inappropriate. Must be 0");
                throw new ValidationException($"This mark {item.Mark} is inappropriate. Must be 0");
            }

            var homework = _mapper.Map<Homework>(item);
            await _homeworkRepository.CreateAsync(homework);

            var studentHomeworkUpdater = new StudentHomeworkUpdater.StudentHomeworkUpdater(_studentRepository, _logger);
            await studentHomeworkUpdater.UpdateAsync(homework, StudentHomeworkUpdater.StudentHomeworkUpdater.UpdateType.AddHomework);
        }

        public async Task UpdateAsync(HomeworkDTO item)
        {
            if (item.StudentPresence && item.HomeworkPresence && item.Mark < 1)
            {
                _logger.LogWarning($"This mark {item.Mark} is inappropriate. Must be at least 1 and at most 5");
                throw new ValidationException($"This mark {item.Mark} is inappropriate. Must be at least 1 and at most 5");
            }

            if ((!item.StudentPresence || !item.HomeworkPresence) && item.Mark > 0)
            {
                _logger.LogWarning($"This mark {item.Mark} is inappropriate. Must be 0");
                throw new ValidationException($"This mark {item.Mark} is inappropriate. Must be 0");
            }

            var homework = await _homeworkRepository.GetAsync(item.Id);

            var validator = new Validator();
            validator.EntityValidation(homework, _logger, nameof(homework));

            var previousHomeworkPresence = homework.StudentPresence;
            var previousStudentId = homework.StudentId;
            var studentHomeworkUpdater = new StudentHomeworkUpdater.StudentHomeworkUpdater(_studentRepository, _logger, previousHomeworkPresence);

            if (previousStudentId != item.StudentId)
                await studentHomeworkUpdater.UpdateAsync(homework, StudentHomeworkUpdater.StudentHomeworkUpdater.UpdateType.RemoveHomework);

            homework.StudentId = item.StudentId;
            homework.LectureId = item.LectureId;
            homework.StudentPresence = item.StudentPresence;
            homework.HomeworkPresence = item.HomeworkPresence;
            homework.Mark = item.Mark;
            homework.Date = item.Date;
            _homeworkRepository.Update(homework);

            if (previousStudentId != homework.StudentId)
                await studentHomeworkUpdater.UpdateAsync(homework, StudentHomeworkUpdater.StudentHomeworkUpdater.UpdateType.AddHomework);
            else
                await studentHomeworkUpdater.UpdateAsync(homework, StudentHomeworkUpdater.StudentHomeworkUpdater.UpdateType.UpdateHomework);
        }

        public IEnumerable<HomeworkDTO> Find(Func<Homework, bool> predicate)
        {
            var homework = _homeworkRepository
                .Find(predicate)
                .ToList();
            return homework
                .Select(h => _mapper.Map<HomeworkDTO>(h));
        }

        public async Task DeleteAsync(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var homework = await _homeworkRepository.GetAsync(id);
            validator.EntityValidation(homework, _logger, nameof(homework));

            _homeworkRepository.Delete(id);
            var studentHomeworkUpdater = new StudentHomeworkUpdater.StudentHomeworkUpdater(_studentRepository, _logger);
            await studentHomeworkUpdater.UpdateAsync(homework, StudentHomeworkUpdater.StudentHomeworkUpdater.UpdateType.RemoveHomework);
        }
    }
}
