using System.Collections.Generic;

namespace BLL.DTO
{
    public class LectureDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ProfessorId { get; set; }

        public List<HomeworkDTO> LectureHomework { get; set; }
    }
}
