using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Interfaces.ServicesInterfaces
{
    public interface IProfessorService
    {
        Task<IEnumerable<ProfessorDTO>> GetAllAsync();
        Task<ProfessorDTO> GetAsync(int? id);
        IEnumerable<ProfessorDTO> Find(Func<Professor, bool> predicate);
        Task CreateAsync(ProfessorDTO item);
        Task UpdateAsync(ProfessorDTO item);
        Task DeleteAsync(int? id);
    }
}