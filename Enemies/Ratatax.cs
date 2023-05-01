namespace StarterGame.Enemies
{
    public class Ratatax : Enemy
    {
        private const int CheeseWanted = 10;
        private int _annoyedRatatax = 0;
        private int _cheeseGiven = 0;
        
        public Ratatax() : base("Ratatax", 100, false)
        {
        }

        public override void OnEncounter(Player.Player player)
        {
            
            player.BattleMessage($">>>You walk towards {Name}, all the mice in the room are watching you.");
            player.BattleMessage("RATATAX: Here to pay my CHEESE TAX?");
        }

        public override void DoAttack(Player.Player player)
        {
            player.BattleMessage("RATATAX: Big mistake, mouse!");
            player.BattleMessage("Ratatax and his minions get you, you're swimming with the fishes now...");
        }

        public override void OnGive(Player.Player player, int amount)
        {
            _cheeseGiven += amount;
            
            if (_annoyedRatatax == 3)
            {
                player.BattleMessage("RATATAX: You've annoyed me mouse. Now you'll have to pay with your life.");
                player.EvenWorseEnd();
            }
            if (_cheeseGiven < CheeseWanted)
            {
                player.BattleMessage("RATATAX: That's not enough! You know my cheese tax is 10 cheese! Pay up!");
                _annoyedRatatax++;
            }
            else
            {
                player.AchievementManager.Notify("PaidCheeseTax", player);
                player.BattleMessage(">>>You gave " + amount + " cheese to Ratatax.");
                player.NormalMessage(">>>You paid the cheese tax.... You've avoid the Mouse Mafia for now...but what will happen next time?");
                player.IsInCombat = false;
                player.BadEnd();
            }
        }
    }
}