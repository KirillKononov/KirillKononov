using System;

namespace Computer_game.Obstacles
{
    abstract class Obstacle : IPositioned
    {
        protected Obstacle(string obstacleType, int x, int y)
        {
            ObstacleType = obstacleType;
            X = x;
            Y = y;
        }
        public void GetPosition()
        {
            Console.WriteLine($"Obstacle {ObstacleType} is located at {X} width and {Y} height");
        }

        public string ObstacleType { get; }
        public int X { get; }
        public int Y { get; }
    }
}
