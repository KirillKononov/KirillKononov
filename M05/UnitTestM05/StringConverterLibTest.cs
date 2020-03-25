using System;
using NUnit.Framework;
using StringConverterLib;

namespace UnitTestM05
{
    public class StringConverterLibTest
    {
        [TestCase("123", 123)]
        [TestCase("-2147483648", -2147483648)]
        [TestCase("2147483647", 2147483647)]
        public void StringConverterToInt32Test(string str, int expectedResult)
        {
            Assert.That(str.ToInt32(), Is.EqualTo(expectedResult));
        }

        [TestCase("")]
        [TestCase("-2147483649")]
        [TestCase("2147483648")]
        [TestCase("wwewet3232er")]
        public void StringConverterToInt32ExceptionTest(string str)
        {
            Assert.Throws(Is.TypeOf<ArgumentException>(), () => str.ToInt32());
        }
    }
}
