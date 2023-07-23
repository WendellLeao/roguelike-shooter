using Leaosoft.Services;
using Leaosoft.Events;
using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Maps
{
    public sealed class MapsManager : Manager
    {
        [Header("Objects")] 
        [SerializeField] private Transform _gridTransform;
        [SerializeField] private MapLayout _mapLayoutLobby;
        [SerializeField] private MapLayout _mapLayoutShop;
        
        [Header("Layouts")]
        [SerializeField] private MapLayout[] _mapLayouts;
        [SerializeField] private int _countToSpawnShop = 5;

        private IEventService _eventService;
        private MapLayout _currentMap;
        private int _lastRandomIndex;
        private int _mapsCount;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _eventService = ServiceLocator.GetService<IEventService>();
            
            _eventService.AddEventListener<CharacterCollideDoorEvent>(HandleCharacterCollideDoor);
            
            SpawnMap(_mapLayoutLobby);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            _eventService.RemoveEventListener<CharacterCollideDoorEvent>(HandleCharacterCollideDoor);
        }

        private void SpawnMap(MapLayout map)
        {
            if (_currentMap != null)
            {
                DestroyCurrentMap();
            }
            
            _currentMap = Instantiate(map, _gridTransform);

            _currentMap.Begin();
            
            _eventService.DispatchEvent(new MapSpawnedEvent(_currentMap));

            _mapsCount++;
        }

        private void DestroyCurrentMap()
        {
            _currentMap.Stop();
            
            Destroy(_currentMap.gameObject);//TODO: Implement pooling

            _currentMap = null;
        }
        
        private void HandleCharacterCollideDoor(ServiceEvent serviceEvent)
        {
            if (serviceEvent is CharacterCollideDoorEvent)
            {
                MapLayout nextMapLayout = null;

                if (_mapsCount <= _countToSpawnShop)
                {
                    nextMapLayout = GetRandomMap();
                }
                else
                {
                    _mapsCount = 0;
                    
                    nextMapLayout = _mapLayoutShop;
                }

                SpawnMap(nextMapLayout);
            }
        }

        private MapLayout GetRandomMap()
        {
            _lastRandomIndex = Random.Range(0, _mapLayouts.Length);
            
            MapLayout randomMap = _mapLayouts[_lastRandomIndex];

            return randomMap;
        }
    }
}
