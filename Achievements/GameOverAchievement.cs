namespace StarterGame.Achievements
{
    public class GameOverAchievement : Achievement
    {
        public GameOverAchievement() : base("Dead Dead", "No more lives, you're dead dead.")
        {
            Unlocked = false;
        }
        public override void Update(string eventType, object data)
        {
            var player = (Player.Player) data;
            if (eventType == "DeadDead" && !Unlocked)
            {
                Achieve(player);
                Unlocked = true;
            }
        }
    }
}