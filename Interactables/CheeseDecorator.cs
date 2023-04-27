using System;

namespace StarterGame.Interactables
{
    public class CheeseDecorator : InteractableDecorator
    {
        private static readonly Random Random = new Random();
        public CheeseDecorator(Interactable interactable, double cheeseProbability) : base(interactable, interactable.Name, interactable.Description)
        {
            CheeseAmount = Random.NextDouble() < cheeseProbability / 100.0 ? Random.Next(1, 5) : 0;
        }
        
    }
}