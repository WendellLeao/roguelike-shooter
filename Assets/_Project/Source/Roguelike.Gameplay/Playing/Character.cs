using Leaosoft.Services;
using Leaosoft.Input;
using UnityEngine;
using Leaosoft;
using Roguelike.Gameplay.Weapons;

namespace Roguelike.Gameplay.Playing
{
    public sealed class Character : Entity, IDamageable
    {
        [Header("Components")]
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private CharacterShoot _shoot;
        [SerializeField] private HealthController _healthController;

        [Header("Objects")] 
        [SerializeField] private CharacterView _characterView;
        [SerializeField] private Weapon _currentWeapon;

        public HealthController HealthController => _healthController;
        
        public void TakeDamage(int damage)
        {
            _healthController.RemoveHealth(damage);
            
            _characterView.PlayHitAnimation();
        }
        
        protected override void OnBegin()
        {
            base.OnBegin();

            IInputService inputService = ServiceLocator.GetService<IInputService>();
            
            _movement.Begin(inputService, _characterView);
            _shoot.Begin(inputService, _currentWeapon, _characterView);
            _healthController.Begin();
            
            _characterView.Setup();
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            _movement.Stop();
            _shoot.Stop();
            _healthController.Stop();
            
            _characterView.Dispose();
        }

        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);
            
            _shoot.Tick(deltaTime);
        }

        protected override void OnFixedTick(float fixedDeltaTime)
        {
            base.OnFixedTick(fixedDeltaTime);
            
            _movement.FixedTick(fixedDeltaTime);
        }
    }
}
