using System;
using System.Collections.Generic;
using NUnit.Framework;
using MatrixSortLib;

namespace UnitTestM07
{
    public class SortingDirectionTest
    {
        [TestCaseSource(nameof(DataForAscendingSort))]
        public void AscendingSortTest(int[,] matrix, int[] sortType, int[,] expectedResult)
        {
            var result = matrix.RowsSort(SortingType.InOrderOfSumsOfRowElements, 
                Sorting.SortingDirection.Ascending);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        private static readonly List<TestCaseData> DataForAscendingSort =
            new List<TestCaseData>(new[]
            {
                new TestCaseData(new int[,] {{}}, new int[] {}, new int[,] {{}}),

                new TestCaseData(new int[4, 2] { { 2, 7 }, { 5, 6 }, { 9, 4 }, { 2, 0 } },
                    new [] {9, 11, 13, 2}, new int[4, 2] { { 2, 0 }, { 2, 7 }, { 5, 6 }, { 9, 4 } }),

                new TestCaseData(new int[2, 4] { { 2, 7, 5, 6 }, { 9, 4, 2, 0 } },
                    new [] {20, 15}, new int[2, 4] { { 9, 4, 2, 0 }, { 2, 7, 5, 6 } }),

                new TestCaseData(new int[2,2] { { 9, 4 }, { 2, 0 } }, new [] {13, 2},
                    new int[2,2] {{ 2, 0 }, { 9, 4 }}),
            });

        [TestCaseSource(nameof(DataForDescendingSort))]
        public void DescendingSortTest(int[,] matrix, int[] sortType, int[,] expectedResult)
        {
            var result = matrix.RowsSort(SortingType.InOrderOfSumsOfRowElements,
                Sorting.SortingDirection.Descending);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        private static readonly List<TestCaseData> DataForDescendingSort =
            new List<TestCaseData>(new[]
            {
                new TestCaseData(new int[,] {{}}, new int[] {}, new int[,] {{}}),

                new TestCaseData(new int[4, 2] { { 2, 7 }, { 5, 6 }, { 9, 4 }, { 2, 0 } },
                    new [] {9, 11, 13, 2}, new int[4, 2] { { 9, 4 }, { 5, 6 }, { 2, 7 }, { 2, 0 }}),

                new TestCaseData(new int[2, 4] { { 2, 7, 5, 6 }, { 9, 4, 2, 0 } },
                    new [] {20, 15}, new int[2, 4] { { 2, 7, 5, 6 }, { 9, 4, 2, 0 } }),

                new TestCaseData(new int[2,2] { { 9, 4 }, { 2, 0 } }, new [] {13, 2},
                    new int[2,2] { { 9, 4 }, { 2, 0 } }),
            });

        [TestCase(null)]
        public void SortExceptionTest(int[,] matrix)
        {
            void Result() => matrix.RowsSort(SortingType.InOrderOfSumsOfRowElements,
                Sorting.SortingDirection.Descending);

            Assert.Throws(Is.TypeOf<ArgumentNullException>(), Result);
        }
    }
}
