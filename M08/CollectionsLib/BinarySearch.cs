using System;
using System.Collections.Generic;

namespace CollectionsLib
{
    public static class BinarySearch<T>
    {
        public static int Find(T[] array, T searchingElement, IComparer<T> comparer)
        {
            if (array == null || searchingElement == null || array.Length == 0)
            {
                throw new ArgumentNullException();
            }

            var first = 0;
            var last = array.Length;

            while (first <= last)
            {
                var middle = (first + last) / 2;

                switch (comparer.Compare(array[middle], searchingElement))
                {
                    case 0:
                        return middle;
                    case 1:
                        last = middle - 1;
                        break;
                    default:
                        first = middle + 1;
                        break;
                }
            }

            return -1;
        }
    }
}
