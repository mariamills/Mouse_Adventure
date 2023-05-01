namespace StarterGame.Enemies
{
    public class Cat : Enemy
    {
        public Cat() : base("Cat", Enemy.RandomNumber(0, 7), false)
        {
        }
        
        public Cat(int scaredLevel) : base("Cat", scaredLevel, false)
        {
        }

        public override void OnEncounter(Player.Player player)
        {
            player.BattleMessage("Combat Mode enabled.");
            player.WarningMessage($"A {Name} is coming towards you. What will you do?");
        }

        public override void DoAttack(Player.Player player)
        {
            player.BattleMessage(Name + " scratches you.");
        }

        public override void OnGive(Player.Player player, int amount)
        {
            player.BattleMessage("I don't think the " + Name + " wants that.");
        }
    }
}