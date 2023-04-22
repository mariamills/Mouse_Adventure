namespace StarterGame
{
    public class Interactable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CheeseAmount { get; set; }

        public Interactable(string name, string description, int cheeseAmount = 0)
        {
            Name = name;
            Description = description;
            CheeseAmount = cheeseAmount;
        }
    }
}