using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Interfaces.ServicesInterfaces
{
    public interface ILectureService
    {
        Task<IEnumerable<LectureDTO>> GetAllAsync();
        Task<LectureDTO> GetAsync(int? id);
        IEnumerable<LectureDTO> Find(Func<Lecture, bool> predicate);
        Task CreateAsync(LectureDTO item);
        Task UpdateAsync(LectureDTO item);
        Task DeleteAsync(int? id);
    }
}