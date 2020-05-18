using System.Collections.Generic;

namespace BLL.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public float AverageMark { get; set; }

        public int MissedLectures { get; set; }

        public List<HomeworkDTO> StudentHomework { get; set; }
    }
}
