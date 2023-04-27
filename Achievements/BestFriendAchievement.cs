namespace StarterGame.Achievements
{
    public class BestFriendAchievement : Achievement
    {
        public BestFriendAchievement() : base("Best Friend", "You made a new friend! You can now use the whistle command if you ever need help!")
        {
            Unlocked = false;
        }
        public override void Update(string eventType, object data)
        {
            var player = (Player.Player) data;
            if (eventType == "FedDog" && !Unlocked)
            {
                Achieve(player);
                Unlocked = true;
            }
        }
    }
}