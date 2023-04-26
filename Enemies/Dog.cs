namespace StarterGame.Enemies
{
    public class Dog : Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public bool IsFriendly { get; set; }

        public Dog() : base("Dog", 3, true)
        {
        }
        
        public override void DoAttack(Player.Player player)
        {
            player.BattleMessage(Name + " bites you.");
        }
    }
}