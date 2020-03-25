using System;

namespace VectorGraphicEditor
{
    class FiguresFactory
    {
        private static double ReadLine()
        {
            var input = Console.ReadLine();
            if (!double.TryParse(input, out _))
            {
                throw new ArgumentException("Printed string isn't a number");
            }

            return double.Parse(input ?? throw new InvalidOperationException());
        }

        public Circle CreateCircle()
        {
            Console.WriteLine("Print the radius of the circle:");
            var radius = ReadLine();
            return new Circle(radius);
        }

        public Rectangle CreateRectangle()
        {
            Console.WriteLine("Print the width and the height of the rectangle " +
                              "(WIDTH ON THE FIRST LINE AND HEIGHT ON THE SECOND):");
            var width = ReadLine();
            var height = ReadLine();
            return new Rectangle(width, height);
        }

        public Square CreateSquare()
        {
            Console.WriteLine("Print the side of the square: ");
            var side = ReadLine();
            return new Square(side);
        }

        public Triangle CreateTriangle()
        {
            Console.WriteLine("Print the lengths of three sides of triangle (ONE SIDE IN ONE LINE):");
            var a = ReadLine();
            var b = ReadLine();
            var c = ReadLine();
            return new Triangle(a, b, c);
        }
    }
}
