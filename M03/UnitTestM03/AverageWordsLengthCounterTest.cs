using System;
using NUnit.Framework;
using M03;

namespace UnitTestM03
{
    class AverageWordsLengthCounterTest
    {
        
        [TestCase("ку ", 2)]
        [TestCase("ку ! ", 2)]
        [TestCase("Create a, class that-implements a method.", 4.7142857142857144)]
        public void CountTest(string str, double expectedResult)
        {
            var length = new AverageWordsLengthCounter(str);

            var result = length.Count();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("- !?,")]
        [TestCase("")]
        public void CountExceptionTest(string str)
        {
            var length = new AverageWordsLengthCounter(str);

            void Result() => length.Count();

            Assert.Throws(Is.TypeOf<ArgumentException>()
                .And.Message.EqualTo("Was printed the string without words"),
                Result);
        }
    }
}
