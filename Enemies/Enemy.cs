using System;

namespace StarterGame.Enemies
{
    public abstract class Enemy
    {
        private static readonly Random Random = new Random();
        private const int ScaredChance = 5;
        public string Name { get; set; }
        public int ScaredLevel { get; set; }
        public bool IsFriendly { get; set; }

        protected Enemy(string name, int scaredLevel, bool isFriendly)
        {
            Name = name;
            ScaredLevel = scaredLevel;
            IsFriendly = isFriendly;
        }

        public abstract void OnEncounter(Player.Player player);
        public abstract void DoAttack(Player.Player player);
        public abstract void OnGive(Player.Player player, int amount);

        public void OnBite(Player.Player player)
        {
            if (IsFriendly)
            {
                player.BattleMessage(Name + " looks at you with disappointment and walks away.");
                player.CurrentRoom.Enemy = null;
                player.IsInCombat = false;
            }
            else
            {
                if (ScaredLevel < ScaredChance)
                {
                    player.AchievementManager.Notify("EnemyDefeated", player);
                    player.BattleMessage(Name + " has ran away screaming...");
                    player.CurrentRoom.Enemy = null;
                    player.IsInCombat = false;
                }
                else
                {
                    DoAttack(player);
                    player.Die();
                }
            }
        }


        public static int RandomNumber(int min, int max)
        {
            return Random.Next(min, max + 1);
        }
    }
}
