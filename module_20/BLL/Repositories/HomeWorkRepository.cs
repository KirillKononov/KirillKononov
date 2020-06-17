using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.BusinessLogic.Student;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.DataAccess;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.Repositories
{
    class HomeworkRepository : IRepository<HomeworkDTO, Homework>
    {
        private readonly DataBaseContext _db;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public HomeworkRepository(DataBaseContext context, IMapper mapper, ILogger logger)
        {
            _db = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HomeworkDTO>> GetAllAsync()
        {
            var homework = await _db.Homework.ToListAsync();

            if (!homework.Any())
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

            var homework = await _db.Homework.FindAsync(id);

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
            await _db.Homework.AddAsync(homework);

            var studentHomeworkUpdater = new StudentHomeworkUpdater(_db, _logger);
            await studentHomeworkUpdater.UpdateAsync(homework, StudentHomeworkUpdater.UpdateType.AddHomework);
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

            var homework = await _db.Homework.FindAsync(item.Id);

            var validator = new Validator();
            validator.EntityValidation(homework, _logger, nameof(homework));

            var previousHomeworkPresence = homework.StudentPresence;
            var previousStudentId = homework.StudentId;
            var studentHomeworkUpdater = new StudentHomeworkUpdater(_db, _logger, previousHomeworkPresence);

            if (previousStudentId != item.StudentId)
                await studentHomeworkUpdater.UpdateAsync(homework, StudentHomeworkUpdater.UpdateType.RemoveHomework);

            homework.StudentId = item.StudentId;
            homework.LectureId = item.LectureId;
            homework.StudentPresence = item.StudentPresence;
            homework.HomeworkPresence = item.HomeworkPresence;
            homework.Mark = item.Mark;
            homework.Date = item.Date;
            _db.Entry(homework).State = EntityState.Modified;

            if (previousStudentId != homework.StudentId)
                await studentHomeworkUpdater.UpdateAsync(homework, StudentHomeworkUpdater.UpdateType.AddHomework);
            else
                await studentHomeworkUpdater.UpdateAsync(homework, StudentHomeworkUpdater.UpdateType.UpdateHomework);
        }

        public IEnumerable<HomeworkDTO> Find(Func<Homework, bool> predicate)
        {
            var homework = _db.Homework
                .Where(predicate)
                .ToList();
            return homework
                .Select(h => _mapper.Map<HomeworkDTO>(h));
        }

        public async Task DeleteAsync(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var homework = await _db.Homework.FindAsync(id);

            validator.EntityValidation(homework, _logger, nameof(homework));

            _db.Homework.Remove(homework);
            var studentHomeworkUpdater = new StudentHomeworkUpdater(_db, _logger);
            await studentHomeworkUpdater.UpdateAsync(homework, StudentHomeworkUpdater.UpdateType.RemoveHomework);
        }
    }
}
