using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using CollectionsLib.Collections;

namespace UnitTestM08
{
    class MyHashSetTest
    {
        private static readonly List<TestCaseData> HashSetAddTestData =
            new List<TestCaseData>(new[]
                {
                    new TestCaseData(
                        new List<int> {1, 3, 5, 43, 65, 19, 6, 7, 8, 9, 13, 45, 13, 14, 15, 16},
                        new List<int> {0, 2, 20, 5, 3},
                        new List<bool> {true, true, true, false, false}
                    )
                }
            );

        [TestCaseSource(nameof(HashSetAddTestData))]
        public void AddCountTest(List<int> source, List<int> addNumbers, List<bool> expectedList)
        {
            var set = new MyHashSet<int>();

            foreach (var num in source)
            {
                set.Add(num);
            }

            var result = addNumbers.Select(item => set.Add(item)).ToList();

            Assert.That(result, Is.EqualTo(expectedList));
        }

        [TestCase(null, typeof(ArgumentNullException))]
        public void NullExceptionTest(string value, Type expectedEx)
        {
            var set = new MyHashSet<string>();

            void Result1() => set.Add(value);
            void Result2() => set.Remove(value);
            void Result3() => set.Contains(value);

            Assert.Throws(expectedEx, Result1);
            Assert.Throws(expectedEx, Result2);
            Assert.Throws(expectedEx, Result3);
        }

        private static readonly List<TestCaseData> HashSetContainsTestData =
            new List<TestCaseData>(new[]
                {
                    new TestCaseData(
                        new List<int> {1, 3, 5, 43, 65, 19, 6, 7, 8, 9, 13, 45, 13, 14, 15, 16},
                        new List<int> {0, 2, 20, 5, 3},
                        new List<bool> {false, false, false, true, true}
                    ),
                }
            );

        [TestCaseSource(nameof(HashSetContainsTestData))]
        public void ContainsTest(List<int> source, List<int> containsNumbers, List<bool> expectedList)
        {
            var set = new MyHashSet<int>();
            foreach (var num in source)
            {
                set.Add(num);
            }

            var result = containsNumbers.Select(item => set.Contains(item)).ToList();

            Assert.That(result, Is.EqualTo(expectedList));
        }

        private static readonly List<TestCaseData> HashSetRemoveTestData =
           new List<TestCaseData>(new[]
               {
                    new TestCaseData(
                        new List<int> {1, 3, 5, 43, 65, 19, 6, 7, 8, 9},
                        new List<int> {0, 19, 20, 43, 65},
                        new List<bool> {false, true, false, true, true},
                        new List<int>() { 1, 3, 5, 6, 7, 8, 9 },
                        7
                    ),
               }
           );

        [TestCaseSource(nameof(HashSetRemoveTestData))]
        public void RemoveCountTest(List<int> source, List<int> removeNumbers, 
            List<bool> expectedList, List<int> afterRemoveExpectedList, int expectedCount)
        {
            var set = new MyHashSet<int>();

            foreach (var num in source)
            {
                set.Add(num);
            }

            var removeResult = removeNumbers.Select(item => set.Remove(item)).ToList();

            var afterRemove = set;

            var resultCount = afterRemove.Count;

            Assert.That(removeResult, Is.EqualTo(expectedList));
            Assert.That(afterRemove, Is.EqualTo(afterRemoveExpectedList));
            Assert.That(resultCount, Is.EqualTo(expectedCount));
        }

        private static readonly List<TestCaseData> HashSetClearTestData =
            new List<TestCaseData>(new[]
                {
                    new TestCaseData(
                        new List<int> { 1, 3, 5, 43, 65, 19, 6, 7, 8, 9 }
                    ),
                    new TestCaseData(
                        new List<int> {}
                    ),
                }
            );

        [TestCaseSource(nameof(HashSetClearTestData))]
        public void ClearTest(List<int> source)
        {
            var set = new MyHashSet<int>();

            foreach (var num in source)
            {
                set.Add(num);
            }

            set.Clear();
            var result = set.Count;

            Assert.That(result, Is.EqualTo(0));
        }
    }
}
