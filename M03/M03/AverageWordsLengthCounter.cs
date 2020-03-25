using System;

namespace M03
{
    public class AverageWordsLengthCounter
    {
        public AverageWordsLengthCounter(string str)
        {
            Str = str;
        }
        public double Count()
        {
            var temp = Str.Split(' ', '.', ',', '!', '?', '-');
            var quantity = 0;
            double length = 0;
            foreach (var i in temp)
            {
                if (i.Length > 0)
                {
                    ++quantity;
                }
                length += i.Length;
            }

            if (quantity == 0)
            {
                throw new ArgumentException("Was printed the string without words");
            }
            return length / quantity;
        }

        public string Str { get; }
    }
}
