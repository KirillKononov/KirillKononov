namespace VectorGraphicEditor
{
    abstract class GeometricFigure
    {
        protected GeometricFigure(string shape)
        {
            Shape = shape;
        }

        public abstract double Area();
        public abstract double Perimeter();
        public string Shape { get; set; }
    }
}
