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
    public class ProfessorRepository : IRepository<Professor>
    {
        private readonly DataBaseContext _db;

        public ProfessorRepository(DataBaseContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Professor>> GetAllAsync()
        {
            var professors = await _db.Professors.ToListAsync();
            return professors;
        }

        public async Task<Professor> GetAsync(int? id)
        {
            var professor = await _db.Professors.FindAsync(id);
            return professor;
        }

        public async Task CreateAsync(Professor prof)
        {
            await _db.Professors.AddAsync(prof);
            await _db.SaveChangesAsync();
        }

        public void Update(Professor professor)
        {
            _db.Entry(professor).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Professor> Find(Func<Professor, bool> predicate)
        {
            var professors =_db.Professors
                .Where(predicate)
                .ToList();
            return professors;
        }

        public void Delete(int? id)
        {
            var professor = _db.Professors.Find(id);
            _db.Professors.Remove(professor);
            _db.SaveChanges();
        }
    }
}
