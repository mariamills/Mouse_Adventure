using System;

namespace StarterGame.Achievements
{
    public abstract class Achievement
    {
        private string Name { get; }
        private string Description { get; }
        public bool Unlocked { get; set; }

        protected Achievement(string name = "No Name", string description = "No Description")
        {
            Name = name;
            Description = description;
            Unlocked = false;
        }
        
        protected void Achieve(Player.Player player)
        {
            if (!Unlocked)
            {
                Unlocked = true;
                player.AchieveMessage("🎖️Achievement Unlocked: [" + Name + "] - " + Description);
                player.AchieveMessage("🎖️You have been awarded 1 cheese.");
                player.Currency++;
            }
        }
        
        public abstract void Update(string eventType, object data);

    }
}