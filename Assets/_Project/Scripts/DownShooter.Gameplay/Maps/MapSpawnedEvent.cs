using Leaosoft.Events;

namespace DownShooter.Gameplay.Maps
{
    public sealed class MapSpawnedEvent : ServiceEvent
    {
        private MapLayout _mapLayout;

        public MapLayout MapLayout => _mapLayout;

        public MapSpawnedEvent(MapLayout mapLayout)
        {
            _mapLayout = mapLayout;
        }
    }
}