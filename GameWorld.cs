namespace StarterGame
{
    public class GameWorld
    {
        private static GameWorld _instance;

        private GameWorld() { }
        
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
            Room mafiaHideout = CreateMafiaHideout();
            Room backAlley = CreateBackAlley();
            Room mousetopia = CreateMousetopia();
            Room cheeseSquare = CreateCheeseSquare();
            Room sewer = CreateSewer();
            Room pipeHub = CreatePipeHub();

            // Orange House
            Room pipe1 = CreateOrangeBathroom();
            Room orangeKitchen = CreateOrangeKitchen();
            
            // Blue House
            Room pipe2 = CreateBlueBathroom();
            Room blueLivingRoom = CreateBlueLivingRoom();
            Room blueKitchen = CreateBlueKitchen();
            Room blueDiningRoom = CreateBlueDiningRoom();
            
            // Green House
            Room pipe3 = CreateGreenBathroom();
            Room messyBedroom = CreateMessyBedroom();
            Room cleanBedroom = CreateCleanBedroom();
            Room hallway = CreateHallway();
            Room gameRoom = CreateGameRoom();
            
            // Red House
            Room pipe4 = CreateRedBathroom();
            Room redKitchen = CreateRedKitchen();
            Room redLivingRoom = CreateRedLivingRoom();
            
            
            mafiaHideout.SetExit("east", backAlley);
            
            backAlley.SetExit("west", mafiaHideout);
            backAlley.SetExit("south", cheeseSquare);
            
            cheeseSquare.SetExit("north", backAlley);
            cheeseSquare.SetExit("east", mousetopia);
            
            mousetopia.SetExit("west", cheeseSquare);
            mousetopia.SetExit("south", sewer);
            
            sewer.SetExit("north", mousetopia);
            sewer.SetExit("south", pipeHub);
            
            // Pipe Hub Exits
            pipeHub.SetExit("north", sewer);
            pipeHub.SetExit("west", pipe1);
            pipeHub.SetExit("southwest", pipe2);
            pipeHub.SetExit("south", pipe3);
            pipeHub.SetExit("east", pipe4);
            
            // Orange House Exits
            pipe1.SetExit("west", pipeHub);
            pipe1.SetExit("east", orangeKitchen);
            orangeKitchen.SetExit("west", pipe1);
            
            // Blue House Exits
            pipe2.SetExit("northeast", pipeHub);
            pipe2.SetExit("west", blueLivingRoom);
            blueLivingRoom.SetExit("east", pipe2);
            blueLivingRoom.SetExit("north", blueKitchen);
            blueKitchen.SetExit("south", blueLivingRoom);
            blueKitchen.SetExit("west", blueDiningRoom);
            blueDiningRoom.SetExit("east", blueKitchen);
            
            // Green House Exits
            pipe3.SetExit("north", pipeHub);
            pipe3.SetExit("west", messyBedroom);
            pipe3.SetExit("east", cleanBedroom);
            messyBedroom.SetExit("east", pipe3);
            messyBedroom.SetExit("south", hallway);
            cleanBedroom.SetExit("west", pipe3);
            cleanBedroom.SetExit("south", hallway);
            hallway.SetExit("northwest", messyBedroom);
            hallway.SetExit("northeast", cleanBedroom);
            hallway.SetExit("south", gameRoom);
            gameRoom.SetExit("north", hallway);
            
            // Red House Exits
            pipe4.SetExit("east", pipeHub);
            pipe4.SetExit("west", redKitchen);
            redKitchen.SetExit("north", redLivingRoom);
            redLivingRoom.SetExit("south", redKitchen);


            return mousetopia;
        }
        
        private Room CreateMafiaHideout()
        {
            Room mafiaHideout = new RoomBuilder()
                .SetName("Mafia Hideout")
                .SetTag("in the Mouse Mafia Hideout. There are a few scary looking mice sitting around a table with a briefcase in the middle.")
                .Build();
            
            Interactable briefcase = new Interactable("briefcase", "You approach the briefcase. The mice around the table are looking at you angrily. Best not to touch it...");
            mafiaHideout.Interactables.Add(briefcase.Name, briefcase);
            return mafiaHideout;
        }
        
        private Room CreateBackAlley()
        {
            Room backAlley = new RoomBuilder()
                .SetName("Back Alley")
                .SetTag("in the Back Alley. There are a few dumpsters and abandoned buildings on the sides.")
                .Build();
            return backAlley;
        }

        private Room CreateMousetopia()
        {
            Room mousetopia = new RoomBuilder()
                .SetName("Mousetopia")
                .SetTag("at the entrance of Mousetopia. There is a large gate in front of you.")
                .Build();
            return mousetopia;
        }
        
        private Room CreateCheeseSquare()
        {
            Room cheeseSquare = new RoomBuilder()
                .SetName("Cheese Square")
                .SetTag("in the Cheese Square. The heart of Mousetopia. There is a fountain in the middle of the square. Mice are interacting with each other and there are a few shops on the sides.")
                .Build();
            
            Interactable fountain = new Interactable("fountain", "You approach the fountain. The fountain is made of stone and is shaped like a mouse. Mice are drinking from the fountain.");
            cheeseSquare.Interactables.Add(fountain.Name, fountain);
            return cheeseSquare;
        }
        
        private Room CreateSewer()
        {
            Room sewer = new RoomBuilder()
                .SetName("Sewer")
                .SetTag("in the Sewer. There are pipes everywhere.")
                .Build();
            return sewer;
        }
        
        private Room CreatePipeHub()
        {
            Room pipeHub = new RoomBuilder()
                .SetName("Pipe Hub")
                .SetTag("in the Pipe Hub. There are four pipes leading in different directions. Wonder where they lead to...?")
                .Build();
            return pipeHub;
        }
        
        private Room CreateOrangeBathroom()
        {
            Room orangeBathroom = new RoomBuilder()
                .SetName("Pipe 1")
                .SetTag("in an Orange Bathroom. It is a small, dirty bathroom.")
                .Build();
            return orangeBathroom;
        }
        
        private Room CreateOrangeKitchen()
        {
            Room orangeKitchen = new RoomBuilder()
                .SetName("Orange Kitchen")
                .SetTag("in an Orange Kitchen.")
                .Build();
            return orangeKitchen;
        }
        
        private Room CreateBlueBathroom()
        {
            Room blueBathroom = new RoomBuilder()
                .SetName("Pipe 2")
                .SetTag("in a Blue Bathroom. It is a medium sized bathroom.")
                .Build();
            return blueBathroom;
        }
        
        private Room CreateBlueLivingRoom()
        {
            Room blueLivingRoom = new RoomBuilder()
                .SetName("Blue Living Room")
                .SetTag("in a Blue Living Room.")
                .Build();
            return blueLivingRoom;
        }
        
        private Room CreateBlueKitchen()
        {
            Room blueKitchen = new RoomBuilder()
                .SetName("Blue Kitchen")
                .SetTag("in a Blue Kitchen.")
                .Build();
            return blueKitchen;
        }
        
        private Room CreateBlueDiningRoom()
        {
            Room blueDiningRoom = new RoomBuilder()
                .SetName("Blue Dining Room")
                .SetTag("in a Blue Dining Room.")
                .Build();
            return blueDiningRoom;
        }
        
        private Room CreateGreenBathroom()
        {
            Room greenBathroom = new RoomBuilder()
                .SetName("Pipe 3")
                .SetTag("in a Green Bathroom. It is a large bathroom.")
                .Build();
            return greenBathroom;
        }
        
        private Room CreateMessyBedroom()
        {
            Room messyBedroom = new RoomBuilder()
                .SetName("Messy Bedroom")
                .SetTag("in a Messy Bedroom.")
                .Build();
            return messyBedroom;
        }
        
        private Room CreateCleanBedroom()
        {
            Room cleanBedroom = new RoomBuilder()
                .SetName("Clean Bedroom")
                .SetTag("in a Clean Bedroom.")
                .Build();
            return cleanBedroom;
        }
        
        private Room CreateHallway()
        {
            Room hallway = new RoomBuilder()
                .SetName("Hallway")
                .SetTag("in a narrow Hallway.")
                .Build();
            return hallway;
        }
        
        private Room CreateGameRoom()
        {
            Room gameRoom = new RoomBuilder()
                .SetName("Game Room")
                .SetTag("in a large Game Room.")
                .Build();
            return gameRoom;
        }
        
        private Room CreateRedBathroom()
        {
            Room redBathroom = new RoomBuilder()
                .SetName("Pipe 4")
                .SetTag("in a Red Bathroom. It is a small bathroom.")
                .SetIsCheckPoint(true)
                .Build();
            return redBathroom;
        }
        
        private Room CreateRedKitchen()
        {
            Room redKitchen = new RoomBuilder()
                .SetName("Red Kitchen")
                .SetTag("in a Red Kitchen.")
                .Build();
            return redKitchen;
        }
        
        private Room CreateRedLivingRoom()
        {
            Room redLivingRoom = new RoomBuilder()
                .SetName("Red Living Room")
                .SetTag("in a Red Living Room.")
                .Build();
            return redLivingRoom;
        }


        /*  public Room CreateWorld()
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
  */
    }
}