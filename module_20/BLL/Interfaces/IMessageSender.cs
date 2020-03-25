using DAL.Entities;
using Microsoft.Extensions.Logging;

namespace BLL.Interfaces
{
    public interface IMessageSender
    {
        void Send(Student student, ILogger logger);
    }
}
