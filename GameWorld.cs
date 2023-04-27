using System;
using System.Collections.Generic;
using StarterGame.Enemies;
using StarterGame.Interactables;
using StarterGame.Rooms;

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
            redKitchen.SetExit("east", pipe4);
            redKitchen.SetExit("north", redLivingRoom);
            redLivingRoom.SetExit("south", redKitchen);

            // Set up teleporters
            Interactable orangeKitchenSink = new TeleporterDecorator(new SimpleInteractable("orange-sink", "You approach the kitchen sink. The drain looks large enough to fit a mouse...maybe give it a \'go\'?"), new List<Room>() {redKitchen, blueKitchen});
            orangeKitchen.Interactables.Add(orangeKitchenSink.Name, orangeKitchenSink);
            
            Interactable blueKitchenSink = new TeleporterDecorator(new SimpleInteractable("blue-sink", "You approach the kitchen sink. The drain looks large enough to fit a mouse...maybe give it a \'go\'?"), new List<Room>() {redKitchen, orangeKitchen});
            blueKitchen.Interactables.Add(blueKitchenSink.Name, blueKitchenSink);

            Interactable redKitchenSink = new TeleporterDecorator(new SimpleInteractable("red-sink", "You approach the kitchen sink. The drain looks large enough to fit a mouse...maybe give it a \'go\'?"), new List<Room>() {orangeKitchen, blueKitchen});
            redKitchen.Interactables.Add(redKitchenSink.Name, redKitchenSink);
            
            Interactable mafiaHideoutToRatataxLair = new TeleporterDecorator(new SimpleInteractable("red-door", "You see a red door in the back. Wonder where it could lead. Maybe give it a \'go\'?"), new List<Room>() {CreateRatataxLair()});
            mafiaHideout.Interactables.Add(mafiaHideoutToRatataxLair.Name, mafiaHideoutToRatataxLair);

            return mousetopia;  
        }

        private Room CreateRatataxLair()
        {
            Enemy ratatax = EnemyFactory.CreateEnemy("Ratatax");
            Room ratataxLair = new RoomBuilder()
                .SetName("Ratatax's Lair")
                .SetTag("in Ratatax's Lair. There is a large throne in the middle of the room, there is a large mouse sitting on it surrounded by other mice.")
                .AddEnemy(ratatax)
                .Build();
            return ratataxLair;
        }

        private Room CreateMafiaHideout()
        {
            Interactable briefcase = new SimpleInteractable("briefcase", "You approach the briefcase. The mice around the table are looking at you angrily. Best not to touch it...");
            Room mafiaHideout = new RoomBuilder()
                .SetName("Mafia Hideout")
                .SetTag("in the Mouse Mafia Hideout. There are a few scary looking mice sitting around a table with a briefcase in the middle.")
                .AddInteractable(briefcase)
                .Build();
            return mafiaHideout;
        }
        
        private Room CreateBackAlley()
        {
            Interactable dumpster = new CheeseDecorator(new SimpleInteractable("dumpster", "You approach the dumpster. It smells like cheese..."), 80);
            Room backAlley = new RoomBuilder()
                .SetName("Back Alley")
                .SetTag("in the Back Alley. There are a few dumpsters and abandoned buildings on the sides.")
                .AddInteractable(dumpster)
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
            Interactable group = new SimpleInteractable("group", "You approach a group of mice. You hear them talking about how they wish someone would put a stop to the Mouse Mafia...");
            Interactable fountain = new CheeseDecorator(new SimpleInteractable("fountain", "You approach the fountain. The fountain is made of stone and is shaped like a mouse. Mice are drinking from the fountain."), 15);
            Room cheeseSquare = new RoomBuilder()
                .SetName("Cheese Square")
                .SetTag("in the Cheese Square. The heart of Mousetopia. There is a fountain in the middle of the square. Mice are interacting with each other and there are a few shops on the sides.")
                .AddInteractable(group)
                .AddInteractable(fountain)
                .AddEnemy(new Dog())
                .Build();
            return cheeseSquare;
        }
        
        private Room CreateSewer()
        {
            Room sewer = new RoomBuilder()
                .SetName("Sewer")
                .SetTag("in the Sewer. There are pipes everywhere.")
                .AddEnemy(new Cat())
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
            Interactable fridge = new CheeseDecorator(new SimpleInteractable("fridge", "You approach the fridge. What's that smell..?"), 100);
            Room blueKitchen = new RoomBuilder()
                .SetName("Blue Kitchen")
                .SetTag("in a Blue Kitchen.")
                .AddInteractable(fridge)
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
            Interactable bed = new CheeseDecorator(new SimpleInteractable("bed", "You approach the bed. It looks like someone has been sleeping up there. You search under the bed."), 50);
            Interactable clothes = new CheeseDecorator(new SimpleInteractable("clothes", "You approach the pile of clothes. It has a familiar smell. You search through the pile."), 75);
            Room messyBedroom = new RoomBuilder()
                .SetName("Messy Bedroom")
                .SetTag("in a Messy Bedroom.")
                .AddInteractable(bed)
                .AddInteractable(clothes)
                .Build();
            return messyBedroom;
        }
        
        private Room CreateCleanBedroom()
        {
            Interactable closet = new CheeseDecorator(new SimpleInteractable("closet", "You approach the closet. It is a large closet. You search through the closet."), 25);
            Room cleanBedroom = new RoomBuilder()
                .SetName("Clean Bedroom")
                .SetTag("in a Clean Bedroom.")
                .AddInteractable(closet)
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
            Interactable bowl = new CheeseDecorator(new SimpleInteractable("bowl", "You approach a bowl on the floor. It is filled with popcorn. You search through the bowl."), 100);
            Room gameRoom = new RoomBuilder()
                .SetName("Game Room")
                .SetTag("in a large Game Room.")
                .AddInteractable(bowl)
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
           // Interactable fridge = Interactable.CreateCheeseInteractable("fridge", "You approach the fridge. It appears to be cracked. You search through the fridge.", 100);
            Room redKitchen = new RoomBuilder()
                .SetName("Red Kitchen")
                .SetTag("in a Red Kitchen.")
             //   .AddInteractable(fridge)
                .Build();
            return redKitchen;
        }
        
        private Room CreateRedLivingRoom()
        {
         //   Interactable couch = Interactable.CreateCheeseInteractable("couch", "You approach the couch. It is a large, red couch. You search under the couch.", 50);
            Room redLivingRoom = new RoomBuilder()
                .SetName("Red Living Room")
                .SetTag("in a Red Living Room.")
             //   .AddInteractable(couch)
                .Build();
            return redLivingRoom;
        }
    }
}