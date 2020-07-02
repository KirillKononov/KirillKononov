using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.Services.StudentHomeworkUpdater
{
    public class StudentHomeworkUpdater : IStudentHomeworkUpdater
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly ILogger _logger;
        private readonly IMessageSender _smsMessageSender;
        private readonly IMessageSender _emailMessageSender;

        public StudentHomeworkUpdater(IRepository<Student> studentRepository, 
            Func<string, IMessageSender> messageSender, 
            ILoggerFactory factory = null)
        {
            _studentRepository = studentRepository;
            _smsMessageSender = messageSender.Invoke("SMS");
            _emailMessageSender = messageSender.Invoke("Email");
            _logger = factory?.CreateLogger("Student Homework Updater");
        }

        public enum UpdateType
        {
            AddHomework,
            UpdateHomework,
            RemoveHomeworkWhileUpdate,
            RemoveHomework
        }

        public async Task UpdateAsync(Homework homework, UpdateType updateType, bool previousPresence = true)
        {
            var student = await _studentRepository.GetAsync(homework.StudentId);

            var validator = new Validator();
            validator.EntityValidation(student, _logger, nameof(student));

            student.AverageMark = AverageMarkCount(student.StudentHomework, homework.Mark, updateType);

            student.MissedLectures = MissedLecturesCount(homework.StudentPresence, previousPresence,
                student.MissedLectures, updateType);

            _studentRepository.Update(student);

            if(updateType == UpdateType.AddHomework || updateType == UpdateType.UpdateHomework)
                SendMessage(student);
        }

        private float AverageMarkCount(IReadOnlyCollection<Homework> studentHomework, int mark, 
            UpdateType updateType)
        {
            float marks = studentHomework.Sum(work => work.Mark);
            return updateType == UpdateType.RemoveHomeworkWhileUpdate ? 
                (marks - mark) / (studentHomework.Count - 1) : marks / studentHomework.Count;
            
        }

        private int MissedLecturesCount(bool presence, bool previousPresence,
            int missedLectures, UpdateType updateType)
        {
            if (updateType == UpdateType.UpdateHomework)
            {
                if (!previousPresence && presence)
                    return missedLectures - 1;

                if (previousPresence && !presence)
                    return missedLectures + 1;
            }
            
            if (!presence)
                return updateType == UpdateType.AddHomework ? 
                    missedLectures + 1 : missedLectures - 1;

            return missedLectures;
        }

        private void SendMessage(Student student)
        {
            if (student.AverageMark < 4)
                _smsMessageSender.Send(student, _logger);

            if (student.MissedLectures > 3)
            {
                _emailMessageSender.Send(student, _logger);
                _emailMessageSender.Send(student, _logger);
            }
                
        }
    }
}
