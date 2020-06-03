using System.Collections.Generic;
using System.Linq;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.DataAccess;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.BusinessLogic.Student
{
    public class StudentHomeworkUpdater : IStudentHomeworkUpdater
    {
        private readonly DataBaseContext _db;
        private readonly ILogger _logger;
        private readonly bool _previousPresence;

        public StudentHomeworkUpdater(DataBaseContext db , ILogger logger, bool previousPresence = true)
        {
            _db = db;
            _logger = logger;
            _previousPresence = previousPresence;
        }

        public enum UpdateType
        {
            AddHomework,
            UpdateHomework,
            RemoveHomework
        }

        public void Update(Homework homework, UpdateType updateType)
        {
            var student = _db.Students.Find(homework.StudentId);

            var validator = new Validator();
            validator.EntityValidation(student, _logger, nameof(student));

            student.AverageMark = AverageMarkCount(student.StudentHomework, homework.Mark, updateType);

            student.MissedLectures = MissedLecturesCount(homework.Presence, student.MissedLectures, updateType);

            _db.Entry(student).State = EntityState.Modified;

            if(updateType == UpdateType.AddHomework || updateType == UpdateType.UpdateHomework)
                SendMessage(student, _logger);
        }

        private float AverageMarkCount(IReadOnlyCollection<Homework> studentHomework,int mark, UpdateType updateType)
        {
            float marks = studentHomework.Sum(work => work.Mark);
            return updateType == UpdateType.RemoveHomework ? 
                (marks - mark) / (studentHomework.Count - 1) : marks / studentHomework.Count;
        }

        private int MissedLecturesCount(bool presence, int missedLectures, UpdateType updateType)
        {
            if (updateType == UpdateType.UpdateHomework)
            {
                if (!_previousPresence && presence)
                    return missedLectures - 1;

                if (_previousPresence && !presence)
                    return missedLectures + 1;
            }
            
            if (!presence)
                return updateType == UpdateType.AddHomework ? 
                    missedLectures + 1 : missedLectures - 1;

            return missedLectures;
        }

        private void SendMessage(DAL.Entities.Student student, ILogger logger)
        {
            IMessageSender message = new SMSSender();
            if (student.AverageMark < 4)
                message.Send(student, logger);

            if (student.MissedLectures > 3)
            {
                message = new EmailSender();
                message.Send(student, logger);
            }
        }
    }
}
