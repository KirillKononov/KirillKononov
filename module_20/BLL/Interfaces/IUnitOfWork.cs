using System;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Student> Students { get; }
        IRepository<Professor> Professors { get; }
        IRepository<Lecture> Lectures { get; }
        IRepository<HomeWork> HomeWorks { get; }
        void Save();
    }
}