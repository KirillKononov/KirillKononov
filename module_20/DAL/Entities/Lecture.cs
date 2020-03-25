using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Lecture
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int ProfessorId { get; set; }
        [Required]
        public virtual Professor Professor { get; set; }

        public virtual List<HomeWork> LectureHomeWorks { get; set; }
    }
}
