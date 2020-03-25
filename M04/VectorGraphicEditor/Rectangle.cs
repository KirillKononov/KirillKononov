namespace VectorGraphicEditor
{
    class Rectangle : GeometricFigure
    {
        public Rectangle(double width, double height) : base("Rectangle")
        {
            Width = width;
            Height = height;
        }

        public override double Area()
        {
            return Width * Height;
        }

        public override double Perimeter()
        {
            return (Width + Height) * 2;
        }

        public double Width { get; }
        public double Height { get; }

    }
}
