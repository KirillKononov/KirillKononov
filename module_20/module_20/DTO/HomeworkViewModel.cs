using System;

namespace module_20.DTO
{
    public class HomeworkViewModel
    {
        public int Id { get; set; }

        public int? StudentId { get; set; }

        public int? LectureId { get; set; }

        public bool StudentPresence { get; set; }

        public bool HomeworkPresence { get; set; }

        public int Mark { get; set; }

        public DateTime Date { get; set; }
    }
}
