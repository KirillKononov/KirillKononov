using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<HomeworkDTO> GetAll()
        {
            var homework = _db.Homework.ToList();

            if (!homework.Any())
            {
                _logger.LogError("There is no homework in data base");
                throw new ValidationException("There is no homework in data base");
            }

            return homework
                .Select(h => _mapper.Map<HomeworkDTO>(h));
        }

        public HomeworkDTO Get(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var homework = _db.Homework.Find(id);

            validator.EntityValidation(homework, _logger, nameof(homework));

            return _mapper.Map<HomeworkDTO>(homework);
        }

        public void Create(HomeworkDTO item)
        {
            var homework = new Homework()
            {
                StudentId = item.StudentId,
                LectureId = item.LectureId,
                StudentPresence = item.StudentPresence,
                HomeworkPresence = item.StudentPresence && item.HomeworkPresence, 
                Mark = item.StudentPresence && item.HomeworkPresence ? item.Mark : 0,
                Date = item.Date
            };

            if (homework.HomeworkPresence && homework.Mark < 1)
            {
                _logger.LogWarning($"This mark {homework.Mark} is inappropriate. Must be at least 1 and at most 5");
                throw new ValidationException($"This mark {homework.Mark} is inappropriate. Must be at least 1 and at most 5");
            }

            _db.Homework.Add(homework);
            var studentHomeworkUpdater = new StudentHomeworkUpdater(_db, _logger);
            studentHomeworkUpdater.Update(homework, StudentHomeworkUpdater.UpdateType.AddHomework);
        }

        public void Update(HomeworkDTO item)
        {
            var homework = _db.Homework.Find(item.Id);
            var previousHomeworkPresence = homework.StudentPresence;

            var validator = new Validator();
            validator.EntityValidation(homework, _logger, nameof(homework));

            homework.StudentId = item.StudentId;
            homework.LectureId = item.LectureId;
            homework.StudentPresence = item.StudentPresence;
            homework.HomeworkPresence = item.StudentPresence && item.HomeworkPresence;
            homework.Mark = homework.HomeworkPresence ? item.Mark : 0;
            homework.Date = item.Date;

            if (homework.HomeworkPresence && homework.Mark < 1)
            {
                _logger.LogWarning($"This mark {homework.Mark} is inappropriate. Must be at least 1 and at most 5");
                throw new ValidationException($"This mark {homework.Mark} is inappropriate. Must be at least 1 and at most 5");
            }

            _db.Entry(homework).State = EntityState.Modified;
            var studentHomeworkUpdater = new StudentHomeworkUpdater(_db, _logger, previousHomeworkPresence);
            studentHomeworkUpdater.Update(homework, StudentHomeworkUpdater.UpdateType.UpdateHomework);
        }

        public IEnumerable<HomeworkDTO> Find(Func<Homework, bool> predicate)
        {
            var homework = _db.Homework
                .Where(predicate)
                .ToList();
            return homework
                .Select(h => _mapper.Map<HomeworkDTO>(h));
        }

        public void Delete(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var homework = _db.Homework.Find(id);

            validator.EntityValidation(homework, _logger, nameof(homework));

            _db.Homework.Remove(homework);
            var studentHomeworkUpdater = new StudentHomeworkUpdater(_db, _logger);
            studentHomeworkUpdater.Update(homework, StudentHomeworkUpdater.UpdateType.RemoveHomework);
        }
    }
}
