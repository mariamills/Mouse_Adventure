namespace StarterGame.Commands
{
    public class DieCommand : Command
    {
        public DieCommand() : base()
        {
            Name = "die";
        }

        public override bool Execute(Player player)
        {
            player.Die();
            return false;
        }
    }
}