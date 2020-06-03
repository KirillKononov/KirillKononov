using BLL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.BusinessLogic.Student
{
    public class EmailSender : IMessageSender
    {
        public void Send(DAL.Entities.Student student, ILogger logger = null)
        {
            logger.LogInformation($"Student {student.FirstName} {student.LastName} missed" +
                                  $" {student.MissedLectures} lectures");
        }
    }
}
