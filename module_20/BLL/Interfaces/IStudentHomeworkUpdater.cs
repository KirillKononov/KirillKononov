using BLL.BusinessLogic;
using DAL.DataAccess;
using DAL.Entities;
using Microsoft.Extensions.Logging;

namespace BLL.Interfaces
{
    public interface IStudentHomeworkUpdater
    {
        void Update(Homework homework, StudentHomeworkUpdater.UpdateType updateType);
    }
}