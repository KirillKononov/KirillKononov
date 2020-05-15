using System;

namespace BLL.DTO
{
    public class HomeworkDTO
    {
        public int Id { get; set; }

        public int? StudentId { get; set; }

        public int? LectureId { get; set; }

        public bool Presence { get; set; }

        public int Mark { get; set; }

        public DateTime Date { get; set; }
    }
}
