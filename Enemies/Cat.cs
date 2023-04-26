namespace StarterGame.Enemies
{
    public class Cat : Enemy
    {
        public string Name { get; set; }
        public int ScareLevel { get; set; }
        public bool IsFriendly { get; set; }

        public Cat() : base("Cat", Enemy.RandomNumber(1, 10), true)
        {
        }

        public override void DoAttack(Player.Player player)
        {
            player.BattleMessage(Name + " scratches you.");
        }
    }
}