using BLL.BusinessLogic;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IStudentHomeworkUpdater
    {
        void Update(Homework homework, StudentHomeworkUpdater.UpdateType updateType);
    }
}