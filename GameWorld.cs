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
            Room mousetopia = new Room("in the comfort of Mousetopia", "Mousetopia");
            Room sewerEntrance = new Room("at the entrance of the sewer", "Sewer Entrance");
            Room sewer = new Room("in the sewer, connecting Mousetopia to the Giant's territory", "Sewer");
            Room pipeHub = new Room("in the pipe hub, where multiple pipes lead to different Giant's houses", "Pipe Hub");
            Room orangeHouse = new Room("in a orange bathroom", "Pipe 1");
            Room greenHouse = new Room("in a green bathroom", "Pipe 2");
            Room redHouse = new Room("in a red bathroom", "Pipe 3");
            
            mousetopia.SetExit("south", sewerEntrance);
            
            sewerEntrance.SetExit("north", mousetopia);
            sewerEntrance.SetExit("south", sewer);
            
            sewer.SetExit("north", sewerEntrance);
            sewer.SetExit("south", pipeHub);
            
            pipeHub.SetExit("north", sewer);
            pipeHub.SetExit("east", orangeHouse);
            pipeHub.SetExit("west", greenHouse);
            pipeHub.SetExit("south", redHouse);
            
            return mousetopia;
        }
        
    }
}