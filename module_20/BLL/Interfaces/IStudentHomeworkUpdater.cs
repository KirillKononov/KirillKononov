using System.Threading.Tasks;
using BLL.Services.StudentHomeworkUpdater;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IStudentHomeworkUpdater
    {
        Task UpdateAsync(Homework homework, StudentHomeworkUpdater.UpdateType updateType);
    }
}