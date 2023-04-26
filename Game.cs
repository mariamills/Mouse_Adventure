using System.Collections;
using System.Collections.Generic;
using System;
using StarterGame.Achievements;
using StarterGame.Commands;

namespace StarterGame
{
    /*
     * Spring 2023
     */
    public class Game
    {
        private readonly Player.Player _player;
        private bool _playing;
        private readonly AchievementManager _achievementManager;
        private readonly Parser _parser;

        public Game()
        {
            _playing = false;
            _parser = new Parser(new CommandWords());
            _achievementManager = AchievementManager.Instance;
            _player = new Player.Player(GameWorld.Instance.CreateWorld());
            InitializeAchievements(); // register achievements
        }
        
        private void InitializeAchievements()
        {
            _achievementManager.RegisterObserver(new HomesickAchievement());
            _achievementManager.RegisterObserver(new NotBeginnersLuckAchievement());
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
            _player.ScanRoom();
        }

        public void End()
        {
            _playing = false;
            _player.InfoMessage(Goodbye());
        }

        private string Welcome()
        {
            return
                "Welcome to Cheese Tax!\n\nThe Mouse Mafia has imposed a harsh and unfair Cheese Tax on Mousetopia, leaving you owing them 10 cheese. Embark on a journey into Giant territory to find cheese and secure your future.\n\nBut maybe there might be another way to free Mousetopia from the Mouse Mafia's grip...\n\nType 'help' if you need help.\n" + _player.CurrentRoom.Details();
        }

        private string Goodbye()
        {
            return "\nThank you for playing, Goodbye. \n";
        }

    }
}
