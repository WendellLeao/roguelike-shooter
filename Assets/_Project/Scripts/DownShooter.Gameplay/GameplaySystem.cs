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
    }
}
