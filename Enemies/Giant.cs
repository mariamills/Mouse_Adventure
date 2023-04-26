namespace StarterGame.Enemies
{
    public class Giant : Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public bool IsFriendly { get; set; }
        
        public Giant() : base("Giant", 10, 5, 5)
        {
            IsFriendly = false;
        }
    }
}