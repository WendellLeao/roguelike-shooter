using DownShooter.Gameplay.Weapons;
using Leaosoft.Services;
using Leaosoft.Input;
using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Playing
{
    public sealed class Character : Entity
    {
        [Header("Components")]
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private CharacterShoot _shoot;

        [Header("Objects")] 
        [SerializeField] private Weapon _currentWeapon;
        
        protected override void OnBegin()
        {
            base.OnBegin();

            IInputService inputService = ServiceLocator.GetService<IInputService>();
            
            _currentWeapon.Begin();
            
            _movement.Begin(inputService);
            _shoot.Begin(inputService, _currentWeapon);
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            _currentWeapon.Stop();

            _movement.Stop();
            _shoot.Stop();
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
