namespace StarterGame.Commands
{
    public class GiveCommand : Command
    {
        public GiveCommand() : base()
        {
            Name = "give";
        }

        public override bool Execute(Player.Player player)
        {
            if (HasSecondWord())
            {
                player.Give(SecondWord);
            } else if (HasThirdWord())
            {
                player.Give(SecondWord, ThirdWord);
            }
            else
            {
                player.ErrorMessage("\nGive what?\nExample: 'give 5 cheese'");
            }
            return false;
        }
    }
}