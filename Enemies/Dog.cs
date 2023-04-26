namespace StarterGame.Enemies
{
    public class Dog : Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public bool IsFriendly { get; set; }
        
        public Dog() : base("Dog", 10, 5, 5)
        {
            IsFriendly = true;
        }
    }
}