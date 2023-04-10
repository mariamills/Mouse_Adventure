namespace StarterGame.Commands
{
    /*
     * Spring 2023
     */
    public class QuitCommand : Command
    {

        public QuitCommand() : base()
        {
            this.Name = "quit";
        }

        override
        public bool Execute(Player player)
        {
            bool answer = true;
            if (this.HasSecondWord())
            {
                player.WarningMessage("\nI cannot quit " + this.SecondWord);
                answer = false;
            }
            return answer;
        }
    }
}
