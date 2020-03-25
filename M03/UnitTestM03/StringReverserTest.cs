using NUnit.Framework;
using M03;

namespace UnitTestM03
{
    class StringReverserTest
    {
        [TestCase("", "")]
        [TestCase("The greatest victory is that which requires no battle", 
            "battle no requires which that is victory greatest The")]
        [TestCase(" The", "The ")]
        [TestCase("The ", " The")]
        [TestCase("5 4 3", "3 4 5")]
        public void ReverseTest(string str, string expectedResult)
        {
            var revWords = new StringReverser(str);

            var result = revWords.Reverse();

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
