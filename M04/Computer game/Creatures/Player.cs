using Computer_game.Bonuses;

namespace Computer_game.Creatures
{
    class Player : Creature
    {
        public Player(string name) : base (name, 0, 0, 0)
        {
            Score = 0;
        }

        private void PickUpBonus(Bonus bonus)
        {
            Score += bonus.ScorePoint;
        }

        public int Score { get; set; }
    }
}
