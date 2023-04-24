namespace StarterGame.Commands
{
    public class WhereAmICommand : Command
    {
        public WhereAmICommand() : base()
        {
            Name = "whereami";
        }

        public override bool Execute(Player.Player player)
        {
            player.OutputMessage("\n" + player.CurrentRoom.Details());
            return false;
        }
    }
}