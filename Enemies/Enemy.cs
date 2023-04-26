using System;

namespace StarterGame.Enemies
{
    public abstract class Enemy
    {
        private static readonly Random Random = new Random();
        public string Name { get; set; }
        public int ScareLevel { get; set; }
        public bool IsFriendly { get; set; }

        public Enemy(string name, int scareLevel, bool isFriendly)
        {
            Name = name;
            ScareLevel = scareLevel;
            IsFriendly = isFriendly;
        }

        public abstract void DoAttack(Player.Player player);


        public static int RandomNumber(int min, int max)
        {
            return Random.Next(min, max + 1);
        }
    }
}
