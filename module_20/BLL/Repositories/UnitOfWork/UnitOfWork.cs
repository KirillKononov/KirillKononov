using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.DataAccess;
using DAL.Entities;
using Microsoft.Extensions.Logging;

namespace BLL.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _db;
        private readonly IMapper _mapper;
        private StudentRepository _studentRepository;
        private ProfessorRepository _professorRepository;
        private LectureRepository _lectureRepository;
        private HomeworkRepository _homeworkRepository;

        private readonly ILoggerFactory _factory;

        public UnitOfWork(DataBaseContext db, IMapperBLL mapper, ILoggerFactory factory = null)
        {
            _db = db;
            _factory = factory;
            _mapper = mapper.CreateMapper();
        }

        public IRepository<StudentDTO,Student> Students => _studentRepository ?? 
                                                (_studentRepository = new StudentRepository(_db, _mapper, _factory));

        public IRepository<ProfessorDTO, Professor> Professors => _professorRepository ?? 
                                                    (_professorRepository = new ProfessorRepository(_db, _mapper, _factory));

        public IRepository<LectureDTO, Lecture> Lectures => _lectureRepository ?? 
                                                (_lectureRepository = new LectureRepository(_db, _mapper, _factory));

        public IRepository<HomeworkDTO, Homework> Homework => _homeworkRepository ??
                                                      (_homeworkRepository = new HomeworkRepository(_db, _mapper, _factory));

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
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
