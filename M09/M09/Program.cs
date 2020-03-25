using System;
using System.Collections.Generic;

namespace M09
{

    class Program
    {
        private static void PrintStudent(IEnumerable<StudentAndTest> students)
        {
            Console.WriteLine("Student".PadRight(25) + "Test".PadRight(15) +
                              "Date".PadRight(15) + "Mark".PadRight(15));

            foreach (var student in students)
            {
                Console.WriteLine("-" + (student.Name).PadRight(25) +
                                  student.Subject.PadRight(15) +
                                  student.Date.ToShortDateString().PadRight(15) +
                                  student.Mark.ToString().PadRight(15));
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello! Please input your criteria.");
                Console.WriteLine("Available flags:");
                Console.WriteLine("-name 'name' -surname 'surname' -test 'testname'");
                Console.WriteLine("-datefrom 'dd/MM/yyyy' -dateto 'dd/MM/yyyy'");
                Console.WriteLine("-minmark 'mark' -maxmark 'mark'");
                Console.WriteLine("-sort name/surname/subject/mark/date asc/dsc");
                Console.WriteLine();

                var criteria = Console.ReadLine() ?? throw new ArgumentNullException();

                Console.WriteLine();

                var dataRetriever = new DataRetriever(Environment.CurrentDirectory + @"\StudentData.json");

                var students = dataRetriever.Filter(criteria);

                PrintStudent(students);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}
