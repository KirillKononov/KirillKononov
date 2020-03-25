using System;

namespace Computer_game.Bonuses
{
    abstract class Bonus : IPositioned
    {
        protected Bonus(string bonusName, int scorePoint, int x, int y)
        {
            BonusName = bonusName;
            ScorePoint = scorePoint;
            X = x;
            Y = y;
        }

        public void GetPosition()
        {
            Console.WriteLine($"Bonus {BonusName} has {ScorePoint} points " +
                              $"and it's located at {X} width and {Y} height");
        }
        public string BonusName { get; }
        public int ScorePoint { get; }
        public int X { get; }
        public int Y { get; }
    }
}
