using BLL.BusinessLogic.Student;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IStudentHomeworkUpdater
    {
        void Update(Homework homework, StudentHomeworkUpdater.UpdateType updateType);
    }
}