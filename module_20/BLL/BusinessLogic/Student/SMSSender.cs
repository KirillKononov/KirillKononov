using BLL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.BusinessLogic.Student
{
    public class SMSSender : IMessageSender
    {
        public void Send(DAL.Entities.Student student, ILogger logger = null)
        {
            logger.LogInformation($"Your average mark is {student.AverageMark}. Start doing something!");
        }
    }
}
