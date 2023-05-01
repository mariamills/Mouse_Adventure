namespace StarterGame.Enemies
{
    public class Dog : Enemy
    {
        private readonly int _cheeseWanted = 5;
        private int _cheeseGiven = 0;

        public Dog() : base("Dog", Enemy.RandomNumber(0, 10), true)
        {
        }
        
        public override void OnEncounter(Player.Player player)
        {
            player.BattleMessage("Combat Mode enabled.");
            player.WarningMessage($"A {Name} is coming towards you. What will you do?");
        }
        
        public override void DoAttack(Player.Player player)
        {
            player.BattleMessage(Name + " bites you.");
        }
        
        public override void OnGive(Player.Player player, int amount)
        {
            _cheeseGiven += amount;
            if (_cheeseGiven >= _cheeseWanted)
            {
                player.AchievementManager.Notify("FedDog", player);
                player.NormalMessage(">>>You gave the dog some cheese. He wags his tail happily. You made a friend.");
                player.IsInCombat = false;
            }
            else
            {
                player.BattleMessage(">>>You gave the dog " + amount + " cheese. He looks at you with disappointment. He wants more cheese.");
            }
        }
    }
}