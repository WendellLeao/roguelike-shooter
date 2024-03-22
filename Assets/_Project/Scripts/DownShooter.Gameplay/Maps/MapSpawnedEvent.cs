using Leaosoft.Events;

namespace DownShooter.Gameplay.Maps
{
    public sealed class MapSpawnedEvent : ServiceEvent
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