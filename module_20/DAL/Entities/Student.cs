﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public float AverageMark { get; set; }

        [Required]
        public int MissedLectures { get; set; }

        //public virtual List<HomeWork> StudentHomeWorks { get; set; }
    }
}
