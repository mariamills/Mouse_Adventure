namespace StarterGame.Commands
{
    public class FleeCommand : Command
    {
        public FleeCommand() :base()
        {
            this.Name = "flee";
        }
        public override bool Execute(Player.Player player)
        {
            if (player.IsInCombat)
            {
                player.Flee();
            }
            else
            {
                player.ErrorMessage("This command can only be used while in combat.");
            }

            return false;
        }
    }
}