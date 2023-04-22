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
    public class Player
    {
        public Room CurrentRoom { get; set; }

        public int Currency { get; set; }
        public int Lives { get; private set; }

        private readonly History _playerHistory;
        private readonly AchievementManager _achievementManager = AchievementManager.Instance;

        public Player(Room room)
        {
            CurrentRoom = room;
            Lives = 3;
            Currency = 0;
            _playerHistory = new History();
            _playerHistory.SaveState(CreateState());
        }

        public void WalkTo(string direction)
        {
            Room nextRoom = CurrentRoom.GetExit(direction);
            if (nextRoom != null)
            {
                CurrentRoom = nextRoom;
                _achievementManager.Notify("RoomChange", this);
                NormalMessage("\n" + this.CurrentRoom.Description());
            }
            else
            {
                ErrorMessage("\nThere is no door on " + direction);
            }
        }
        
        public void Die()
        {
            Lives--;
            _achievementManager.Notify("PlayerDeath", this);
            if (Lives > 0)
            {
                // If player has no checkpoints, create one
                if (_playerHistory.Count == 1)
                {
                    _playerHistory.SaveState(CreateState());
                }
                LoadCheckpoint(_playerHistory.RestoreState());
                ErrorMessage("\nYou have died. You have " + Lives + " lives left.");
                InfoMessage("You've returned to your last checkpoint: " + CurrentRoom.Description());
            }
            else
            {
                ErrorMessage("\nYou have died. You have no lives left. Game Over.");
                // TODO: Game Over
            }
        }

        // Produce snapshot of player state
        public PlayerState CreateState()
        {
            return new PlayerState(CurrentRoom, Currency);
        }
        
        // Load last checkpoint
        // Restore player state
        public void LoadCheckpoint(PlayerState state)
        {
            CurrentRoom = state.CurrentRoom;
            Currency = state.Currency;
        }

        public void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ColoredMessage(string message, ConsoleColor newColor)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = newColor;
            OutputMessage(message);
            Console.ForegroundColor = oldColor;
        }

        public void NormalMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.White);
        }
        
        public void AchieveMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Green);
        }

        public void InfoMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Blue);
        }

        public void WarningMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.DarkYellow);
        }

        public void ErrorMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Red);
        }
    }

}
