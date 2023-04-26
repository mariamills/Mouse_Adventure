namespace StarterGame.Enemies
{
    public class Giant : Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public bool IsFriendly { get; set; }
        
        public Giant() : base("Giant", Enemy.RandomNumber(0, 10), false)
        {
        }
        
        public override void DoAttack(Player.Player player)
        {
            player.BattleMessage(Name + " steps on you.");
        }
    }
}