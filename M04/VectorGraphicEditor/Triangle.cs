using System;

namespace VectorGraphicEditor
{
    class Triangle : GeometricFigure
    {
        public Triangle(double a, double b, double c) : base("Triangle")
        {
            A = a;
            B = b;
            C = c;
        }

        public override double Area()
        {
            var halfPerimeter = Perimeter() / 2;
            if (halfPerimeter == A || halfPerimeter == B || halfPerimeter == C)
            {
                throw new ArgumentException("Printed sides of triangle are incorrect");
            }
            return Math.Sqrt(halfPerimeter * (halfPerimeter - A) * (halfPerimeter - B) * (halfPerimeter - C));
        }

        public override double Perimeter()
        {
            return A + B + C;
        }

        public double A { get; }
        public double B { get; }
        public double C { get; }

    }
}
