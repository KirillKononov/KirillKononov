using System;
using System.Collections.Generic;

namespace Calculator
{
    public static class PolishNotationCalculator
    {

        public static double Calculate(this string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentNullException();
            }

            var numbers = new Stack<double>();

            var parsedExpression = expression.Split(' ');

            foreach (var item in parsedExpression)
            {

                if (IsOperationSign(item))
                {
                    if (numbers.Count < 2)
                    {
                        throw new FormatException("Entered invalid expression");
                    }

                    var num1 = numbers.Pop();
                    var num2 = numbers.Pop();
                    
                    numbers.Push(Calculation(num1, num2, item));
                }
                else
                {
                    numbers.Push(double.Parse(item));
                }
            }

            return numbers.Pop();
        }

        private static double Calculation(double num1, double num2, string sign)
        {
            switch (sign)
            {
                case "+":
                    return num2 + num1;
                case "-":
                    return num2 - num1;
                case "*":
                    return num2 * num1;
                case "/":
                {
                    if (Math.Abs(num1) < double.Epsilon)
                    {
                        throw new DivideByZeroException();
                    }

                    return num2 / num1;
                }
                default:
                    throw  new FormatException("Entered invalid expression");
            }
        }

        private static bool IsOperationSign(string item)
        {
            return OperationSigns.Contains(item);
        }

        private const string OperationSigns = "+-*/";
    }
}
