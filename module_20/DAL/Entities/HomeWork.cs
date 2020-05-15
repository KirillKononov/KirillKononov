using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Homework
    {
        public int Id { get; set; }

        public int? StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int? LectureId { get; set; }
        public virtual Lecture Lecture { get; set; }

        [Required]
        public bool Presence { get; set; }

        [Required]
        public int Mark { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
