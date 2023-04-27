namespace StarterGame.Achievements
{
    public class FearlessAchievement : Achievement
    {
        public FearlessAchievement() : base("Fearless", "Defeat an enemy.")
        {
        }
        public override void Update(string eventType, object data)
        {
            var player = (Player.Player) data;
            if (eventType == "EnemyDefeated")
            {
                Achieve(player);
                Unlocked = true;
            }
        }
    }
}