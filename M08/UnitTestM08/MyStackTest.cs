using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using CollectionsLib.Collections;

namespace UnitTestM08
{
    class MyStackTest
    {
        private static readonly List<TestCaseData> PushPopTestData =
            new List<TestCaseData>(new[]
                {
                    new TestCaseData(
                        new List<int> {1, 2, 3},
                        new List<int> {3, 2, 1}
                    ),
                    new TestCaseData(
                        new List<int> {},
                        new List<int> {}
                    )
                }
            );

        [TestCaseSource(nameof(PushPopTestData))]
        public void PushPopTest(List<int> push, List<int> expectedPop)
        {
            var stack = new MyStack<int>();

            foreach (var num in push)
            {
                stack.Push(num);
            }

            var result = new List<int>();

            for (var i = 0; i < expectedPop.Count; ++i)
            {
                result.Add(stack.Pop());
            }

            Assert.That(result, Is.EqualTo(expectedPop));
        }

        [TestCase(null, typeof(ArgumentNullException))]
        public void PushNullExceptionTest(string item, Type expectedEx)
        {
            var stack = new MyStack<string>();

            void Result() => stack.Push(item);

            Assert.Throws(expectedEx, Result);
        }

        private static readonly List<TestCaseData> PeekTestData =
            new List<TestCaseData>(new[]
                {
                    new TestCaseData(
                        new List<int> {1, 2, 3},
                        3
                    ),
                }
            );

        [TestCaseSource(nameof(PeekTestData))]
        public void PeekTest(List<int> source, int expectedResult)
        {
            var stack = new MyStack<int>();

            foreach (var item in source)
            {
                stack.Push(item);
            }

            var result = stack.Peek();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(typeof(InvalidOperationException))]
        public void PeekExceptionTest(Type expectedEx)
        {
            var stack = new MyStack<int>();

            void Result() => stack.Peek();

            Assert.Throws(expectedEx, Result);
        }

        private static readonly List<TestCaseData> ContainsTestData =
            new List<TestCaseData>(new[]
                {
                    new TestCaseData(
                        new List<int> {1, 2, 3},
                        new List<int> {1, 0, 5},
                        new List<bool> { true, false, false}
                    )
                }
            );

        [TestCaseSource(nameof(ContainsTestData))]
        public void ContainsTest(List<int> source, List<int> contains, List<bool> expectedResult)
        {
            var stack = new MyStack<int>();

            foreach (var item in source)
            {
                stack.Push(item);
            }

            var result = contains.Select(item => stack.Contains(item)).ToList();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(null, typeof(ArgumentNullException))]
        public void ContainsExceptionTest(string contains, Type expectedEx)
        {
            var stack = new MyStack<string>();

            void Result() => stack.Contains(contains);

            Assert.Throws(expectedEx, Result);
        }

        private static readonly List<TestCaseData> ClearTestData =
            new List<TestCaseData>(new[]
                {
                    new TestCaseData(
                        new List<int> {1, 2, 3}
                    )
                }
            );

        [TestCaseSource(nameof(ClearTestData))]
        public void ClearTest(List<int> source)
        {
            var stack = new MyStack<int>();

            foreach (var item in source)
            {
                stack.Push(item);
            }

            stack.Clear();
            var result = stack.ToList().Count;

            Assert.That(result , Is.EqualTo(0));
        }

        private static readonly List<TestCaseData> CountTestData =
            new List<TestCaseData>(new[]
                {
                    new TestCaseData(new List<int> {}, 0, 0),
                    new TestCaseData(new List<int> {1, 2, 3}, 3, 0),
                    new TestCaseData(new List<int> {1, 2, 3}, 0, 3),
                    new TestCaseData(new List<int> {1, 2, 3}, 2, 1),
                }
            );

        [TestCaseSource(nameof(CountTestData))]
        public void CountTest(List<int> push, int pop, int expectedCount)
        {
            var stack = new MyStack<int>();

            foreach (var item in push)
            {
                stack.Push(item);
            }

            for (var i = 0; i < pop; ++i)
            {
                stack.Pop();
            }

            var result = stack.Count;

            Assert.That(result, Is.EqualTo(expectedCount));
        }
    }
}
