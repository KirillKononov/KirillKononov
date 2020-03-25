using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace M03
{
    class PhoneNumbersExtractor
    {
        public void ExtractNumbersFromFile()
        {
            if (!File.Exists("Text.txt"))
            {
                throw new ArgumentException($"File with name Text.txt does not exist");
            }

            var text = File.ReadAllText("Text.txt");

            var matches = _phonePatterns.Matches(text);
            var phoneNumbers = (from Match match in matches select match.Value).ToArray();

            foreach (var number in phoneNumbers)
            {
                Console.WriteLine(number);
            }

            File.WriteAllLines("Numbers.txt", phoneNumbers);
        }

        private const string Pattern1 = (@"[+][0-9]\s[(][0-9]{3}[)]\s[0-9]{3}-[0-9]{2}-[0-9]{2}");
        private const string Pattern2 = (@"[0-9]\s[0-9]{3}\s[0-9]{3}-[0-9]{2}-[0-9]{2}");
        private const string Pattern3 = (@"[+][0-9]{3}\s[(][0-9]{2}[)]\s[0-9]{3}-[0-9]{4}");
        private readonly Regex _phonePatterns = new Regex($"{Pattern1} | {Pattern2} | {Pattern3}",
            RegexOptions.IgnorePatternWhitespace);
    }
}
