using System;
using NUnit.Framework;
using CollectionsLib;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestM08
{
    class FibonacciNumbersTest
    {
        private static readonly List<TestCaseData> GetNumbersTestData =
            new List<TestCaseData>(new[]
                {
                    new TestCaseData(
                        0,
                        new List<int> {}
                    ),
                    new TestCaseData(
                        1,
                        new List<int> {1}
                    ),
                    new TestCaseData(
                        2,
                        new List<int> {1, 1}
                    ),
                    new TestCaseData(
                        6,
                        new List<int> {1, 1, 2, 3, 5, 8}
                    )
                }
            );

        [TestCaseSource(nameof(GetNumbersTestData))]
        public void GetNumbersTest(int number, List<int> expectedResult)
        {
            var result = FibonacciNumbers.GetNumbers(number).ToList();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(-1, typeof(ArgumentException))]
        public void FibonacciExceptionsTest(int number, Type expectedEx)
        {
            void Result() => FibonacciNumbers.GetNumbers(number).ToList();

            Assert.Throws(expectedEx, Result);
        }
    }
}
