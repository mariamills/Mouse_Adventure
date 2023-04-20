namespace StarterGame.Achievements
{
    public class NotBeginnersLuckAchievement : Achievement
    {
        public NotBeginnersLuckAchievement() : base("Beginner's Luck...Not!", "Die for the first time.")
        {
            Unlocked = false;
        }
        public override void Update(string eventType, object data)
        {
            if(eventType == "PlayerDeath" && !Unlocked)
            {
                var player = (Player) data;
                if (player.Deaths == 1)
                {
                    Achieve(player);
                }
            }
        }
    }
}