﻿namespace StarterGame
{
    public class GameWorld
    {
        private static GameWorld _instance;

        private GameWorld()
        {
        }
        
        public static GameWorld Instance
        {
            get
            {
                // Lazy instantiation
                if (_instance == null)
                {
                    _instance = new GameWorld();
                }
                return _instance;
            }
        }
        
        public Room CreateWorld()
        {
            // Mouse Mafia
            Room mafiaHideout = new Room("in the Mouse Mafia Hideout", "Mouse Mafia Hideout", "The Mouse Mafia Hideout is a small, dark room. There are about five mice sitting around the table, all of them are wearing black suits and black ties. There is a large, black briefcase on the table.");
            Room backAlley = new Room("in the back alley", "Back Alley", "The Back Alley is a dark, narrow alleyway. There are a few dumpsters and trash cans scattered around and nothing but abandoned buildings on either side.");
            
            // Mouse Mafia Interactables
            Interactable briefcase = new Interactable("briefcase", "You approach the briefcase. The surrounding mice look at you with suspicion. Best to leave it alone.");
            mafiaHideout.Interactables.Add("briefcase", briefcase);

            // Mousetopia
            Room cheeseSquare = new Room("in the Cheese Square", "Cheese Square", "The Cheese Square is a large, open area with a fountain in the middle. There are a few benches scattered around. Mice are interacting with each other and there are a few shops on the sides.");
            Room mousetopia = new Room("in the comfort of Mousetopia", "Mousetopia", "The entrance to Mousetopia, the home of the mice.");
            Room sewer = new Room("in the sewer, connecting Mousetopia to the Giant's territory", "Sewer", "The Sewer is a dark, damp tunnel. There are a few puddles of water on the ground.");
            Room pipeHub = new Room("in the pipe hub, where multiple pipes lead to different Giant's houses", "Pipe Hub", "The Pipe Hub is a large, open area. There are four pipes...wonder where they lead?");
            
            // Mousetopia Interactables
            Interactable cheeseFountain = new Interactable("fountain", "You approach the fountain. There are a few mice drinking from it.");
            cheeseSquare.Interactables.Add("fountain", cheeseFountain);
            
            // Giant's Territory
            // Orange House
            Room orangeHouseBathroom = new Room("in a orange bathroom", "Pipe 1", "The Orange Bathroom is a small, dark room.");
            Room orangeHouseKitchen = new Room("in a orange kitchen", "Orange Kitchen", "The Orange Kitchen is a large, open area.");
            
            // Blue House
            Room blueHouseBathroom = new Room("in a blue bathroom", "Pipe 2", "The Blue Bathroom is a small, dark room.");
            Room blueHouseLivingRoom = new Room("in a blue living room", "Blue Living Room", "The Blue Living Room is a large, open area.");
            Room blueHouseKitchen = new Room("in a blue kitchen", "Blue Kitchen", "The Blue Kitchen is a large, open area.");
            Room blueHouseDiningRoom = new Room("in a blue dining room", "Blue Dining Room", "The Blue Dining Room is a large, open area.");
            
            // Green House
            Room greenHouseBathroom = new Room("in a green bathroom", "Pipe 3", "The Green Bathroom is a large room.");
            Room greenHouseBedroom1 = new Room("in a messy room...", "Messy Room", "This room is a mess.");
            Room greenHouseBedroom2 = new Room("in a clean room...", "Clean Room", "This room is clean.");
            Room greenHouseHallway = new Room("in a hallway", "Hallway", "The Hallway is a long, narrow hallway.");
            Room greenHouseGameRoom = new Room("in a game room", "Game Room", "The Game Room is a large, open area.");
            
            // Red House
            Room redHouseBathroom = new Room("in a red bathroom", "Pipe 4", "The Red Bathroom is a small.");
            Room redHouseKitchen = new Room("in a red kitchen", "Red Kitchen", "The Red Kitchen is a large, open area.");
            Room redHouseLivingRoom = new Room("in a red living room", "Red Living Room", "The Red Living Room is a large, open area.");

            mafiaHideout.SetExit("east", backAlley);
            
            backAlley.SetExit("west", mafiaHideout);
            backAlley.SetExit("south", cheeseSquare);
            
            cheeseSquare.SetExit("north", backAlley);
            cheeseSquare.SetExit("east", mousetopia);
            
            mousetopia.SetExit("west", cheeseSquare);
            mousetopia.SetExit("south", sewer);

            sewer.SetExit("north", mousetopia);
            sewer.SetExit("south", pipeHub);
            
            pipeHub.SetExit("north", sewer);
            pipeHub.SetExit("west", orangeHouseBathroom);
            pipeHub.SetExit("south", greenHouseBathroom);
            pipeHub.SetExit("east", redHouseBathroom);
            pipeHub.SetExit("southwest", blueHouseBathroom);
            
            // Orange House
            orangeHouseBathroom.SetExit("east", pipeHub);
            orangeHouseBathroom.SetExit("west", orangeHouseKitchen);
            orangeHouseKitchen.SetExit("east", orangeHouseBathroom);
            
            // Blue House
            blueHouseBathroom.SetExit("northeast", pipeHub);
            blueHouseBathroom.SetExit("west", blueHouseLivingRoom);
            
            blueHouseLivingRoom.SetExit("east", blueHouseBathroom);
            blueHouseLivingRoom.SetExit("north", blueHouseKitchen);
            
            blueHouseKitchen.SetExit("south", blueHouseLivingRoom);
            blueHouseLivingRoom.SetExit("west", blueHouseDiningRoom);
            
            blueHouseDiningRoom.SetExit("east", blueHouseLivingRoom);
            
            // Green House
            greenHouseBathroom.SetExit("north", pipeHub);
            greenHouseBathroom.SetExit("west", greenHouseBedroom1);
            greenHouseBathroom.SetExit("east", greenHouseBedroom2);
            
            greenHouseBedroom1.SetExit("east", greenHouseBathroom);
            greenHouseBedroom1.SetExit("south", greenHouseHallway);
            
            greenHouseBedroom2.SetExit("west", greenHouseBathroom);
            greenHouseBedroom2.SetExit("south", greenHouseHallway);
            
            greenHouseHallway.SetExit("northwest", greenHouseBedroom1);
            greenHouseHallway.SetExit("northeast", greenHouseBedroom2);
            greenHouseHallway.SetExit("south", greenHouseGameRoom);
            
            greenHouseGameRoom.SetExit("north", greenHouseHallway);
            
            // Red House
            redHouseBathroom.SetExit("west", pipeHub);
            redHouseBathroom.SetExit("east", redHouseKitchen);
            redHouseKitchen.SetExit("west", redHouseBathroom);
            redHouseKitchen.SetExit("north", redHouseLivingRoom);
            redHouseLivingRoom.SetExit("south", redHouseKitchen);
 
            
            return mousetopia;
        }

    }
}