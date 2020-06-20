using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Interfaces.ServicesInterfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetAllAsync();
        Task<StudentDTO> GetAsync(int? id);
        IEnumerable<StudentDTO> Find(Func<Student, bool> predicate);
        Task CreateAsync(StudentDTO item);
        Task UpdateAsync(StudentDTO item);
        Task DeleteAsync(int? id);
    }
}