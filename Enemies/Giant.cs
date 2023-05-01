namespace StarterGame.Enemies
{
    public class Giant : Enemy
    {
        public Giant() : base("Giant", Enemy.RandomNumber(0, 10), false)
        {
        }
        
        public override void OnEncounter(Player.Player player)
        {
            player.BattleMessage("Combat Mode enabled.");
            player.WarningMessage($"A {Name} is coming towards you. What will you do?");
        }
        
        public override void DoAttack(Player.Player player)
        {
            player.BattleMessage(Name + " steps on you.");
        }

        public override void OnGive(Player.Player player, int amount)
        {
            player.BattleMessage("I don't think the " + Name + " wants that.");
        }
    }
}