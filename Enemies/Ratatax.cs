namespace StarterGame.Enemies
{
    public class Ratatax : Enemy
    {
        public string Name { get; set; }
        public int ScareLevel { get; set; }
        public bool IsFriendly { get; set; }
        
        public Ratatax() : base("Ratatax", 100, false)
        {
        }

        public override void DoAttack(Player.Player player)
        {
            player.BattleMessage("RATATAX: Big mistake, mouse!");
            player.BattleMessage("Ratatax and his minions get you, you're swimming with the fishes now...");
        }
    }
}