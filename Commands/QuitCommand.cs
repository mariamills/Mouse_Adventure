namespace StarterGame.Commands
{
    /*
     * Spring 2023
     */
    public class QuitCommand : Command
    {

        public QuitCommand() : base()
        {
            Name = "quit";
        }

        override
        public bool Execute(Player.Player player)
        {
            bool answer = true;
            if (HasSecondWord())
            {
                player.WarningMessage("\nI cannot quit " + this.SecondWord);
                answer = false;
            }
            return answer;
        }
    }
}
