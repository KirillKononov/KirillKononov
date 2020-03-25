using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Students
{
    class Program
    {
        private static void MakeStudent()
        {
            var subjects = new string[] { "Maths", "PE", "Physics", "History" };


            var st1_1 = new Student("Kirill Kononov", "kirill.kononov@epam.com");
            var st1_2 = new Student("Ivan Ivanov", "ivan.ivanov@epam.com");
            var st1_3 = new Student("Petr Petrov", "petr.petrov@epam.com");


            var st2_1 = new Student("kirill.kononov@epam.com");
            var st2_2 = new Student("ivan.ivanov@epam.com");
            var st2_3 = new Student("petr.petrov@epam.com");


            var studentSubjectDict = new Dictionary<Student, HashSet<string>> { };
            var rnd = new Random();
            studentSubjectDict[st1_1] = new HashSet<string> { subjects[rnd.Next(subjects.Length)],
                                                              subjects[rnd.Next(subjects.Length)],
                                                              subjects[rnd.Next(subjects.Length)]
                                                            };
            studentSubjectDict[st1_2] = new HashSet<string> { subjects[rnd.Next(subjects.Length)],
                                                              subjects[rnd.Next(subjects.Length)],
                                                              subjects[rnd.Next(subjects.Length)]
                                                            };
            studentSubjectDict[st1_3] = new HashSet<string> { subjects[rnd.Next(subjects.Length)],
                                                              subjects[rnd.Next(subjects.Length)],
                                                              subjects[rnd.Next(subjects.Length)]
                                                            };
            studentSubjectDict[st2_1] = new HashSet<string> { subjects[rnd.Next(subjects.Length)],
                                                              subjects[rnd.Next(subjects.Length)],
                                                              subjects[rnd.Next(subjects.Length)]
                                                            };
            studentSubjectDict[st2_2] = new HashSet<string> { subjects[rnd.Next(subjects.Length)],
                                                              subjects[rnd.Next(subjects.Length)],
                                                              subjects[rnd.Next(subjects.Length)]
                                                            };
            studentSubjectDict[st2_3] = new HashSet<string> { subjects[rnd.Next(subjects.Length)],
                                                              subjects[rnd.Next(subjects.Length)],
                                                              subjects[rnd.Next(subjects.Length)]
                                                            };

            Console.WriteLine(studentSubjectDict.Count());
            Console.WriteLine(st1_1.Equals(st2_1));


            foreach (var student in studentSubjectDict)
            {
                Console.WriteLine(student.Key.Name + " " + student.Key.Email);
            }

            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            try
            {
                MakeStudent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }

            Console.ReadLine();
        }
    }
}
