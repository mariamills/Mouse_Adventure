namespace StarterGame
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
            // Mousetopia
            Room mafiaHideout = new Room("in the Mouse Mafia Hideout", "Mouse Mafia Hideout", "The Mouse Mafia Hideout is a small, dark room. There are about five mice sitting around the table, all of them are wearing black suits and black ties. There is a large, black briefcase on the table. There is a door to the north...");
            Room backAlley = new Room("in the back alley", "Back Alley", "The Back Alley is a dark, narrow alleyway. There are a few dumpsters and trash cans scattered around and nothing but abandoned buildings on either side.");
            Room cheeseSquare = new Room("in the Cheese Square", "Cheese Square", "The Cheese Square is a large, open area with a fountain in the middle. There are a few benches scattered around. Mice are interacting with each other and there are a few shops on the sides.");
            Room mousetopia = new Room("in the comfort of Mousetopia", "Mousetopia", "The entrance to Mousetopia, the home of the mice.");
            Room sewer = new Room("in the sewer, connecting Mousetopia to the Giant's territory", "Sewer", "The Sewer is a dark, damp tunnel. There are a few puddles of water on the ground.");
            Room pipeHub = new Room("in the pipe hub, where multiple pipes lead to different Giant's houses", "Pipe Hub", "The Pipe Hub is a large, open area. There are four pipes...wonder where they lead?");
            
            mousetopia.Interactables.Add("trash", 1);
            mousetopia.Interactables.Add("bench", 0);
            
            // Giant's Territory
            // Orange House
            Room orangeHouseBathroom = new Room("in a orange bathroom", "Pipe 1", "The Orange Bathroom is a small, dark room. There is a toilet and a sink.");
            Room orangeHouseKitchen = new Room("in a orange kitchen", "Orange Kitchen", "The Orange Kitchen is a large, open area. There is a large, orange refrigerator, and a sink.");
            
            // Blue House
            Room blueHouseBathroom = new Room("in a blue bathroom", "Pipe 2", "The Blue Bathroom is a small, dark room. There is a toilet and a sink.");
            Room blueHouseLivingRoom = new Room("in a blue living room", "Blue Living Room", "The Blue Living Room is a large, open area. There is a large, blue couch and a large, blue TV.");
            Room blueHouseKitchen = new Room("in a blue kitchen", "Blue Kitchen", "The Blue Kitchen is a large, open area. There is a large, blue refrigerator, and a sink.");
            Room blueHouseDiningRoom = new Room("in a blue dining room", "Blue Dining Room", "The Blue Dining Room is a large, open area. There is a large, blue table and a large, blue chandelier.");
            
            // Green House
            Room greenHouseBathroom = new Room("in a green bathroom", "Pipe 3", "The Green Bathroom is a large room. There are two toilets, two sinks and two showers.");
            Room greenHouseBedroom1 = new Room("in a messy room...", "Messy Room", "This room is a mess. There is a clothes pile on the floor. There is also a bed, a desk and a closet.");
            Room greenHouseBedroom2 = new Room("in a clean room...", "Clean Room", "This room is clean. There is a bed, a desk and a closet.");
            Room greenHouseHallway = new Room("in a hallway", "Hallway", "The Hallway is a long, narrow hallway. There is a cabinet near the end of the hallway.");
            Room greenHouseGameRoom = new Room("in a game room", "Game Room", "The Game Room is a large, open area. There is a large, green couch and a large, green TV.");
            
            // Red House
            Room redHouseBathroom = new Room("in a red bathroom", "Pipe 4", "The Red Bathroom is a small. There is a toilet, a sink and a cabinet.");
            Room redHouseKitchen = new Room("in a red kitchen", "Red Kitchen", "The Red Kitchen is a large, open area. There is a large, red refrigerator, cabinets and a sink.");
            Room redHouseLivingRoom = new Room("in a red living room", "Red Living Room", "The Red Living Room is a large, open area. There is a large, red couch and a large, red TV.");

            mafiaHideout.SetExit("east", backAlley);
            
            backAlley.SetExit("west", mafiaHideout);
            backAlley.SetExit("south", cheeseSquare);
            
            cheeseSquare.SetExit("north", backAlley);
            cheeseSquare.SetExit("east", mousetopia);
            
            mousetopia.SetExit("west", cheeseSquare);
            mousetopia.SetExit("south", sewer);

            sewer.SetExit("north", sewer);
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