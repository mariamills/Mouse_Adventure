using System;
using System.Collections.Generic;
using StarterGame.Rooms;

namespace StarterGame.Interactables
{
    public class Teleporter : Interactable
    {
        private readonly List<Room> _possibleDestinations;
        private Random _random;
        public Teleporter(string name, string description, List<Room> possibleDestinations) : base(name, description)
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
    }
}