namespace StarterGame.Enemies
{
    public class Cat : Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public bool IsFriendly { get; set; }

        public Cat() : base("Cat", 10, 5, 5)
        {
            IsFriendly = false;
        }
        
        public void Encounter(Player.Player player)
        {
            if (IsFriendly)
            {
                player.NormalMessage("\n" + Name + " is friendly and will not attack you.");
            }
            else
            {
                player.NormalMessage("\n" + Name + " is not friendly and will attack you.");
            }
        }

    }
}