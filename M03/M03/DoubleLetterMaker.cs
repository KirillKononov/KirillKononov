using System.Linq;
using System.Text;

namespace M03
{
    public class DoubleLetterMaker
    {
        public DoubleLetterMaker(string first, string second)
        {
            First = first;
            Second = second;
        }

        public string MakeDoubles()
        {
            var temp = new StringBuilder();
            for (int i = 0; i < First.Length; ++i)
            {
                temp.Append(First[i]);
                if (Second.Contains(First[i]) && First[i] != ' ')
                {
                    temp.Append(First[i]);
                }
            }
            return temp.ToString();
        }

        public string First { get; } 
        public string Second { get; }
    }
}
