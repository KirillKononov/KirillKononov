using System;

namespace BLL.DTO
{
    public class Attendance
    {
        public string LectureName { get; set; }

        public string ProfessorName { get; set; }

        public string StudentName { get; set; }

        public bool Presence { get; set; }

        public int Mark { get; set; }

        public DateTime Date { get; set; }
    }
}
