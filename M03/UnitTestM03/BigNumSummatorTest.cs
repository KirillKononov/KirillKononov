using System;
using NUnit.Framework;
using M03;

namespace UnitTestM03
{
    class BigNumSummatorTest
    {
        [TestCase("", "", "")]
        [TestCase("92233720368547758070", "90000000000000000000", "182233720368547758070")]
        [TestCase("90000000000000000000", "92233720368547758070", "182233720368547758070")]
        public void TheSumTest(string first, string second, string expectedResult)
        {
            var bigNumSummator = new BigNumSummator(first, second);

            var result = bigNumSummator.TheSum();

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
