namespace StarterGame.Commands
{
    /*
     * Spring 2023
     */
    public class HelpCommand : Command
    {
        private readonly CommandWords _words;

        public HelpCommand() : this(new CommandWords()){}

        // Designated Constructor
        public HelpCommand(CommandWords commands)
        {
            _words = commands;
            Name = "help";
        }

        override
        public bool Execute(Player.Player player)
        {
            if (this.HasSecondWord())
            {
                player.WarningMessage("\nI cannot help you with " + this.SecondWord);
            }
            else
            {
                // These commands/tips are subject to change
                // This is what my current ideas/plans are
                // Verbatim string literal
                player.InfoMessage(@"You are a little mouse that is taking on a big journey.

Your available commands are:
  - help: Display this help message.
  - hint: Display game hints.
  - go [direction]: Move in the specified direction (north, south, east, west).
  - whereami: Display the name of the current room.
  - back: Return to the previous room. Cannot be used after go through secret passages.
  - look [obj]: Search an object in the room for cheese.
  - cheese: Display the amount of cheese you currently have.
  - die: Lose a life and return to the last checkpoint. This was added for testing purposes but will remain in the game just for fun.
  - quit: Exit the game.
------COMBAT COMMANDS------
  - give [amount] [item]: Give an item(only cheese currently) from your inventory to an encountered enemy.
  - give [amount]: A shorthand for giving a specified amount of cheese to an encountered enemy.
  - bite: Attempt to bite the encountered enemy.
  - flee: Escape from a dangerous situation, going back to the previous room.
  - whistle: Whistle for help. This command has to be unlocked and can only be used ONCE. Choose when wisely.

Tips:
- Explore different rooms to find cheese and try to avoid enemies.
- Interact with objects in the room looking for cheese.
- Choose your actions wisely when encountering enemies.
- Find 10 cheese to win the game... or maybe there's another way to win?");
            }
            return false;
        }
    }
}




