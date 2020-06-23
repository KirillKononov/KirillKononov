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
    public class LectureRepository : IRepository<Lecture>
    {
        private readonly DataBaseContext _db;

        public LectureRepository(DataBaseContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Lecture>> GetAllAsync()
        {
            var lectures = await _db.Lectures.ToListAsync();
            return lectures;
        }

        public async Task<Lecture> GetAsync(int? id)
        {
            var lecture = await _db.Lectures.FindAsync(id);
            return lecture;
        }

        public async Task CreateAsync(Lecture lecture)
        {
            await _db.Lectures.AddAsync(lecture);
            await _db.SaveChangesAsync();
        }

        public void Update(Lecture lecture)
        {
            _db.Entry(lecture).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Lecture> Find(Func<Lecture, bool> predicate)
        {
            var lectures = _db.Lectures
                .Where(predicate)
                .ToList();
            return lectures;
        }

        public void Delete(int? id)
        {
            var lecture = _db.Lectures.Find(id);
            _db.Lectures.Remove(lecture);
            _db.SaveChanges();
        }
    }
}
