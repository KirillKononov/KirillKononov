using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class LectureDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ProfessorId { get; set; }

        public List<HomeWorkDTO> LectureHomeWorks { get; set; }
    }
}
