namespace StarterGame
{
    public class PlayerState
    {
        public Room CurrentRoom { get; }
        public int Currency { get; }
        public int Lives { get; }
        public int Deaths { get; }

        public PlayerState(Room currentRoom, int currency, int lives, int deaths)
        {
            CurrentRoom = currentRoom;
            Currency = currency;
            Lives = lives;
            Deaths = deaths;
        }
        
    }
}