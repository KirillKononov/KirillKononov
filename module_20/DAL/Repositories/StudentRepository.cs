using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DataAccess;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly DataBaseContext _db;

        public StudentRepository(DataBaseContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var students = await _db.Students.ToListAsync();
            return students;
        }

        public async Task<Student> GetAsync(int? id)
        {
            var student = await _db.Students.FindAsync(id);
            return student;
        }

        public async Task CreateAsync(Student student)
        {
            await _db.Students.AddAsync(student);
            await _db.SaveChangesAsync();
        }

        public void Update(Student student)
        {
            _db.Entry(student).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Student> Find(Func<Student, bool> predicate)
        {
            var students = _db.Students
                .Where(predicate)
                .ToList();
            return students;
        }

        public void Delete(int? id)
        {
            var student = _db.Students.Find(id);
            _db.Students.Remove(student);
            _db.SaveChanges();
        }
    }
}
