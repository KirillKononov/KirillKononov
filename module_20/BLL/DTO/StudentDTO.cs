using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public float AverageMark { get; set; }

        public int MissedLectures { get; set; }

        public List<HomeWorkDTO> StudentHomeWorks { get; set; }
    }
}
