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
                var player = (Player.Player) data;
                if (player.Lives == 2)
                {
                    Achieve(player);
                    Unlocked = true;
                }
            }
        }
    }
}