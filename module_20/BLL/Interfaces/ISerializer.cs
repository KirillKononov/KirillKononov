using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface ISerializer
    {
        string Serialize(IEnumerable<Attendance> attendance);
    }
}