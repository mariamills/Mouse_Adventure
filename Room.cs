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
        public string Tag { get { return _tag; } set { _tag = value; } }

        public Room() : this("No Tag"){}

        // Designated Constructor
        public Room(string tag)
        {
            _exits = new Dictionary<string, Room>();
            this.Tag = tag;
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
            string exitNames = "Routes: ";
            Dictionary<string, Room>.KeyCollection keys = _exits.Keys;
            // Show the exit names and the room tags
            foreach (string exitName in keys)
            {
                exitNames += " " + exitName + " " + _exits[exitName].Tag;
            }
            return exitNames;
        }

        public string Description()
        {
            return "You are " + Tag + ".\n *** " + GetExits();
        }
    }
}
