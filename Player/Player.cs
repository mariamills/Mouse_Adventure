using System;
using System.Collections.Generic;
using StarterGame.Achievements;
using StarterGame.Interactables;
using StarterGame.Rooms;

namespace StarterGame.Player
{
    /*
     * Spring 2023
     */
    public class Player
    {
        public Room CurrentRoom { get; set; }

        public int Currency { get; private set; }
        public int Lives { get; private set; }
        public static int MaxCurrency => 10;

        private readonly PlayerHistory _playerHistory;
        private readonly AchievementManager _achievementManager = AchievementManager.Instance;

        public Player(Room room)
        {
            CurrentRoom = room;
            Lives = 3;
            Currency = 0;
            _playerHistory = new PlayerHistory();
            _playerHistory.SaveState(CreateState());
        }

        public void WalkTo(string direction)
        {
            Room nextRoom = CurrentRoom.GetExit(direction);
            if (nextRoom != null)
            {
                _playerHistory.RoomHistory.Push(CurrentRoom);
                CurrentRoom = nextRoom;
                _achievementManager.Notify("RoomChange", this);
                NormalMessage("\n" + CurrentRoom.Details());
                ScanRoom();
                if (CurrentRoom.IsCheckPoint)
                {
                    _playerHistory.SaveState(CreateState());
                }
            }
            else if (direction.Contains("sink"))
            {
                UseTeleporter(direction);
            }
            else
            {
                ErrorMessage("\nThere is no path in the " + direction);
            }
        }

        private void UseTeleporter(string direction)
        {
            Teleporter teleporter = CurrentRoom.Interactables[direction] as Teleporter;
            if (teleporter != null)
            {
                teleporter.Interact(this);
                _playerHistory.RoomHistory = new Stack<Room>();
                _playerHistory.SaveState(CreateState());
                _achievementManager.Notify("UsedTeleporter", this);
            }
        }

        public void ScanRoom()
        {
            NormalMessage("\nYou see:");
            if (CurrentRoom.Interactables.Count > 0)
            {
                foreach (KeyValuePair<string, Interactable> interactable in CurrentRoom.Interactables)
                {
                    ColoredMessage(interactable.Key, ConsoleColor.DarkYellow);
                }
            }
            else
            {
                InfoMessage("Nothing of interest.");
            }
        }

        public void Look(string obj)
        {
            if (CurrentRoom.Interactables.TryGetValue(obj, out Interactable interactable))
            {
                NormalMessage(interactable.Description);
                HandleCheeseInteraction(interactable);
                RemoveInteractableIfNotTeleporter(obj, interactable);
                ScanRoom();
            }
            else
            {
                InfoMessage($"I don't see a {obj} here.");
            }
        }

        private void HandleCheeseInteraction(Interactable interactable)
        {
            if (Currency <= MaxCurrency)
            {
                if (interactable.CheeseAmount > 0)
                {
                    AchieveMessage($"You found {interactable.CheeseAmount} cheese!");
                    Currency += interactable.CheeseAmount;
                    interactable.CheeseAmount = 0;
                    _achievementManager.Notify("CheeseFound", this);
                    InfoMessage($"You now have {Currency} cheese.");
                }
            }
            else
            {
                ErrorMessage("I can't carry any more cheese. I am just a mouse after all.");
            }
        }

        private void RemoveInteractableIfNotTeleporter(string obj, Interactable interactable)
        {
            if (!(interactable is Teleporter))
            {
                CurrentRoom.Interactables.Remove(obj);
            }
        }

        public void Back()
        {
            if (_playerHistory.RoomHistory.Count > 0)
            {
                CurrentRoom = _playerHistory.RoomHistory.Pop();
                NormalMessage("\n" + CurrentRoom.Details());
                ScanRoom();
            }
            else
            {
                InfoMessage("There's nowhere to go back to.");
            }
        }

        public void Die()
        {
            Lives--;
            _achievementManager.Notify("PlayerDeath", this);
            PlayerState playerState = _playerHistory.RestoreState();
            if (Lives > 0)
            {
                if (_playerHistory.Count == 1)
                {
                    playerState = _playerHistory.PeekState();
                }
                LoadCheckpoint(playerState);
                ErrorMessage("\nYou have died. You have " + Lives + " lives left.");
                InfoMessage("You've returned to your last checkpoint: " + CurrentRoom.Name);
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
            ColoredMessage(message, ConsoleColor.Yellow);
        }

        public void ErrorMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Red);
        }
    }

}
