using System;

namespace StarterGame.Enemies
{
    public class EnemyFactory
    {
        public static readonly Random Random = new Random();
        public static Enemy CreateEnemy(string enemyType)
        {
            Enemy enemy = null;
            switch (enemyType)
            {
                case "Giant":
                    enemy = new Giant();
                    break;
                case "Dog":
                    enemy = new Dog();
                    break;
                case "Cat":
                    enemy = new Cat();
                    break;
            }
            return enemy;
        }
    }
}