using Leaosoft.Services;
using Leaosoft.Input;
using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Playing
{
    public sealed class Character : Entity
    {
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private CharacterRotation _rotation;
        
        protected override void OnBegin()
        {
            base.OnBegin();

            IInputService inputService = ServiceLocator.GetService<IInputService>();
            
            _movement.Begin(inputService);
            _rotation.Begin();
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            _movement.Stop();
            _rotation.Stop();
        }

        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);
            
            _movement.Tick(deltaTime);
            _rotation.Tick(deltaTime);
        }

        protected override void OnFixedTick(float fixedDeltaTime)
        {
            base.OnFixedTick(fixedDeltaTime);
            
            _movement.FixedTick(fixedDeltaTime);
        }
    }
}
