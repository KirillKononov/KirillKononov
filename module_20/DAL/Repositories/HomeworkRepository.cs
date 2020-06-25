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
    public class HomeworkRepository : IRepository<Homework>
    {
        private readonly DataBaseContext _db;

        public HomeworkRepository(DataBaseContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Homework>> GetAllAsync()
        {
            var homework = await _db.Homework.ToListAsync();
            return homework;
        }

        public async Task<Homework> GetAsync(int? id)
        {
            var homework = await _db.Homework.FindAsync(id);
            return homework;
        }

        public async Task CreateAsync(Homework homework)
        {
            await _db.Homework.AddAsync(homework);
            await _db.SaveChangesAsync();
        }

        public void Update(Homework homework)
        {
            _db.Entry(homework).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Homework> Find(Func<Homework, bool> predicate)
        {
            var homework = _db.Homework
                .Where(predicate)
                .ToList();
            return homework;
        }

        public void Delete(Homework homework)
        {
            _db.Homework.Remove(homework);
            _db.SaveChanges();
        }
    }
}
