using System;

namespace StarterGame.Enemies
{
    public abstract class Enemy
    {
        private static readonly Random Random = new Random();
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public Enemy(string name, int health, int attack, int defense)
        {
            Name = name;
            Health = health;
            Attack = attack;
            Defense = defense;
        }

        public void Encounter(Player.Player player)
        {
            // Implement encounter logic here.
        }

        public void GiveCheese(Player.Player player)
        {
            // Implement give cheese logic here.
        }

        public void Bite(Player.Player player)
        {
            // Implement bite logic here.
        }

        public void Flee(Player.Player player)
        {
            // Implement flee logic here.
        }
        
        public static int RandomNumber(int min, int max)
        {
            return Random.Next(min, max + 1);
        }
    }
}
