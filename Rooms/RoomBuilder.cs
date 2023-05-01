using StarterGame.Enemies;
using StarterGame.Interactables;

namespace StarterGame.Rooms
{
    public class RoomBuilder
    {
        private readonly Room _room;

        public RoomBuilder()
        {
            _room = new Room();
        }
        
        public RoomBuilder SetName(string name)
        {
            _room.Name = name;
            return this;
        }
        
        public RoomBuilder SetTag(string tag)
        {
            _room.Tag = tag;
            return this;
        }
        
        public RoomBuilder SetIsCheckPoint(bool isCheckPoint)
        {
            _room.IsCheckPoint = isCheckPoint;
            return this;
        }

        public RoomBuilder AddInteractable(Interactable interactable)
        {
            _room.Interactables.Add(interactable.Name, interactable);
            return this;
        }
        
        public RoomBuilder AddEnemy(Enemy enemy)
        {
            _room.Enemy = enemy;
            return this;
        }

        public Room Build()
        {
            return _room;
        }
    }
}