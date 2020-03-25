using System;

namespace Computer_game.Creatures
{
    abstract class Creature : IPositioned
    {
        protected Creature(string name, int damage, int x, int y)
        {
            Name = name;
            Damage = damage;
            X = x;
            Y = y;
        }

        public void Move()
        {
            //moving algorithm
        }
        public void GetPosition()
        {
            Console.WriteLine($"Creature {Name} has {Damage} points of damage " +
                              $"and it's located at {X} width and {Y} height");
        }
        public string Name { get; }
        public int Damage { get; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
