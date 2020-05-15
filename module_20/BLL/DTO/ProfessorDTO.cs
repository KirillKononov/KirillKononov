using System.Collections.Generic;

namespace BLL.DTO
{
    public class ProfessorDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<LectureDTO> Lectures { get; set; }
    }
}
