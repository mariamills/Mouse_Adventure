using StarterGame.Rooms;

namespace StarterGame.Player
{
    public class PlayerState
    {
        public Room CurrentRoom { get; }
        public int Currency { get; }

        public PlayerState(Room currentRoom, int currency)
        {
            CurrentRoom = currentRoom;
            Currency = currency;
        }
        
    }
}