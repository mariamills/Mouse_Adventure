using System;

namespace StarterGame.Interactables
{
    public class Interactable
    {
        private static readonly Random Random = new Random();
        public string Name { get; }
        public string Description { get; }
        public int CheeseAmount { get; set; }

        public Interactable(string name, string description, int cheeseAmount = 0)
        {
            Name = name;
            Description = description;
            CheeseAmount = cheeseAmount;
        }
        
        public static Interactable CreateCheeseInteractable(string name, string description, double cheeseProbability)
        {
            int cheeseAmount = 0;

            if (Random.NextDouble() < cheeseProbability / 100.0)
            {
                cheeseAmount = Random.Next(1, 4); // Generate a random cheese amount between 1 and 3.
            }

            return new Interactable(name, description, cheeseAmount);
        }
    }
}