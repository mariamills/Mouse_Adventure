using System.Collections.Generic;

namespace StarterGame.Player
{
    public class PlayerHistory
    {
        private readonly Stack<PlayerState> _playerStates = new Stack<PlayerState>();
        public Stack<Rooms.Room> RoomHistory = new Stack<Rooms.Room>();
        public int Count => _playerStates.Count;

        // Check if there is a previous state to restore
        public bool HasPreviousState()
        {
            return _playerStates.Count > 0;
        }
        
        // Save the current state of the player to the history
        public void SaveState(PlayerState state)
        {
            _playerStates.Push(state);
        }
        
        // Get the previous state of the player from the history
        public PlayerState RestoreState()
        {
              return _playerStates.Pop();
        }
        
        public PlayerState PeekState()
        {
            return _playerStates.Peek();
        }
    }
}