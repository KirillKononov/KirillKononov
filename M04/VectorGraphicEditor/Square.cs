namespace VectorGraphicEditor
{
    class Square : Rectangle
    {
        public Square(double side) : base(side, side)
        {
            Side = side;
            Shape = "Square";
        }

        public double Side { get; }
    }
}
