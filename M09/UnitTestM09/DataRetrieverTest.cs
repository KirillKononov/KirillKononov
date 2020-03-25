using System;
using System.Collections.Generic;
using System.Linq;
using M09;
using NUnit.Framework;

namespace UnitTestM09
{
    class DataRetrieverTest
    {
        private static readonly List<TestCaseData> DataForFilterTest =
           new List<TestCaseData>(new[]
           {
               new TestCaseData("-name Kirill",
                   new List<StudentAndTest> {}),

               new TestCaseData("-name Ivan -surname Kononov",
                   new List<StudentAndTest> {}),

               new TestCaseData(
                    "-name Ivan",
                    new List<StudentAndTest>
                    {
                        new StudentAndTest {Name = "Ivan Ivanov", Subject = "Maths", Mark = 5,
                            Date = DateTime.ParseExact("20/11/2019", "dd/MM/yyyy", null)},

                        new StudentAndTest {Name = "Ivan Ivanov", Subject = "Physics", Mark = 4,
                            Date = DateTime.ParseExact("24/11/2019", "dd/MM/yyyy", null)},

                        new StudentAndTest {Name = "Ivan Ivanov", Subject = "Biology", Mark = 5,
                            Date = DateTime.ParseExact("28/11/2019", "dd/MM/yyyy", null)},

                        new StudentAndTest {Name = "Ivan Ivanov", Subject = "History", Mark = 5,
                            Date = DateTime.ParseExact("01/12/2019", "dd/MM/yyyy", null)},
                    }
                    ),

                new TestCaseData(
                    "-name Ivan -surname Ivanov -minmark 5 -datefrom 28/11/2019 -dateto 29/11/2019",
                    new List<StudentAndTest>
                    {
                        new StudentAndTest {Name = "Ivan Ivanov", Subject = "Biology", Mark = 5,
                            Date = DateTime.ParseExact("28/11/2019", "dd/MM/yyyy", null)},
                    }
                ),

                new TestCaseData(
                    "-minmark 3 -maxmark 4 -datefrom 15/11/2019 -dateto 01/12/2019",
                    new List<StudentAndTest>
                    {
                        new StudentAndTest {Name = "Ivan Ivanov", Subject = "Physics", Mark = 4,
                            Date = DateTime.ParseExact("24/11/2019", "dd/MM/yyyy", null)},

                        new StudentAndTest {Name = "Semen Babaev", Subject = "Physics", Mark = 4,
                            Date = DateTime.ParseExact("19/11/2019", "dd/MM/yyyy", null)},

                        new StudentAndTest {Name = "Semen Babaev", Subject = "History", Mark = 3,
                            Date = DateTime.ParseExact("01/12/2019", "dd/MM/yyyy", null)},
                    }
                ),

                new TestCaseData(
                    "-minmark 3 -maxmark 4 -datefrom 15/11/2019 -dateto 02/12/2019 -test Physics",
                    new List<StudentAndTest>
                    {
                        new StudentAndTest {Name = "Ivan Ivanov", Subject = "Physics", Mark = 4,
                            Date = DateTime.ParseExact("24/11/2019", "dd/MM/yyyy", null)},

                        new StudentAndTest {Name = "Semen Babaev", Subject = "Physics", Mark = 4,
                            Date = DateTime.ParseExact("19/11/2019", "dd/MM/yyyy", null)},
                    }
                ),

                new TestCaseData(
                    "-test Physics",
                    new List<StudentAndTest>
                    {
                        new StudentAndTest {Name = "Ivan Ivanov", Subject = "Physics", Mark = 4,
                            Date = DateTime.ParseExact("24/11/2019", "dd/MM/yyyy", null)},

                        new StudentAndTest {Name = "Semen Babaev", Subject = "Physics", Mark = 4,
                            Date = DateTime.ParseExact("19/11/2019", "dd/MM/yyyy", null)}
                    }
                ),

                new TestCaseData(
                    "-minmark 3 -maxmark 4 -datefrom 15/11/2019 -dateto 02/12/2019 -sort surname asc",
                    new List<StudentAndTest>
                    {
                        new StudentAndTest {Name = "Semen Babaev", Subject = "Physics", Mark = 4,
                            Date = DateTime.ParseExact("19/11/2019", "dd/MM/yyyy", null)},

                        new StudentAndTest {Name = "Semen Babaev", Subject = "History", Mark = 3,
                            Date = DateTime.ParseExact("01/12/2019", "dd/MM/yyyy", null)},

                        new StudentAndTest {Name = "Ivan Ivanov", Subject = "Physics", Mark = 4,
                            Date = DateTime.ParseExact("24/11/2019", "dd/MM/yyyy", null)}
                    }
                ),

                new TestCaseData(
                    "-minmark 3 -maxmark 4 -datefrom 15/11/2019 -dateto 02/12/2019 -sort mark asc",
                    new List<StudentAndTest>
                    {
                        new StudentAndTest {Name = "Semen Babaev", Subject = "History", Mark = 3,
                            Date = DateTime.ParseExact("01/12/2019", "dd/MM/yyyy", null)},

                        new StudentAndTest {Name = "Ivan Ivanov", Subject = "Physics", Mark = 4,
                            Date = DateTime.ParseExact("24/11/2019", "dd/MM/yyyy", null)},

                        new StudentAndTest {Name = "Semen Babaev", Subject = "Physics", Mark = 4,
                        Date = DateTime.ParseExact("19/11/2019", "dd/MM/yyyy", null)}
                    }
                )
           });

        [TestCaseSource(nameof(DataForFilterTest))]
        public void FilterTest(string criteria, List<StudentAndTest> expectedResult)
        {
            var dataRetriever = new DataRetriever(Environment.CurrentDirectory + @"\StudentDataForTest.json");

            var result = dataRetriever.Filter(criteria).ToList();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(null, typeof(NullReferenceException))]
        [TestCase("-name Ivan rtr", typeof(FormatException))]
        [TestCase("rtr", typeof(FormatException))]
        [TestCase("-sort nam asc", typeof(FormatException))]
        [TestCase("-sort name as", typeof(FormatException))]
        public void FilterExceptionTest(string criteria, Type expectedEx)
        {
            var dataRetriever = new DataRetriever(Environment.CurrentDirectory + @"\StudentDataForTest.json");

            void Result() => dataRetriever.Filter(criteria).ToList();

            Assert.Throws(expectedEx, Result);
        }
    }
}
