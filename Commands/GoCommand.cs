namespace StarterGame.Commands
{
    /*
     * Spring 2023
     */
    public class GoCommand : Command
    {

        public GoCommand() : base()
        {
            Name = "go";
        }

        override
        public bool Execute(Player.Player player)
        {
            if (HasSecondWord())
            {
                player.WalkTo(SecondWord);
            }
            else
            {
                player.WarningMessage("\nGo Where?");
            }
            return false;
        }
    }
}
