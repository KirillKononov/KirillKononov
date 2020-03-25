using System;

namespace VectorGraphicEditor
{
    class Circle : GeometricFigure
    {
        public Circle (double radius) : base("Circle")
        {
            Radius = radius;
        }

        public override double Area()
        {
            return Pi * Math.Pow(Radius, 2);
        }

        public override double Perimeter()
        {
            return 2 * Pi * Radius;
        }

        public double Radius { get; }
        private const double Pi = 3.14;
    }
}
