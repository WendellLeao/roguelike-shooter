using Leaosoft.Input;
using UnityEngine;
using Leaosoft;

namespace Roguelike.Gameplay.Playing
{
    public sealed class CharacterMovement : EntityComponent
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private float _movementSpeed;
        
        private IInputService _inputService;
        private CharacterView _characterView;
        
        private Vector2 _movement;

        public void Begin(IInputService inputService, CharacterView characterView)
        {
            _inputService = inputService;
            _characterView = characterView;
            
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

            Vector3 characterBodyOrientation = GetCharacterBodyOrientation(normalizedMovement);
            
            _characterView.RotateCharacterBodyTowards(characterBodyOrientation);
            
            _rigidbody.velocity = normalizedMovement * _movementSpeed;
        }

        private void HandleReadInputs(InputsData inputsData)
        {
            _movement = inputsData.Movement;
        }
        
        private Vector3 GetCharacterBodyOrientation(Vector2 normalizedMovement)
        {
            if (normalizedMovement == Vector2.up)
            {
                return new Vector3(0f, 0f, 0f);
            }
            
            if (normalizedMovement == Vector2.down)
            {
                return new Vector3(0f, 0f, 180f);
            }
            
            if (normalizedMovement == Vector2.left)
            {
                return new Vector3(0f, 0f, 90f);
            }
            
            if (normalizedMovement == Vector2.right)
            {
                return new Vector3(0f, 0f, -90f);
            }
            
            return _characterView.BodyRotation.eulerAngles;
        }
    }
}
