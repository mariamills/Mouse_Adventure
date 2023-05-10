using System;
using System.Collections.Generic;
using StarterGame.Rooms;

namespace StarterGame.Interactables
{
    public class TeleporterDecorator : InteractableDecorator
    {
        private readonly List<Room> _possibleDestinations;
        private readonly Random _random;
        public TeleporterDecorator(Interactable interactable, List<Room> possibleDestinations) : base(interactable, interactable.Name, interactable.Description)
        {
            _possibleDestinations = possibleDestinations;
            _random = new Random();
        }
        
        public void Interact(Player.Player player)
        {
            Room nextRoom = _possibleDestinations[_random.Next(0, _possibleDestinations.Count)];
            player.CurrentRoom = nextRoom;
            player.WarningMessage("\nYou have been teleported to " + player.CurrentRoom.Name);
            player.InfoMessage("\n" + player.CurrentRoom.Details());
            player.ScanRoom();
        }
        
        public void EnterBossRoom(Player.Player player)
        {
            player.CurrentRoom = _possibleDestinations[0];
            player.BattleMessage("You've entered "+ player.CurrentRoom.Name + "!");
            player.InfoMessage("\n" + player.CurrentRoom.Tag);
        }
    }
}