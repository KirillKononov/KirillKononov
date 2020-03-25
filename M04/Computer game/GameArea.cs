namespace Computer_game
{
    class GameAreaCounter
    {
        public GameAreaCounter(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double AreaCount()
        {
            return Width * Height;
        }

        public double Width { get; }
        public double Height { get; }

    }
}
