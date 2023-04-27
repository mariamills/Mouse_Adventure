using System.Collections.Generic;
using StarterGame.Enemies;
using StarterGame.Interactables;

namespace StarterGame.Rooms
{
    /*
     * Spring 2023
     */
    public class Room
    {
        private readonly Dictionary<string, Room> _exits;
        private string _tag;
        private string _name;
        public string Tag { get { return _tag; } set { _tag = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public bool IsCheckPoint { get; set; }
        public Enemy Enemy { get; set; }

        public Dictionary<string, Interactable> Interactables { get; }

        // Designated Constructor
        public Room(string tag = "unknown", string name = "No Name")
        {
            _exits = new Dictionary<string, Room>();
            Tag = tag;
            Name = name;
            Interactables = new Dictionary<string, Interactable>();
        }

        public void SetExit(string exitName, Room room)
        {
            _exits[exitName] = room;
        }

        public Room GetExit(string exitName)
        {
            Room room = null;
            _exits.TryGetValue(exitName, out room);
            return room;
        }

        private string GetExits()
        {
            string exitNames = "------Available Paths------";
            Dictionary<string, Room>.KeyCollection keys = _exits.Keys;
            // Show the exit names and the room tags
            foreach (string exitName in keys)
            {
                exitNames += "\n" + exitName + " --> " + _exits[exitName].Name;
            }
            return exitNames;
        }

        public string Details()
        {
            return "You are " + Tag + "\n" + GetExits();
        }
    }
}
