using UnityEngine;
using Leaosoft;

namespace Roguelike.Gameplay.Playing
{
    public sealed class CharacterRotation : EntityComponent
    {
        private Camera _mainCamera;

        protected override void OnBegin()
        {
            base.OnBegin();

            _mainCamera = Camera.main;
        }

        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);

            Vector3 mousePosition = GetMousePosition();

            Transform characterTransform = transform;
            
            Vector3 position = characterTransform.position;
            
            Vector2 direction = new Vector2(
                mousePosition.x - position.x,
                mousePosition.y - position.y
            );

            characterTransform.up = direction;
        }

        private Vector3 GetMousePosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            
            mousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);

            return mousePosition;
        }
    }
}
