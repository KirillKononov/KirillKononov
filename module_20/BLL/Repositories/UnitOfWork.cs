using System;
using BLL.DTO;
using BLL.Interfaces;
using DAL.DataAccess;
using DAL.Entities;
using Microsoft.Extensions.Logging;

namespace BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _db;
        private StudentRepository _studentRepository;
        private ProfessorRepository _professorRepository;
        private LectureRepository _lectureRepository;
        //private HomeWorkRepository _homeWorkRepository;

        private readonly ILogger _logger;

        public UnitOfWork(DataBaseContext db, ILogger<UnitOfWork> logger = null)
        {
            _db = db;
            _logger = logger;
        }

        public IRepository<StudentDTO,Student> Students => _studentRepository ?? 
                                                (_studentRepository = new StudentRepository(_db, _logger));

        public IRepository<ProfessorDTO, Professor> Professors => _professorRepository ?? 
                                                    (_professorRepository = new ProfessorRepository(_db, _logger));

        public IRepository<LectureDTO, Lecture> Lectures => _lectureRepository ?? 
                                                (_lectureRepository = new LectureRepository(_db, _logger));

        //public IRepository<HomeWork> HomeWorks => _homeWorkRepository ??
                                                  //    (_homeWorkRepository = new HomeWorkRepository(_db, _logger));

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true); 
            GC.SuppressFinalize(this);
        }
    }
}
