using Leaosoft.Input;
using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Playing
{
    public sealed class CharacterMovement : EntityComponent
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _movementSpeed;
        
        private IInputService _inputService;
        
        private Vector2 _movement;

        public void Begin(IInputService inputService)
        {
            _inputService = inputService;
            
            Begin();
        }

        protected override void OnBegin()
        {
            base.OnBegin();

            _inputService.OnReadInputs += HandleReadInputs;
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            _inputService.OnReadInputs -= HandleReadInputs;
        }

        protected override void OnFixedTick(float fixedDeltaTime)
        {
            base.OnFixedTick(fixedDeltaTime);

            float horizontalMovement = _movement.x;
            float verticalMovement = _movement.y;

            Vector2 normalizedMovement = new Vector2(horizontalMovement, verticalMovement).normalized;
            
            _rigidbody.velocity = normalizedMovement * _movementSpeed;
        }

        private void HandleReadInputs(InputsData inputsData)
        {
            _movement = inputsData.Movement;
        }
    }
}
