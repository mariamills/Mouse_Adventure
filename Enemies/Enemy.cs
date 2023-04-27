using System;

namespace StarterGame.Enemies
{
    public abstract class Enemy
    {
        private static readonly Random Random = new Random();
        public string Name { get; set; }
        public int ScaredChance { get; set; }
        public bool IsFriendly { get; set; }

        public Enemy(string name, int scaredChance, bool isFriendly)
        {
            Name = name;
            ScaredChance = scaredChance;
            IsFriendly = isFriendly;
        }

        public abstract void DoAttack(Player.Player player);


        public static int RandomNumber(int min, int max)
        {
            return Random.Next(min, max + 1);
        }
    }
}
