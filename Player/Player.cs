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

        public int Currency { get; private set; }
        public int Lives { get; private set; }
        public bool IsInCombat { get; private set; }
        private static readonly int MaxCurrency = 10;
        private readonly PlayerHistory _playerHistory;
        private readonly AchievementManager _achievementManager = AchievementManager.Instance;
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
                _achievementManager.Notify("RoomChange", this);
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
            TeleporterDecorator teleporterDecorator = CurrentRoom.Interactables[direction] as TeleporterDecorator;
            if (teleporterDecorator != null)
            {
                if (Currency >= 10)
                {
                    teleporterDecorator.EnterBossRoom(this);
                    _achievementManager.Notify("EnteredBossLair", this);
                    EnemyEncounter();
                }
                else
                {
                    ErrorMessage("You approach the door, but the group of mice guarding it stop you. They say if you're not here to pay the tax, you're not welcome.");
                }
            }
        }

        private void UseTeleporter(string direction)
        {
            TeleporterDecorator teleporterDecorator = CurrentRoom.Interactables[direction] as TeleporterDecorator;
            if (teleporterDecorator != null)
            {
                teleporterDecorator.Interact(this);
                _playerHistory.RoomHistory = new Stack<Room>();
                _playerHistory.SaveState(CreateState());
                _achievementManager.Notify("UsedTeleporter", this);
                EnemyEncounter();
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
                RemoveInteractable(obj, interactable);
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
                ErrorMessage("I have enough to pay the toll.");
            }
        }

        private void RemoveInteractable(string obj, Interactable interactable)
        {
            if (!(interactable is TeleporterDecorator) && interactable.CheeseAmount == 0)
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

        private void EnemyEncounter()
        {
            Enemy enemy = CurrentRoom.Enemy;
            if (enemy != null)
            {
                IsInCombat = true;
                if (enemy is Ratatax)
                {
                    _achievementManager.Notify("BossEncounter", this);
                    BattleMessage($">>>You walk towards {enemy.Name}, all the mice in the room are watching you.");
                    BattleMessage("RATATAX: Here to pay my CHEESE TAX?");
                    HandleEnemyInteraction();
                }
                else
                {
                    _achievementManager.Notify("EnemyEncounter", this);
                    WarningMessage($"An {enemy.Name} is coming towards you. What will you do?");
                    HandleEnemyInteraction();
                }
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
            BattleMessage(">>>You've chosen to bite " + CurrentRoom.Enemy.Name);
            if (enemy.IsFriendly)
            {
                BattleMessage(enemy.Name + " looks at you with disappointment and walks away.");
                CurrentRoom.Enemy = null;
            }
            else
            {
                if (enemy.ScaredChance < 5)
                {
                    _achievementManager.Notify("EnemyDefeated", this);
                    BattleMessage(enemy.Name + " has ran away...");
                    CurrentRoom.Enemy = null;
                }
                else
                {
                    enemy.DoAttack(this);
                    Die();
                }
            }
            IsInCombat = false;
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
                if (enemy is Ratatax)
                {
                    int annoyedRatatax = 0;
                    if (annoyedRatatax == 3)
                    {
                        BattleMessage("RATATAX: You've annoyed me mouse. Now you'll have to pay with your life.");
                        EvenWorseEnd();
                    }
                    if (amountToGive < 10)
                    {
                        BattleMessage("RATATAX: That's not enough! You know my cheese tax is 10 cheese! Pay up!");
                        annoyedRatatax++;
                    }
                    else
                    {
                        Currency -= amountToGive;
                        _achievementManager.Notify("PaidCheeseTax", this);
                        BattleMessage(">>>You gave " + amountToGive + " cheese to Ratatax.");
                        NormalMessage(">>>You paid the cheese tax.... You've avoid the Mouse Mafia for now...but what will happen next time?");
                        IsInCombat = false;
                        BadEnd();
                    }
                } 
                else if (enemy is Dog)
                {
                    Currency -= amountToGive;
                    _achievementManager.Notify("FedDog", this);
                    NormalMessage(">>>You gave the dog some cheese. He wags his tail happily. You made a friend.");
                    AchieveMessage("You've unlocked the 'whistle' command! Use it when you REALLY need help. You may only use it ONCE. Choose wisely.");
                    IsInCombat = false;
                }
                else
                {
                    NormalMessage("I don't think I should give that to them.");
                }
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
                    if (enemy is Ratatax)
                    {
                        HandleRatataxExchange(amountToGive);
                    } 
                    else if (enemy is Dog)
                    {
                        HandleDogEncounter(amountToGive);
                    }
                    else
                    {
                        NormalMessage("I don't think I should give that to them.");
                    }
                } 
            }
            else 
            {
                ErrorMessage("I can't give that.");
            }
        }

        private void HandleRatataxExchange(int amountToGive)
        {
            int annoyedRatatax = 0;
            if (annoyedRatatax == 3)
            {
                BattleMessage("RATATAX: You've annoyed me mouse. Now you'll have to pay with your life.");
                EvenWorseEnd();
            }
            if (amountToGive < 10)
            {
                BattleMessage("RATATAX: That's not enough! You know my cheese tax is 10 cheese! Pay up!");
                annoyedRatatax++;
            }
            else
            {
                Currency -= amountToGive;
                _achievementManager.Notify("PaidCheeseTax", this);
                BattleMessage(">>>You gave " + amountToGive + " cheese to Ratatax.");
                NormalMessage(">>>You paid the cheese tax.... You've avoid the Mouse Mafia for now...but what will happen next time?");
                IsInCombat = false;
                BadEnd();
            }
        }

        private void HandleDogEncounter(int amountToGive)
        {
            int amountWanted = 5;
            int amountGiven = 0;
            amountGiven += amountToGive;
            Currency -= amountToGive;
            if (amountGiven >= amountWanted)
            {
                _achievementManager.Notify("FedDog", this);
                NormalMessage(">>>You gave the dog some cheese. He wags his tail happily. You made a friend.");
                IsInCombat = false;
            }
            else
            {
                BattleMessage(">>>You gave the dog " + amountToGive + " cheese. He looks at you with disappointment. He wants more cheese.");
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
        
        private void BadEnd()
        {
            NormalMessage("You rest easy knowing that your cheese tax with the Mouse Mafia has been paid. However, what will you do next time? If only someone would have put a stop to the Mouse Mafia once and for all...");
            Lives = 0;
        }
        
        private void GoodEnd()
        {
            NormalMessage("You've defeated the Mouse Mafia and saved the town from their tyranny. You are a hero. You are a legend. You are...the Mouse King.");
            Lives = 0;
        }

        private void EvenWorseEnd()
        {
            NormalMessage("You've annoyed Ratatax one too many times. He's had enough of your shenanigans. He kills you and takes all of your cheese. You are dead.");
            Lives = 0;
        }

        public void Die()
        {
            Lives--;
            _achievementManager.Notify("PlayerDeath", this);
            PlayerState playerState = _playerHistory.PeekState();
            if (Lives > 0)
            {
                if (_playerHistory.Count > 1)
                {
                    playerState = _playerHistory.RestoreState();
                } 
                LoadCheckpoint(playerState);
                ErrorMessage("\nYou have died. You have " + Lives + " lives left.");
                InfoMessage("You've returned to your last checkpoint: " + CurrentRoom.Name);
            //    InfoMessage(CurrentRoom.Details());
            //    ScanRoom();
            }
            else
            {
                ErrorMessage("\nYou have died. You have no lives left. Game Over.");
                _achievementManager.Notify("DeadDead", this);
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
