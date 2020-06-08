using System;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<StudentDTO, Student> Students { get; }
        IRepository<ProfessorDTO, Professor> Professors { get; }
        IRepository<LectureDTO, Lecture> Lectures { get; }
        IRepository<HomeworkDTO, Homework> Homework { get; }
        Task SaveAsync();
    }
}