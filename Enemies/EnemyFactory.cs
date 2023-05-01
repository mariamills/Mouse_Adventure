using System;

namespace StarterGame.Enemies
{
    public abstract class EnemyFactory
    {
        public static readonly Random Random = new Random();
        public static Enemy CreateEnemy(string enemyType)
        {
            Enemy enemy = null;
            switch (enemyType)
            {
                case "Ratatax":
                    enemy = new Ratatax();
                    break;
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