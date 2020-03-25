using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class HomeWork
    {
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }
        [Required]
        public virtual Student Student { get; set; }

        [Required]
        public int LectureId { get; set; }
        [Required]
        public virtual Lecture Lecture { get; set; }

        [Required]
        public bool Presence { get; set; }

        [Required]
        public int Mark { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
