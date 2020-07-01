using System;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.Extensions.Logging;

namespace BLL.Services.StudentHomeworkUpdater
{
    public class EmailSender : IMessageSender
    {
        public void Send(Student student, ILogger logger = null)
        {
            logger?.LogInformation($"{DateTime.UtcNow} Student {student.FirstName} {student.LastName} " +
                                  $"with id {student.Id}" +
                                  $" missed {student.MissedLectures} lectures");
        }
    }
}
