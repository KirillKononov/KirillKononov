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

        public HomeworkRepository(DataBaseContext context, ILogger logger)
        {
            _db = context;
            _logger = logger;
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
                .Select(CreateHomeworkDTO);
        }

        public HomeworkDTO Get(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var homework = _db.Homework.Find(id);

            validator.EntityValidation(homework, _logger, nameof(homework));

            return CreateHomeworkDTO(homework);
        }

        public void Create(HomeworkDTO item)
        {
            var homework = new Homework()
            {
                StudentId = item.StudentId,
                LectureId = item.LectureId,
                Presence = item.Presence,
                Mark = item.Presence ? item.Mark : 0,
                Date = item.Date
            };
            _db.Homework.Add(homework);
            IStudentHomeworkUpdater studentHomeworkUpdater = new StudentHomeworkUpdater(_db, _logger);
            studentHomeworkUpdater.Update(homework, StudentHomeworkUpdater.UpdateType.AddHomework);
        }

        public void Update(HomeworkDTO item)
        {
            var homework = _db.Homework.Find(item.Id);
            var previousHomeworkPresence = homework.Presence;

            var validator = new Validator();
            validator.EntityValidation(homework, _logger, nameof(homework));

            homework.StudentId = item.StudentId;
            homework.LectureId = item.LectureId;
            homework.Presence = item.Presence;
            homework.Mark = item.Presence ? item.Mark : 0;
            homework.Date = item.Date;

            _db.Entry(homework).State = EntityState.Modified;
            IStudentHomeworkUpdater studentHomeworkUpdater = new StudentHomeworkUpdater(_db, _logger, previousHomeworkPresence);
            studentHomeworkUpdater.Update(homework, StudentHomeworkUpdater.UpdateType.UpdateHomework);
        }

        public IEnumerable<HomeworkDTO> Find(Func<Homework, bool> predicate)
        {
            var homework = _db.Homework
                .Where(predicate)
                .ToList();
            return homework
                .Select(CreateHomeworkDTO);
        }

        public void Delete(int? id)
        {
            var validator = new Validator();
            validator.IdValidation(id, _logger);

            var homework = _db.Homework.Find(id);

            validator.EntityValidation(homework, _logger, nameof(homework));

            _db.Homework.Remove(homework);
            IStudentHomeworkUpdater studentHomeworkUpdater = new StudentHomeworkUpdater(_db, _logger);
            studentHomeworkUpdater.Update(homework, StudentHomeworkUpdater.UpdateType.RemoveHomework);
        }

        private static HomeworkDTO CreateHomeworkDTO(Homework homework)
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Homework, HomeworkDTO>()).CreateMapper();
            return mapper.Map<Homework, HomeworkDTO>(homework);
        }
    }
}
