using System;
using System.Text;

namespace M03
{
    public class BigNumSummator
    {
        public BigNumSummator(string first, string second)
        {
            if (!IsNumeric(first) || !IsNumeric(second))
            {
                throw new ArgumentException("Entered string is not a number");
            }
            if (second.Length > first.Length)
            {
                var t = first;
                first = second;
                second = t;
            }
            _first = first;
            _second = second;
        }

        private bool IsNumeric(string s)
        {
            foreach (var letter in s)
            {
                if (!char.IsDigit(letter))
                {
                    return false;
                }
            }
            return true;
        }

        public string TheSum()
        {
            var index = _second.Length - 1;
            var temp = 0;
            var sb = new StringBuilder();

            for (int i = _first.Length - 1; i >= 0; --i)
            {
                var num1 = _first[i] - '0';
                var num2 = 0;
                if (index >= 0)
                {
                    num2 = _second[index] - '0';
                }
                var result = (num1 + num2 + temp).ToString();
                if (result.Length > 1)
                {
                    sb.Insert(0, result[1]);
                    temp = result[0] - '0';
                }
                else
                {
                    temp = 0;
                    sb.Insert(0, result);
                }
                --index;
            }
            return temp == 0 ? sb.ToString() : sb.Insert(0, temp).ToString();
        }

        private readonly string _first;
        private readonly string _second;
    }
}
