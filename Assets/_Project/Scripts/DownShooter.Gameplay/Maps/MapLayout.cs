using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Maps
{
    public sealed class MapLayout : Entity
    {
        [SerializeField]
        private Doors _doors;
        [SerializeField]
        private MapLayoutData _data;
        [SerializeField]
        private MapSpawnPoints _mapSpawnPoints;

        public MapLayoutData Data => _data;
        public MapSpawnPoints MapSpawnPoints => _mapSpawnPoints;
        
        protected override void OnBegin()
        {
            base.OnBegin();
            
            _doors.Begin();
        }

        protected override void OnStop()
        {
            base.OnStop();

            _doors.Stop();
        }
    }
}