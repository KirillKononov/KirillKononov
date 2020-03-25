using System;
using System.Collections.Generic;

namespace CollectionsLib
{
    public static class FibonacciNumbers
    {
        public static IEnumerable<int> GetNumbers(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException("Entered invalid number");
            }

            var first = 0;
            var second = 1;

            if (number > 0)
            {
                yield return second;

            }

            for (var i = 1; i < number; ++i)
            {
                var third = first + second;
                yield return third;
                first = second;
                second = third;
            }
        }
    }
}
