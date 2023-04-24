namespace StarterGame.Commands
{
    public class LookCommand : Command
    {
        public LookCommand() : base()
        {
            Name = "look";
        }
        public override bool Execute(Player.Player player)
        {
            if (HasSecondWord())
            {
                player.Look(SecondWord);
            }
            else
            {
                player.ErrorMessage("Look what at?");
            }
            return false;
        }
    }
}