using System;
using NUnit.Framework;
using Calculator;

namespace UnitTestM08
{
    public class CalculatorTest
    {
        [TestCase("3 4 2 * 1 5 - / +", 1)]
        [TestCase("5 1 2 + 4 * + 3 -", 14)]
        public void CalculateTest(string expression, double expectedResult)
        {
            var result = expression.Calculate();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("1 0 /", typeof(DivideByZeroException))]
        [TestCase("1 p 0 /", typeof(FormatException))]
        [TestCase("1 /", typeof(FormatException))]
        [TestCase("", typeof(ArgumentNullException))]
        public void CalculatorExceptionTest(string expression, Type expectedEx)
        {
            void Result() => expression.Calculate();

            Assert.Throws(expectedEx, Result);
        }
    }
}
