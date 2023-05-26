using Leaosoft.Services;
using Leaosoft.Input;
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

        private MapLayout _currentMap;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            SpawnMap(_mapLayoutLobby);

            //DEBUG
            IInputService inputService = ServiceLocator.GetService<IInputService>();

            inputService.OnReadInputs += HandleReadInputs;
            
            void HandleReadInputs(InputsData obj)
            {
                if (obj.PressJump)
                {
                    HandleCharacterCollideDoor();
                }
            }
        }

        private void SpawnMap(MapLayout map)
        {
            if (_currentMap != null)
            {
                DestroyCurrentMap();
            }
            
            _currentMap = Instantiate(map, _gridTransform);

            _currentMap.OnCharacterCollideDoor += HandleCharacterCollideDoor;
        }

        private void DestroyCurrentMap()
        {
            _currentMap.OnCharacterCollideDoor -= HandleCharacterCollideDoor;
                
            Destroy(_currentMap.gameObject);

            _currentMap = null;
        }
        
        private void HandleCharacterCollideDoor()
        {
            MapLayout randomMap = GetRandomMap();
            
            SpawnMap(randomMap);
        }

        private MapLayout GetRandomMap()
        {
            int randomIndex = Random.Range(0, _mapLayouts.Length);

            MapLayout randomMap = _mapLayouts[randomIndex];

            return randomMap;
        }
    }
}
