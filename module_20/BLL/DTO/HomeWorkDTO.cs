using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class HomeWorkDTO
    {
        public int Id { get; set; }

        public int? StudentId { get; set; }

        public int? LectureId { get; set; }

        public bool Presence { get; set; }

        public int Mark { get; set; }

        public DateTime Date { get; set; }
    }
}
