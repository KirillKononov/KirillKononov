using System;
using MatrixSortLib;
using SubscriptionLib;

namespace M07
{
    class Program
    {
        private static void PrintMatrix(int[,] mass)
        {
            for (var i = 0; i < mass.GetLength(0); ++i)
            {
                for (var j = 0; j < mass.GetLength(1); ++j)
                {
                    Console.Write($"{mass[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        private static void MatrixSort()
        {
            var matrix = new int[4, 2] { { 2, 7 }, { 5, 6 }, { 9, 4 }, { 2, 0 } };
            Console.WriteLine("We have a matrix:");
            PrintMatrix(matrix);

            Console.WriteLine("How do you want to sort it?");
            Console.WriteLine("1. In ascending order of sums of matrix row elements");
            Console.WriteLine("2. In descending order of sums of matrix row elements");
            Console.WriteLine();
            Console.WriteLine("3. In ascending order of maximum element in a matrix row");
            Console.WriteLine("4. In descending order of maximum element in a matrix row");
            Console.WriteLine();
            Console.WriteLine("5. In ascending order of minimum element in a matrix row");
            Console.WriteLine("6. In descending order of minimum element in a matrix row");
            Console.WriteLine();
            Console.WriteLine("Please, choose the one number of type of sorting:");

            var sortType = Console.ReadLine();

            switch (sortType)
            {
                case "1":
                    matrix.RowsSort(SortingType.InOrderOfSumsOfRowElements,
                        Sorting.SortingDirection.Ascending);
                    break;
                case "2":
                    matrix.RowsSort(SortingType.InOrderOfSumsOfRowElements,
                        Sorting.SortingDirection.Descending);
                    break;
                case "3":
                    matrix.RowsSort(SortingType.InOrderOfMaxElementInRow,
                        Sorting.SortingDirection.Ascending);
                    break;
                case "4":
                    matrix.RowsSort(SortingType.InOrderOfMaxElementInRow,
                        Sorting.SortingDirection.Descending);
                    break;
                case "5":
                    matrix.RowsSort(SortingType.InOrderOfMinElementInRow,
                        Sorting.SortingDirection.Ascending);
                    break;
                case "6":
                    matrix.RowsSort(SortingType.InOrderOfMinElementInRow,
                        Sorting.SortingDirection.Descending);
                    break;
                default:
                    throw new ArgumentException("The invalid number was printed");
            }

            Console.WriteLine("Sorted matrix:");
            PrintMatrix(matrix);
        }

        static void Main(string[] args)
        {
            try
            {
                MatrixSort();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }

            Console.WriteLine();

            var channel = new Channel("EPAM");
            var subscriber = new Subscriber("Peter");

            channel.AddSubscriber(subscriber);
            channel.SendNotification(5000);
            channel.DeleteSubscriber(subscriber);

            Console.ReadLine();
        }
    }
}
