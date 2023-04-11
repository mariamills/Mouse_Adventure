namespace StarterGame.Achievements
{
    public class HomesickAchievement : Achievement
    {
        private bool _isAchieved;
        
        public HomesickAchievement() : base("Homesick", "Leave your home for the first time")
        {
            _isAchieved = false;
        }
        public override void Update(string eventType, object data)
        {
            if (eventType == "RoomChange" && !_isAchieved)
            {
                var player = (Player) data;
                if (player.CurrentRoom.Name != "Mousetopia")
                {
                    _isAchieved = true;
                    Achieve(player);
                }
            }
        }
    }
}