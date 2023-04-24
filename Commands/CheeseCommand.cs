namespace StarterGame.Commands
{
    public class CheeseCommand : Command
    {
        
        public CheeseCommand()
        {
            Name = "cheese";
        }
        public override bool Execute(Player.Player player)
        {
            player.InfoMessage("You have " + player.Currency + " cheese.");
            return false;
        }
    }
}