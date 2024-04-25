using Roguelike.Gameplay.Enemies;
using Roguelike.Gameplay.Maps;
using Roguelike.Gameplay.Playing;
using UnityEngine;

namespace Roguelike.Gameplay
{
    public sealed class GameplaySystem : Leaosoft.System
    {
        [SerializeField]
        private CharacterManager _characterManager;
        [SerializeField]
        private EnemiesManager _enemiesManager;
        [SerializeField]
        private MapsManager _mapsManager;
        
        protected override void OnInitialize()
        {
            base.OnInitialize();

            _enemiesManager.Initialize();
            _characterManager.Initialize();
            _mapsManager.Initialize();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            _enemiesManager.Dispose();
            _characterManager.Dispose();
            _mapsManager.Dispose();
        }

        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);
            
            _enemiesManager.Tick(deltaTime);
            _characterManager.Tick(deltaTime);
        }

        protected override void OnFixedTick(float fixedDeltaTime)
        {
            base.OnFixedTick(fixedDeltaTime);
            
            _enemiesManager.FixedTick(fixedDeltaTime);
            _characterManager.FixedTick(fixedDeltaTime);
        }
    }
}
