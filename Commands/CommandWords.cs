using System.Collections.Generic;

namespace StarterGame.Commands
{
    /*
     * Spring 2023
     */
    public class CommandWords
    {
        private Dictionary<string, Command> _commands;
        private static Command[] _commandArray = { new GoCommand(), new QuitCommand() };

        public CommandWords() : this(_commandArray) {}

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
