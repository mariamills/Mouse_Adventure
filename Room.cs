using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    /*
     * Spring 2023
     */
    public class Room
    {
        private Dictionary<string, Room> _exits;
        private string _tag;
        private string _name;
        public string Tag { get { return _tag; } set { _tag = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        
        // Designated Constructor
        public Room(string tag = "No Tag", string name = "No Name")
        {
            _exits = new Dictionary<string, Room>();
            Tag = tag;
            Name = name;
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

        public string Description()
        {
            return "You are " + Tag + ".\n" + GetExits();
        }
    }
}
