using NUnit.Framework;
using M03;

namespace UnitTestM03
{
    class DoubleLetterMakerTest
    {
        [TestCase("", "", "")]
        [TestCase("omg i love shrek", "o kek", "oomg i loovee shreekk")]
        [TestCase("o kek", "omg i love shrek", "oo kkeekk")]
        [TestCase("omg ", "omg ", "oommgg ")]
        public void MakeDoublesTest(string first, string second, string expectedResult)
        {
            var doubles = new DoubleLetterMaker(first, second);

            var result = doubles.MakeDoubles();

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
