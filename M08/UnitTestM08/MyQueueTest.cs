using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using CollectionsLib.Collections;

namespace UnitTestM08
{
    class MyQueueTest
    {
        private static readonly List<TestCaseData> EnqueueDequeueTestData =
            new List<TestCaseData>(new[]
                {
                    new TestCaseData(
                        new List<int> {1, 2, 3},
                        new List<int> {1, 2, 3}
                    ),
                    new TestCaseData(
                        new List<int> {},
                        new List<int> {}
                    ),
                }
            );

        [TestCaseSource(nameof(EnqueueDequeueTestData))]
        public void EnqueueDequeueTest(List<int> enqueue, List<int> expectedDequeue)
        {
            var queue = new MyQueue<int>();

            foreach (var item in enqueue)
            {
                queue.Enqueue(item);
            }

            var result = new List<int>();

            for (var i = 0; i < expectedDequeue.Count; ++i)
            {
                result.Add(queue.Dequeue());
            }

            Assert.That(result, Is.EqualTo(expectedDequeue));
        }

        [TestCase(null, typeof(ArgumentNullException))]
        public void EnqueueNullExceptionTest(string item, Type expectedEx)
        {
            var queue = new MyQueue<string>();

            void Result() => queue.Enqueue(item);

            Assert.Throws(expectedEx, Result);
        }

        private static readonly List<TestCaseData> PeekTestData =
            new List<TestCaseData>(new[]
                {
                    new TestCaseData(
                        new List<int> {1, 2, 3},
                        1
                    ),
                }
            );

        [TestCaseSource(nameof(PeekTestData))]
        public void PeekTest(List<int> source, int expected)
        {
            var queue = new MyQueue<int>();
            foreach (var item in source)
            {
                queue.Enqueue(item);
            }

            var result = queue.Peek();

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(typeof(InvalidOperationException))]
        public void PeekExceptionTest(Type expectedEx)
        {
            var queue = new MyQueue<int>();

            void Result() => queue.Peek();

            Assert.Throws(expectedEx, Result);
        }

        private static readonly List<TestCaseData> ContainsTestData =
            new List<TestCaseData>(new[]
                {
                    new TestCaseData(
                        new List<int> {1, 2, 3},
                        new List<int> {1, 0, 5},
                        new List<bool> {true, false, false}
                    ),
                }
            );

        [TestCaseSource(nameof(ContainsTestData))]
        public void ContainsTest(List<int> source, List<int> contains, List<bool> expectedResult)
        {
            var queue = new MyQueue<int>();

            foreach (var item in source)
            {
                queue.Enqueue(item);
            }

            var result = contains.Select(item => queue.Contains(item));

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(null, typeof(ArgumentNullException))]
        public void ContainsExceptionTest(string source, Type expectedEx)
        {
            var queue = new MyQueue<string>();

            void Result() => queue.Contains(source);

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
            var queue = new MyQueue<int>();

            foreach (var item in source)
            {
                queue.Enqueue(item);
            }

            queue.Clear();
            var result = queue.ToList().Count;

            Assert.That(result, Is.EqualTo(0));
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
        public void CountTest(List<int> enqueue, int dequeue, int expectedCount)
        {
            var queue = new MyQueue<int>();

            foreach (var item in enqueue)
            {
                queue.Enqueue(item);
            }

            for (var i = 0; i < dequeue; ++i)
            {
                queue.Dequeue();
            }

            var result = queue.Count;

            Assert.That(result, Is.EqualTo(expectedCount));
        }
    }
}
