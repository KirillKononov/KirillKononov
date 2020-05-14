using System;
using BLL.DTO;
using BLL.Repositories;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<StudentDTO, Student> Students { get; }
        IRepository<ProfessorDTO, Professor> Professors { get; }
        IRepository<LectureDTO, Lecture> Lectures { get; }
        //IRepository<HomeWork> HomeWorks { get; }
        void Save();
    }
}