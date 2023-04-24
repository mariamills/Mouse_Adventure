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
        public bool Execute(Player player)
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
  - go [direction]: Move in the specified direction (north, south, east, west).
  - whereami: Display the name of the current room.
  - die: Lose a life and return to the last checkpoint.
  - look: Examine your surroundings and find objects.
  - cheese: Display the amount of cheese you currently have.
  - give [item]: Give an item from your inventory to an encountered enemy.
  - bite: Attempt to bite the encountered enemy.
  - flee: Attempt to escape from a dangerous situation.
  - quit: Exit the game.

Tips:
- Explore different rooms to find cheese and try to avoid enemies.
- Interact with objects in the room looking for cheese.
- Choose your actions wisely when encountering enemies.");
            }
            return false;
        }
    }
}




