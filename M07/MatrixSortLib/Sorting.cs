using System;

namespace MatrixSortLib
{
    public static class Sorting
    {
        public delegate int SortingType(int[] row1, int[] row2);

        public enum SortingDirection
        {
            Ascending,
            Descending
        }

        public static int[,] RowsSort(this int[,] matrix, SortingType sortingType, SortingDirection direction)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException();
            }

            for (var i = 0; i < matrix.GetLength(0) - 1; ++i)
            {
                for (var j = 0; j < matrix.GetLength(0) - i - 1; ++j)
                {
                    try
                    {

                        var compareResult = sortingType(GetRow(matrix, j), GetRow(matrix, j + 1));

                        if ((direction == SortingDirection.Ascending && compareResult == 1)
                            || (direction == SortingDirection.Descending && compareResult == -1))
                        {
                            SwapRows(ref matrix, j, j + 1);
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("Argument exception was caught from comparing method");
                        Console.WriteLine(ex);
                        Console.WriteLine("Throwing further");
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unregistered exception was caught from comparing method");
                        Console.WriteLine(ex);
                        Console.WriteLine("Throwing further");
                        throw;
                    }

                }
            }

            return matrix;
        }

        private static int[] GetRow(int[,] matrix, int index)
        {
            var row = new int[matrix.GetLength(1)];
            for (var i = 0; i < matrix.GetLength(1); ++i)
            {
                row[i] = matrix[index, i];
            }

            return row;
        }

        private static void SwapRows(ref int[,] matrix, int row1, int row2)
        {
            for (var i = 0; i < matrix.GetLength(1); ++i)
            {
                var temp = matrix[row1, i];
                matrix[row1, i] = matrix[row2, i];
                matrix[row2, i] = temp;
            }
        }

    }
}
