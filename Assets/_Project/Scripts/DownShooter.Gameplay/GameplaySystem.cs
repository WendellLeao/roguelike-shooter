using DownShooter.Gameplay.Playing;
using DownShooter.Gameplay.Enemies;
using UnityEngine;

namespace DownShooter.Gameplay
{
    public sealed class GameplaySystem : Leaosoft.System
    {
        [SerializeField] private CharacterManager _characterManager;
        [SerializeField] private EnemiesManager _enemiesManager;
        
        protected override void OnInitialize()
        {
            base.OnInitialize();

            _characterManager.Initialize();
            _enemiesManager.Initialize();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            _characterManager.Dispose();
            _enemiesManager.Dispose();
        }

        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);
            
            _characterManager.Tick(deltaTime);
            _enemiesManager.Tick(deltaTime);
        }

        protected override void OnFixedTick(float fixedDeltaTime)
        {
            base.OnFixedTick(fixedDeltaTime);
            
            _characterManager.FixedTick(fixedDeltaTime);
            _enemiesManager.FixedTick(fixedDeltaTime);
        }
    }
}
