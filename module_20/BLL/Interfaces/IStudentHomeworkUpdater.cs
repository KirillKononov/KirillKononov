using System.Threading.Tasks;
using BLL.BusinessLogic.Student;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IStudentHomeworkUpdater
    {
        Task UpdateAsync(Homework homework, StudentHomeworkUpdater.UpdateType updateType);
    }
}