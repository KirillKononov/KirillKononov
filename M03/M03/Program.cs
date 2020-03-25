using System;

namespace M03
{

    class Program
    {
        private static void MakeBigNumSummator()
        {
            var num1 = "9";
            var num2 = "9";
            var twoNumbers = new BigNumSummator(num1, num2);
            Console.WriteLine(twoNumbers.TheSum());
        }

        private static void MakePhoneNumbersExtractor()
        {
            var numbers = new PhoneNumbersExtractor();
            numbers.ExtractNumbersFromFile();
        }

        private static void MakeAverageWordsLengthCounter()
        {
            var str = "  ";
            var length = new AverageWordsLengthCounter(str);
            Console.WriteLine(length.Count());
        }

        static void Main(string[] args)
        {
            var first = "omg i love shrek";
            var second = "o kek";
            var doubles = new DoubleLetterMaker(first, second);
            Console.WriteLine(doubles.MakeDoubles());


            var statement = "The greatest victory is that which requires no battle";
            var revWords = new StringReverser(statement);
            Console.WriteLine(revWords.Reverse());

            try
            {
                MakeBigNumSummator();
                MakeAverageWordsLengthCounter();
                MakePhoneNumbersExtractor();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
    }
}