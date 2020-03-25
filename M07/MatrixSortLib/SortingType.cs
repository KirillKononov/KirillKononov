using System;
using System.Linq;

namespace MatrixSortLib
{
    public static class SortingType
    {
        public static int InOrderOfSumsOfRowElements(int[] row1, int[] row2)
        {
            Validate(row1, row2);

            var sum1 = row1.Sum();
            var sum2 = row2.Sum();
            if (sum1 == sum2)
                return 0;

            return (sum1 > sum2) ? 1 : -1;
        }

        public static int InOrderOfMaxElementInRow(int[] row1, int[] row2)
        {
            Validate(row1, row2);

            var max1 = row1.Max();
            var max2 = row2.Max();
            if (max1 == max2)
                return 0;

            return (max1 > max2) ? 1 : -1;
        }

        public static int InOrderOfMinElementInRow(int[] row1, int[] row2)
        {
            Validate(row1, row2);

            var min1 = row1.Min();
            var min2 = row2.Min();
            if (min1 == min2)
                return 0;

            return (min1 > min2) ? 1 : -1;
        }

        private static void Validate(int[] row1, int[] row2)
        {
            if (row1 == null || row2 == null)
            {
                throw new ArgumentNullException();
            }

            if (row1.Length == 0 || row2.Length == 0)
            {
                throw new ArgumentException("There is an empty row");
            }
        }
    }
}

