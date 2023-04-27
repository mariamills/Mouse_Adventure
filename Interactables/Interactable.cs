using System;

namespace StarterGame.Interactables
{
    public abstract class Interactable
    {
        public string Name { get; }
        public string Description { get; }
        public int CheeseAmount { get; set; }

        protected Interactable(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}