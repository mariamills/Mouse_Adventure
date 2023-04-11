using System.Collections;
using System.Collections.Generic;
using System;
using StarterGame.Achievements;

namespace StarterGame
{
    /*
     * Spring 2023
     */
    public class Player
    {
        private Room _currentRoom = null;
        public Room CurrentRoom { get { return _currentRoom; } set { _currentRoom = value; } }

        public Player(Room room)
        {
            _currentRoom = room;
        }

        public void WalkTo(string direction)
        {
            AchievementManager achievementManager = AchievementManager.Instance;
            Room nextRoom = this.CurrentRoom.GetExit(direction);
            if (nextRoom != null)
            {
                CurrentRoom = nextRoom;
                achievementManager.Notify("RoomChange", this);
                NormalMessage("\n" + this.CurrentRoom.Description());
            }
            else
            {
                ErrorMessage("\nThere is no door on " + direction);
            }
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
