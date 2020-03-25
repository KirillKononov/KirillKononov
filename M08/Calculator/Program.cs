using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var expression = "3 4 2 * 1 5 - / +";
            Console.WriteLine(expression.Calculate());
        }
    }
}
