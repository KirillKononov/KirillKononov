using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.DataAccess;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.BusinessLogic
{
    public class StudentHomeworkUpdater : IStudentHomeworkUpdater
    {
        private readonly DataBaseContext _db;
        private readonly ILogger _logger;

        public StudentHomeworkUpdater(DataBaseContext db, ILogger logger)
        {
            _db = db;
            _logger = logger;
        }

        public enum UpdateType
        {
            AddHomework,
            RemoveHomework
        }

        public void Update(Homework homework, UpdateType updateType)
        {
            var student = _db.Students.Find(homework.StudentId);

            var validator = new Validator();
            validator.EntityValidation(student, _logger, nameof(student));

            student.AverageMark = AverageMarkCount(student.StudentHomework);

            if (!homework.Presence)
                student.MissedLectures = updateType == UpdateType.AddHomework ? 
                    student.MissedLectures + 1 : student.MissedLectures - 1;

            _db.Entry(student).State = EntityState.Modified;

            if(updateType == UpdateType.AddHomework)
                SendMessage(student, _logger);
        }

        private float AverageMarkCount(IReadOnlyCollection<Homework> studentHomework)
        {
            float marks = studentHomework.Sum(work => work.Mark);
            return marks / studentHomework.Count;
            //return updateType == UpdateType.AddHomework ? 
            //    (marks + mark) / (studentHomework.Count + 1) : (marks - mark) / (studentHomework.Count - 1);
        }

        private void SendMessage(Student student, ILogger logger)
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
