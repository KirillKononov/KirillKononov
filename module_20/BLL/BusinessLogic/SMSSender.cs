using BLL.Interfaces;
using DAL.Entities;
using Microsoft.Extensions.Logging;

namespace BLL.BusinessLogic
{
    public class SMSSender : IMessageSender
    {
        public void Send(Student student, ILogger logger = null)
        {
            if (student.AverageMark < 4)
            {
                logger.LogInformation($"Your average mark is {student.AverageMark}. Start doing something!");
            }
        }
    }
}
