using System;
using BLL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.BusinessLogic.Student
{
    public class SMSSender : IMessageSender
    {
        public void Send(DAL.Entities.Student student, ILogger logger = null)
        {
            logger.LogInformation($"{DateTime.UtcNow} Student {student.FirstName} {student.LastName} " +
                                  $"with id {student.Id} has an average mark {student.AverageMark}. Start doing something!");
        }
    }
}
