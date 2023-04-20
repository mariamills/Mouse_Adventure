using System.Collections.Generic;

namespace StarterGame
{
    public class History
    {
        private readonly Stack<PlayerState> _playerStates = new Stack<PlayerState>();
        
        // Check if there is a previous state to restore
        public bool HasState()
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
    }
}