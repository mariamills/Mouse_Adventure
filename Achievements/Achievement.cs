using System;

namespace StarterGame.Achievements
{
    public abstract class Achievement
    {
        private string Name { get; }
        private string Description { get; }
        protected bool Unlocked { get; set; }

        protected Achievement(string name = "No Name", string description = "No Description")
        {
            Name = name;
            Description = description;
            Unlocked = false;
        }
        
        protected void Achieve(Player player)
        {
            if (!Unlocked)
            {
                Unlocked = true;
                player.AchieveMessage("Achievement Unlocked: " + Name + " - " + Description);
            }
        }
        
        public abstract void Update(string eventType, object data);

    }
}