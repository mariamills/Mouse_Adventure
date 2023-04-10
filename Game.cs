using System.Collections;
using System.Collections.Generic;
using System;
using StarterGame.Commands;

namespace StarterGame
{
    /*
     * Spring 2023
     */
    public class Game
    {
        private Player _player;
        private readonly Parser _parser;
        private bool _playing;

        public Game()
        {
            _playing = false;
            _parser = new Parser(new CommandWords());
            _player = new Player(CreateWorld());
        }

        private static Room CreateWorld()
        {
            Room mousetopia = new Room("in the comfort of Mousetopia", "Mousetopia");
            Room sewerEntrance = new Room("at the entrance of the sewer", "Sewer Entrance");
            Room sewer = new Room("in the sewer, connecting Mousetopia to the Giant's territory", "Sewer");
            Room pipeHub = new Room("in the pipe hub, where multiple pipes lead to different Giant's houses", "Pipe Hub");
            Room orangeHouse = new Room("in a orange bathroom", "Pipe 1");
            Room greenHouse = new Room("in a green bathroom", "Pipe 2");
            Room redHouse = new Room("in a red bathroom", "Pipe 3");
            
            mousetopia.SetExit("south", sewerEntrance);
            
            sewerEntrance.SetExit("north", mousetopia);
            sewerEntrance.SetExit("south", sewer);
            
            sewer.SetExit("north", sewerEntrance);
            sewer.SetExit("south", pipeHub);
            
            pipeHub.SetExit("north", sewer);
            pipeHub.SetExit("east", orangeHouse);
            pipeHub.SetExit("west", greenHouse);
            pipeHub.SetExit("south", redHouse);
            
            return mousetopia;
        }

        /**
     *  Main play routine.  Loops until end of play.
     */
        public void Play()
        {

            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.

            bool finished = false;
            while (!finished)
            {
                Console.Write("\n>");
                Command command = _parser.ParseCommand(Console.ReadLine());
                if (command == null)
                {
                    _player.ErrorMessage("I don't understand...");
                }
                else
                {
                    finished = command.Execute(_player);
                }
            }
        }


        public void Start()
        {
            _playing = true;
            _player.InfoMessage(Welcome());
        }

        public void End()
        {
            _playing = false;
            _player.InfoMessage(Goodbye());
        }

        private string Welcome()
        {
            return "Welcome to Cheese Tax!\n\nEmbark on a journey into Giant territory in search of the precious cheese. Can you escape your enemies and secure your family's future?\n\nType 'help' if you need help. " + _player.CurrentRoom.Description();
        }

        private string Goodbye()
        {
            return "\nThank you for playing, Goodbye. \n";
        }

    }
}
