using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IRepository<T, out TU> 
        where T : class
        where TU : class
    {
        IEnumerable<T> GetAll();
        T Get(int? id);
        IEnumerable<T> Find(Func<TU, bool> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int? id);
    }
}