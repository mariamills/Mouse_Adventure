using System;
using System.Collections.Generic;
using StarterGame.Achievements;
using StarterGame.Commands;
using StarterGame.Enemies;
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

        public int Currency { get; set; }
        public int Lives { get; set; }
        public bool IsInCombat { get; set; }
        public readonly AchievementManager AchievementManager = AchievementManager.Instance;
        private static readonly int MaxCurrency = 15;
        private readonly PlayerHistory _playerHistory;
        private static readonly Command[] CombatCommands = {new BiteCommand(), new FleeCommand(), new GiveCommand(), new WhistleCommand()};
        private readonly Parser _combatParser = new Parser(new CommandWords(CombatCommands));

        public Player(Room room)
        {
            CurrentRoom = room;
            Lives = 3;
            Currency = 0;
            IsInCombat = false;
            _playerHistory = new PlayerHistory();
            _playerHistory.SaveState(CreateState());
        }

        public void WalkTo(string direction)
        {
            Room nextRoom = CurrentRoom.GetExit(direction);
            if (nextRoom != null)
            {
                if (CurrentRoom.IsCheckPoint)
                {
                    _playerHistory.SaveState(CreateState());
                }
                _playerHistory.RoomHistory.Push(CurrentRoom);
                CurrentRoom = nextRoom;
                AchievementManager.Notify("RoomChange", this);
                EnemyEncounter();
                NormalMessage("\n" + CurrentRoom.Details());
                ScanRoom();
            }
            else if (direction.Contains("sink"))
            {
                UseTeleporter(direction);
            } 
            else if (direction.Contains("door"))
            {
                EnterRatataxLair(direction);
            }
            else
            {
                ErrorMessage("\nThere is no path in the " + direction);
            }
        }
        
        private void EnterRatataxLair(string direction)
        {
            CurrentRoom.Interactables.TryGetValue(direction, out Interactable interactable);
            TeleporterDecorator teleporterDecorator = interactable as TeleporterDecorator;
            if (teleporterDecorator != null)
            {
                if (Currency >= 10)
                {
                    teleporterDecorator.EnterBossRoom(this);
                    AchievementManager.Notify("EnteredBossLair", this);
                    EnemyEncounter();
                }
                else
                {
                    ErrorMessage("You approach the door, but the group of mice guarding it stop you. They say if you're not here to pay the tax, you're not welcome.");
                }
            }
            else
            {
                ErrorMessage("I don't see a " + direction + " here.");
            }
        }

        private void UseTeleporter(string direction)
        {
            CurrentRoom.Interactables.TryGetValue(direction, out Interactable interactable);
            TeleporterDecorator teleporterDecorator = interactable as TeleporterDecorator;
            if (teleporterDecorator != null)
            {
                teleporterDecorator.Interact(this);
                _playerHistory.RoomHistory = new Stack<Room>();
                _playerHistory.SaveState(CreateState());
                AchievementManager.Notify("UsedTeleporter", this);
                EnemyEncounter();
            }
            else
            {
                ErrorMessage("I don't see a " + direction + " here.");
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
                if (interactable.CheeseAmount > 0)
                { 
                    HandleCheeseInteraction(interactable);
                }
                else
                {
                    RemoveInteractable(interactable);
                }
                ScanRoom();
            }
            else
            {
                // string interpolation
                InfoMessage($"I don't see a {obj} here.");
            }
        }

        private void HandleCheeseInteraction(Interactable interactable)
        {
            if (Currency >= MaxCurrency)
            { 
                ErrorMessage("This cheese is getting heavy, I have enough cheese to pay the tax. I should go back to Mousetopia.");
            }
            else
            {
                AchieveMessage($"You found {interactable.CheeseAmount} cheese!");
                Currency += interactable.CheeseAmount;
                interactable.CheeseAmount = 0;
                AchievementManager.Notify("CheeseFound", this);
                InfoMessage($"You now have {Currency} cheese.");
                RemoveInteractable(interactable);
            }
        }

        private void RemoveInteractable(Interactable interactable)
        {
            if (!(interactable is TeleporterDecorator) && interactable.CheeseAmount == 0)
            {
                CurrentRoom.Interactables.Remove(interactable.Name);
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

        private void EnemyEncounter()
        {
            Enemy enemy = CurrentRoom.Enemy;
            if (enemy != null)
            {
                if (enemy is Ratatax)
                {
                    AchievementManager.Notify("BossEncounter", this);
                }
                IsInCombat = true;
                AchievementManager.Notify("EnemyEncounter", this);
                enemy.OnEncounter(this);
                HandleEnemyInteraction();
            }
        }

        private void HandleEnemyInteraction()
        {
            while (IsInCombat)
            {
                Console.Write(">");
                Command command = _combatParser.ParseCommand(Console.ReadLine());
                if (command == null)
                {
                    ErrorMessage("I don't understand...");
                }
                else 
                {
                    command.Execute(this);
                }
            }
        }
        
        //Combat
        public void Bite()
        {
            Enemy enemy = CurrentRoom.Enemy;
            enemy.OnBite(this);
        }

        public void Flee()
        {
            if (!(CurrentRoom.Enemy is Ratatax))
            {
                BattleMessage("You've chosen to flee and return to the previous room.");
                IsInCombat = false;
                Back();
            }
            else
            {
                BattleMessage("You can't flee from Ratatax!");
            }
        }
        
        public void Give(string amount)
        {
            Enemy enemy = CurrentRoom.Enemy;
            int amountToGive = Convert.ToInt32(amount);
            if(amountToGive > Currency)
            {
                ErrorMessage("I don't have that much cheese.");
            }
            else
            {
                Currency -= amountToGive;
                enemy.OnGive(this, amountToGive);
            }
        }
        
        public void Give(string amount, string itemName)
        {
            Enemy enemy = CurrentRoom.Enemy;
            int amountToGive = Convert.ToInt32(amount);
            if (itemName == "cheese")
            {
                if(amountToGive > Currency)
                {
                    ErrorMessage("I don't have that much cheese.");
                }
                else
                {
                    Currency -= amountToGive;
                    enemy.OnGive(this, amountToGive);
                } 
            }
            else 
            {
                ErrorMessage("I can't give that.");
            }
        }

        public void Whistle()
        {
            NormalMessage(">>>You whistle loudly.");
            Enemy enemy = CurrentRoom.Enemy;
            if (enemy != null)
            {
                if (enemy is Ratatax)
                {
                    BattleMessage("Your furry friend comes to your aid! He attacks " + enemy.Name + " and his minions leaving nothing but a pile of fur. He then runs away.");
                    CurrentRoom.Enemy = null;
                    IsInCombat = false;
                    GoodEnd();
                } 
                else if (enemy is Giant)
                {
                    BattleMessage("Your furry friend comes to your aid! He attacks " + enemy.Name + " but is quickly swatted away. He runs away in fear.");
                }
                else
                {
                    BattleMessage("Your furry friend comes to aid you! He attacks the " + enemy.Name + " and leaves nothing but a pile of fur. He then runs away.");
                    IsInCombat = false;
                }
            }
        }
        
        public void BadEnd()
        {
            NormalMessage("You rest easy knowing that your cheese tax with the Mouse Mafia has been paid. However, what will you do next time? If only someone would have put a stop to the Mouse Mafia once and for all...");
            Lives = 0;
        }

        public void EvenWorseEnd()
        {
            NormalMessage("You've annoyed Ratatax one too many times. He's had enough of your shenanigans. He kills you and takes all of your cheese. You are dead.");
            Lives = 0;
        }
        
        private void GoodEnd()
        {
            NormalMessage("You've defeated the Mouse Mafia and saved the town from their tyranny. You are a hero. You are a legend. You are...the Mouse King.");
            Lives = 0;
        }

        public void Die()
        {
            Lives--;
            AchievementManager.Notify("PlayerDeath", this);
            PlayerState playerState = _playerHistory.PeekState();
            if (Lives > 0)
            {
                IsInCombat = false;
                if (_playerHistory.Count > 1)
                {
                    playerState = _playerHistory.RestoreState();
                } 
                LoadCheckpoint(playerState);
                ErrorMessage("\nYou have died. You have " + Lives + " lives left.");
                InfoMessage("You've returned to your last checkpoint: " + CurrentRoom.Name);
            }
            else
            {
                ErrorMessage("\nYou have died. You have no lives left. Game Over.");
                AchievementManager.Notify("DeadDead", this);
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

        public void BattleMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Magenta);
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
