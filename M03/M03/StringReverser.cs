using System;

namespace M03
{
    public class StringReverser
    {
        public StringReverser(string str)
        {
            Str = str;
        }

        public string Reverse()
        {
            var strings = Str.Split(' ');
            Array.Reverse(strings);
            return string.Join(" ", strings);
        }

        public string Str { get; }
    }
}

