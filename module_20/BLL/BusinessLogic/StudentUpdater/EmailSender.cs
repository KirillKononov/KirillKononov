using System;
using BLL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.BusinessLogic.StudentUpdater
{
    public class EmailSender : IMessageSender
    {
        public void Send(DAL.Entities.Student student, ILogger logger = null)
        {
            logger.LogInformation($"{DateTime.UtcNow} Student {student.FirstName} {student.LastName} with id {student.Id}" +
                                  $" missed {student.MissedLectures} lectures");
        }
    }
}
