using System;

namespace BLL.DTO
{
    public class Attendance
    {
        public string LectureName { get; set; }

        public string ProfessorName { get; set; }

        public string StudentName { get; set; }

        public bool StudentPresence { get; set; }

        public bool HomeworkPresence { get; set; }

        public int Mark { get; set; }

        public DateTime Date { get; set; }
    }
}
