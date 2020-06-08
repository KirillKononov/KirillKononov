using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRepository<T, out TU> 
        where T : class
        where TU : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int? id);
        IEnumerable<T> Find(Func<TU, bool> predicate);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int? id);
    }
}