namespace StarterGame.Commands
{
    /*
     * Spring 2023
     */
    public class GoCommand : Command
    {

        public GoCommand() : base()
        {
            this.Name = "go";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.WaltTo(this.SecondWord);
            }
            else
            {
                player.WarningMessage("\nGo Where?");
            }
            return false;
        }
    }
}
