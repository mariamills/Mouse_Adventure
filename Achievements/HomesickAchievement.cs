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
            if (eventType == "RoomChange" && !Unlocked)
            {
                var player = (Player) data;
                if (player.CurrentRoom.Name != "Mousetopia")
                {
                    Achieve(player);
                }
            }
        }
    }
}