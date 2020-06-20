using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int? id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        Task CreateAsync(T homework);
        void Update(T homework);
        void Delete(T homework);
    }
}