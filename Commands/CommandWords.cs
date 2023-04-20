using System.Collections.Generic;

namespace StarterGame.Commands
{
    /*
     * Spring 2023
     */
    public class CommandWords
    {
        private readonly Dictionary<string, Command> _commands;
        private static readonly Command[] CommandArray = { new GoCommand(), new QuitCommand(), new WhereAmICommand(), new CheeseCommand(), new DieCommand() };

        public CommandWords() : this(CommandArray) {}

        // Designated Constructor
        public CommandWords(Command[] commandList)
        {
            _commands = new Dictionary<string, Command>();
            foreach (Command command in commandList)
            {
                _commands[command.Name] = command;
            }
            Command help = new HelpCommand(this);
            _commands[help.Name] = help;
        }

        public Command Get(string word)
        {
            Command command = null;
            _commands.TryGetValue(word, out command);
            return command;
        }

        public string Description()
        {
            string commandNames = "";
            Dictionary<string, Command>.KeyCollection keys = _commands.Keys;
            foreach (string commandName in keys)
            {
                commandNames += " " + commandName;
            }
            return commandNames;
        }
    }
}
