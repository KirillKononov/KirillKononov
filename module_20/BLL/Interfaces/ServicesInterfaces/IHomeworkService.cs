using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Interfaces.ServicesInterfaces
{
    public interface IHomeworkService
    {
        Task<IEnumerable<HomeworkDTO>> GetAllAsync();
        Task<HomeworkDTO> GetAsync(int? id);
        IEnumerable<HomeworkDTO> Find(Func<Homework, bool> predicate);
        Task CreateAsync(HomeworkDTO item);
        Task UpdateAsync(HomeworkDTO item);
        Task DeleteAsync(int? id);
    }
}