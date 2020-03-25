using System;
using NUnit.Framework;
using CollectionsLib;
using System.Collections.Generic;

namespace UnitTestM08
{
    class BinarySearchTest
    {
        private static readonly List<TestCaseData> DataForFindTest =
            new List<TestCaseData>(new[]
            {
                new TestCaseData(
                    new int[] {1, 2, 3, 4},
                    2,
                    1
                    ),
                new TestCaseData(
                    new int[] {1, 2, 3, 4},
                    0,
                    -1
                    ),
                new TestCaseData(
                    new int[] {1, 2, 3, 4, 5},
                    5,
                    4
                    ),
            });

        [TestCaseSource(nameof(DataForFindTest))]
        public void FindTest(int[] array,int searchingElement, int expectedResult)
        {
            var result = BinarySearch<int>.Find(array, searchingElement, Comparer<int>.Default);

            Assert.That(result, Is.EqualTo(expectedResult));

        }

        private static readonly List<TestCaseData> BinarySearchExceptionTestData =
            new List<TestCaseData>(new[]
                {
                    new TestCaseData(
                        null,
                        "reg",
                        typeof(ArgumentNullException)
                    ),
                    new TestCaseData(
                        new string[] {"reg"},
                        null,
                        typeof(ArgumentNullException)
                    ),
                    new TestCaseData(
                        new string[] {},
                        "reg",
                        typeof(ArgumentNullException)
                    )
                }
            );

        [TestCaseSource(nameof(BinarySearchExceptionTestData))]
        public void BinarySearchExceptionTest(string[] array, string searchingElement, Type expectedEx)
        {
            void Result() => BinarySearch<string>.Find(array, searchingElement, Comparer<string>.Default);

            Assert.Throws(expectedEx, Result);
        }
    }
}
