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

        private readonly PlayerHistory _playerPlayerHistory;
        private readonly AchievementManager _achievementManager = AchievementManager.Instance;

        public Player(Room room)
        {
            CurrentRoom = room;
            Lives = 3;
            Currency = 0;
            _playerPlayerHistory = new PlayerHistory();
            _playerPlayerHistory.SaveState(CreateState());
        }

        public void WalkTo(string direction)
        {
            Room nextRoom = CurrentRoom.GetExit(direction);
            if (nextRoom != null)
            {
                CurrentRoom = nextRoom;
                _achievementManager.Notify("RoomChange", this);
                NormalMessage("\n" + CurrentRoom.Details());
                ScanRoom();
                if (CurrentRoom.IsCheckPoint)
                {
                    _playerPlayerHistory.SaveState(CreateState());
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
            if (teleporter != null) teleporter.Interact(this);
            _achievementManager.Notify("Teleport", this);
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
            if (interactable.CheeseAmount > 0)
            {
                AchieveMessage($"You found {interactable.CheeseAmount} cheese!");
                Currency += interactable.CheeseAmount;
                interactable.CheeseAmount = 0;
                _achievementManager.Notify("CheeseFound", this);
                InfoMessage($"You now have {Currency} cheese.");
            }
        }

        private void RemoveInteractableIfNotTeleporter(string obj, Interactable interactable)
        {
            if (!(interactable is Teleporter))
            {
                CurrentRoom.Interactables.Remove(obj);
            }
        }


        public void Die()
        {
            Lives--;
            _achievementManager.Notify("PlayerDeath", this);
            PlayerState playerState = _playerPlayerHistory.RestoreState();
            if (Lives > 0)
            {
                if (_playerPlayerHistory.Count == 1)
                {
                    playerState = _playerPlayerHistory.PeekState();
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
