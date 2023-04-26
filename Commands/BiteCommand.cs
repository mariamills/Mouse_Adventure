using Microsoft.VisualBasic;

namespace StarterGame.Commands
{
    public class BiteCommand : Command
    {
        public BiteCommand() : base()
        {
            this.Name = "bite";
        }
        public override bool Execute(Player.Player player)
        {
            if (player.IsInCombat)
            {
                player.Bite();
            }
            else
            {
                player.ErrorMessage("This command can only be used while in combat.");
            }

            return false;
        }
    }
}