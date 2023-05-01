namespace StarterGame.Commands
{
    public class HintCommand : Command
    {
        public HintCommand() : base()
        {
            this.Name = "hint";
        }
        public override bool Execute(Player.Player player)
        {
            player.InfoMessage(@">>>Hints:

>>>You can use the 'go [direction]' command to move around the map.
>>>You can use the 'look [object]' command to look at something you see in your current location.
>>>Achievements give you cheese, but you can only get them once.
>>>Sometimes, it's better to avoid a fight rather than engage in combat. You can use the 'flee' command to escape from a dangerous situation.
>>>If an enemy is too strong, best to avoid that area.
>>>Combat commands are only available when you encounter an enemy. The text will be Magenta when you are in combat mode.
>>>Not every room has cheese, you don't have to search every room.
>>>There could be secret tunnels somewhere... why not give them a 'go' by using the 'go [object]' command?
>>>Paying the Cheese Tax can't be the only way to win the game, right...?
            
            ");
            return false;
        }
    }
}