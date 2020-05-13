using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Lecture
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int? ProfessorId { get; set; }
        public virtual Professor Professor { get; set; }

        //public virtual List<HomeWork> LectureHomeWorks { get; set; }
    }
}
