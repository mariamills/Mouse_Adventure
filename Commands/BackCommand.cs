using System.ComponentModel.Design.Serialization;

namespace StarterGame.Commands
{
    public class BackCommand : Command
    {
        public BackCommand()
        {
            Name = "back";
        }
        public override bool Execute(Player.Player player)
        {
            player.Back();
            return false;
        }
    }
}