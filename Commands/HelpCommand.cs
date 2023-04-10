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
        public HelpCommand(CommandWords commands) : base()
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
                player.InfoMessage(@"You are a little mouse that is taking on a big journey.

Your available commands are:
  - help: Display this help message.
  - go [direction]: Move in the specified direction (north, south, east, west).
  - whereami: Display the name of the current room.
  - look: Examine your surroundings and find objects or enemies in the room.
  - take [item]: Pick up an item and add it to your inventory.
  - use [item]: Use an item from your inventory.
  - inventory: Display the items currently in your inventory.
  - bite: Attempt to bite the encountered enemy.
  - give [item]: Give an item from your inventory to an encountered enemy.
  - flee: Attempt to escape from a dangerous situation.
  - save: Save your current game progress.
  - load: Load your saved game.
  - quit: Exit the game.

Tips:
- Explore different rooms to find cheese, items, and complete quests.
- Interact with fellow mice and enemies to learn more about the game world.
- Choose your actions wisely when encountering enemies; some options may have a higher chance of success than others.
- Remember to save your progress regularly, especially after reaching checkpoints or completing important tasks.");
            }
            return false;
        }
    }
}




