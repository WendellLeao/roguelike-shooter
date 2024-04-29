using Leaosoft.Events;

namespace Roguelike.Gameplay.Maps
{
    public sealed class MapSpawnedEvent : GameEvent
    {
        private MapLayout _mapLayout;

        public MapLayout MapLayout => _mapLayout;
        
        // TODO: expose a list of available (non-occupied) spawn points

        public MapSpawnedEvent(MapLayout mapLayout)
        {
            _mapLayout = mapLayout;
        }
    }
}