using System;
using NLog;
using System.Linq;

namespace StringConverterLib
{
    public static class StringConverter
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static int ToInt32(this string str)
        {
            Log.Info($"User made a request for converting a string: {str} to int");

            Log.Info("Checking if the string is not empty");

            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("Entered string is empty");
            }

            var negative = str.StartsWithMinusSign();
            str = negative ? str.TrimStart('-') : str;

            str.Validate(negative);
            Log.Info("The string was checked on mistakes");


            var result = 0;
            Log.Info("Converting the string to int");
            for (var i = 0; i < str.Length; ++i)
            {
                result += str[i] - '0';
                result = i == str.Length - 1 ? result : result * 10;
            }

            Log.Info("The string was successfully converted to int");
            return negative ? -result : result;
        }

        private static void Validate(this string str, bool negative)
        {
            if (!str.IsNumeric())
            {
                throw new ArgumentException("Entered string isn't an integer number");
            }

            if (!str.LessThanInt32(negative))
            {
                throw new ArgumentException("Entered number is more than Int32");
            }

        }

        private static bool IsNumeric(this string str)
        {
            Log.Info("Checking if the string is a number");
            return str.All(char.IsDigit);
        }

        private static bool StartsWithMinusSign(this string str)
        {
            Log.Info("Checking if the string starts with minus");
            return str[0] == '-';
        }

        private static bool LessThanInt32(this string str, bool negative)
        {
            Log.Info("Checking if the string isn't more than int32");
            var max = negative ? int.MinValue.ToString().TrimStart('-') : int.MaxValue.ToString();

            if (str.Length > max.Length)
                return false;

            if (str.Length != max.Length)
                return true;

            for (var i = 0; i < max.Length; ++i)
            {
                if (str[i] > max[i])
                    return false;
            }

            return true;
        }
    }
}
