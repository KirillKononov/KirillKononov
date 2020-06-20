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
        private readonly bool _previousPresence;

        public StudentHomeworkUpdater(IRepository<Student> studentRepository,
            ILogger logger, bool previousPresence = true)
        {
            _studentRepository = studentRepository;
            _logger = logger;
            _previousPresence = previousPresence;
        }

        public enum UpdateType
        {
            AddHomework,
            UpdateHomework,
            RemoveHomework
        }

        public async Task UpdateAsync(Homework homework, UpdateType updateType)
        {
            var student = await _studentRepository.GetAsync(homework.StudentId);

            var validator = new Validator();
            validator.EntityValidation(student, _logger, nameof(student));

            student.AverageMark = AverageMarkCount(student.StudentHomework, homework.Mark, updateType);

            student.MissedLectures = MissedLecturesCount(homework.StudentPresence, student.MissedLectures, updateType);

            _studentRepository.Update(student);

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
