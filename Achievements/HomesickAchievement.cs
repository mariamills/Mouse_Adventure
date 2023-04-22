namespace StarterGame.Achievements
{
    public class HomesickAchievement : Achievement
    {
        public HomesickAchievement() : base("Homesick", "Leave your home for the first time")
        {
            Unlocked = false;
        }
        public override void Update(string eventType, object data)
        {
            var player = (Player) data;
            if (eventType == "RoomChange" && !Unlocked)
            {
                if (player.CurrentRoom.Tag.Contains("bathroom"))
                { 
                    Achieve(player);
                    Unlocked = true;
                } 
            }
        }
    }
}