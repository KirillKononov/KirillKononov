using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int? id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        Task CreateAsync(T item);
        void Update(T item);
        void Delete(int? id);
    }
}