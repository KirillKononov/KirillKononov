﻿using System;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataAccess
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Homework> Homework { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Professor> Professors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student[]
                {
                    new Student { Id = 1, FirstName = "Kirill", LastName = "Kononov",
                        AverageMark = (float) 4.67, MissedLectures = 0},
        
                    new Student { Id = 2, FirstName = "Ivan", LastName = "Ivanov",
                    AverageMark = (float) 0, MissedLectures = 3},
        
                    new Student { Id = 3, FirstName = "Semen", LastName = "Petrov",
                        AverageMark = (float) 4.0, MissedLectures = 0},
        
                    new Student { Id = 4, FirstName = "Dennis", LastName = "Gavrilov",
                        AverageMark = (float) 1.67, MissedLectures = 2},
        
                    new Student { Id = 5, FirstName = "Anton", LastName = "Antipov",
                        AverageMark = (float) 3.67, MissedLectures = 0}
                });
        
            modelBuilder.Entity<Professor>().HasData(
                new Professor[]
                {
                    new Professor { Id = 1, FirstName = "Mihail", LastName = "Sulimov" },
                    new Professor { Id = 2, FirstName = "Ludmila", LastName = "Kozlova" },
                    new Professor { Id = 3, FirstName = "Denis", LastName = "Filatov" }
                });
        
            modelBuilder.Entity<Lecture>().HasData(
                new Lecture[]
                {
                    new Lecture { Id = 1, Name = "Robotics", ProfessorId = 1},
                    new Lecture { Id = 2, Name = "Mechatronics", ProfessorId = 1},
                    new Lecture { Id = 3, Name = "Physics", ProfessorId = 3}
                });
        
            modelBuilder.Entity<Homework>().HasData(
                new Homework[]
                {
                    new Homework { Id = 1, StudentId = 1, LectureId = 1, StudentPresence = true,
                        HomeworkPresence = true, Mark = 5, Date = new DateTime(2019,12, 02)},
                    new Homework { Id = 2, StudentId = 2, LectureId = 1, StudentPresence = false,
                        HomeworkPresence = false, Mark = 0, Date = new DateTime(2019,12, 02)},
                    new Homework { Id = 3, StudentId = 3, LectureId = 1, StudentPresence = true,
                        HomeworkPresence = true, Mark = 4, Date = new DateTime(2019,12, 02)},
                    new Homework { Id = 4, StudentId = 4, LectureId = 1, StudentPresence = true,
                        HomeworkPresence = true, Mark = 5, Date = new DateTime(2019,12, 02)},
                    new Homework { Id = 5, StudentId = 5, LectureId = 1, StudentPresence = true,
                        HomeworkPresence = true, Mark = 5, Date = new DateTime(2019,12, 02)},
        
                    new Homework { Id = 6, StudentId = 1, LectureId = 2, StudentPresence = true,
                        HomeworkPresence = true, Mark = 5, Date = new DateTime(2019,12, 04)},
                    new Homework { Id = 7, StudentId = 2, LectureId = 2, StudentPresence = false,
                        HomeworkPresence = false, Mark = 0, Date = new DateTime(2019,12, 04)},
                    new Homework { Id = 8, StudentId = 3, LectureId = 2, StudentPresence = true,
                        HomeworkPresence = true, Mark = 4, Date = new DateTime(2019,12, 04)},
                    new Homework { Id = 9, StudentId = 4, LectureId = 2, StudentPresence = false,
                        HomeworkPresence = false, Mark = 0, Date = new DateTime(2019,12, 04)},
                    new Homework { Id = 10, StudentId = 5, LectureId = 2, StudentPresence = true,
                        HomeworkPresence = true, Mark = 5, Date = new DateTime(2019,12, 04)},
        
                    new Homework { Id = 11, StudentId = 1, LectureId = 3, StudentPresence = true,
                        HomeworkPresence = true, Mark = 4, Date = new DateTime(2019,12, 06)},
                    new Homework { Id = 12, StudentId = 2, LectureId = 3, StudentPresence = false,
                        HomeworkPresence = false, Mark = 0, Date = new DateTime(2019,12, 06)},
                    new Homework { Id = 13, StudentId = 3, LectureId = 3, StudentPresence = true,
                        HomeworkPresence = true, Mark = 4, Date = new DateTime(2019,12, 06)},
                    new Homework { Id = 14, StudentId = 4, LectureId = 3, StudentPresence = false,
                        HomeworkPresence = false, Mark = 0, Date = new DateTime(2019,12, 06)},
                    new Homework { Id = 15, StudentId = 5, LectureId = 3, StudentPresence = true,
                        HomeworkPresence = true, Mark = 1, Date = new DateTime(2019,12, 06)}
                });
        }
    }
}
