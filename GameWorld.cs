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
            Room mafiaHideout = new Room("in the Mouse Mafia Hideout", "Mouse Mafia Hideout");
            Room backAlley = new Room("in the back alley", "Back Alley");
            Room cheeseSquare = new Room("in the Cheese Square", "Cheese Square");
            Room mousetopia = new Room("in the comfort of Mousetopia", "Mousetopia");
            Room sewerEntrance = new Room("at the entrance of the sewer", "Sewer Entrance");
            Room sewer = new Room("in the sewer, connecting Mousetopia to the Giant's territory", "Sewer");
            Room pipeHub = new Room("in the pipe hub, where multiple pipes lead to different Giant's houses", "Pipe Hub");
            
            // Giant's Territory
            // Orange House
            Room orangeHouseBathroom = new Room("in a orange bathroom", "Pipe 1");
            Room orangeHouseKitchen = new Room("in a orange kitchen", "Orange Kitchen");
            
            // Blue House
            Room blueHouseBathroom = new Room("in a blue bathroom", "Pipe 2");
            Room blueHouseLivingRoom = new Room("in a blue living room", "Blue Living Room");
            Room blueHouseKitchen = new Room("in a blue kitchen", "Blue Kitchen");
            Room blueHouseDiningRoom = new Room("in a blue dining room", "Blue Dining Room");
            
            // Green House
            Room greenHouseBathroom = new Room("in a green bathroom", "Pipe 3");
            Room greenHouseBedroom1 = new Room("in a messy room...", "Messy Room");
            Room greenHouseBedroom2 = new Room("in a clean room...", "Clean Room");
            Room greenHouseHallway = new Room("in a hallway", "Hallway");
            Room greenHouseGameRoom = new Room("in a game room", "Game Room");
            
            // Red House
            Room redHouseBathroom = new Room("in a red bathroom", "Pipe 4");
            Room redHouseKitchen = new Room("in a red kitchen", "Red Kitchen");
            Room redHouseLivingRoom = new Room("in a red living room", "Red Living Room");

            mafiaHideout.SetExit("east", backAlley);
            
            backAlley.SetExit("west", mafiaHideout);
            backAlley.SetExit("south", cheeseSquare);
            
            cheeseSquare.SetExit("north", backAlley);
            cheeseSquare.SetExit("east", mousetopia);
            
            mousetopia.SetExit("west", cheeseSquare);
            mousetopia.SetExit("south", sewerEntrance);
            
            sewerEntrance.SetExit("north", mousetopia);
            sewerEntrance.SetExit("south", sewer);
            
            sewer.SetExit("north", sewerEntrance);
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