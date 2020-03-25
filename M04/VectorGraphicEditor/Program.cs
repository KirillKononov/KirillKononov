using System;
using System.Collections.Generic;


namespace VectorGraphicEditor
{
    class Program
    {
        public static List<GeometricFigure> CreateFigures()
        {
            Console.WriteLine("1. Circle\n2. Rectangle\n3. Square\n4. Triangle");
            Console.WriteLine("Print the numbers of the figures which you want to create:");
            var numbers = Console.ReadLine() ?? throw new ArgumentNullException();
            var figures = new List<GeometricFigure>();
            var factory = new FiguresFactory();

            foreach (var figure in numbers)
            {
                switch (figure)
                {
                    case '1': 
                        figures.Add(factory.CreateCircle());
                        break;
                    case '2':
                        figures.Add(factory.CreateRectangle());
                        break;
                    case '3':
                        figures.Add(factory.CreateSquare());
                        break;
                    case '4':
                        figures.Add(factory.CreateTriangle());
                        break;
                    default:
                        throw new ArgumentException("The invalid number was printed");
                }
            }

            return figures;
        }

        public static void PrintFigures(List<GeometricFigure> figures)
        {
            foreach (var figure in figures)
            {
                Console.WriteLine($"The figure {figure.Shape} has the area equals {figure.Area()} " +
                                  $"and the perimeter equals {figure.Perimeter()}");
            }

            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            try
            {
                var figures = CreateFigures();
                PrintFigures(figures);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
    }
}
