using Cysharp.Threading.Tasks;
using UnityEngine;
using Leaosoft;
using System;
using Roguelike.Gameplay.Weapons.Projectiles;

namespace Roguelike.Gameplay.Playing
{
    public sealed class CharacterView : EntityView
    {
        [Header("Objects")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _headTransform;
        [SerializeField] private Transform _bodyTransform;
        
        [Header("Settings")]
        [SerializeField] private Color _hitColor;
        
        private Color _originalColor;

        public Quaternion BodyRotation => _bodyTransform.rotation;

        public void RotateCharacterHeadTowards(ProjectileDirection projectileDirection)
        {
            Vector3 newRotation = ConvertProjectileDirectionToVector(projectileDirection);

            _headTransform.rotation = Quaternion.Euler(newRotation);
        }
        
        public void RotateCharacterBodyTowards(Vector3 bodyRotation)
        {
            _bodyTransform.rotation = Quaternion.Euler(bodyRotation);
        }
        
        public void PlayHitAnimation()
        {
            UniTask async = PlayHitAnimationAsync();
        }
        
        protected override void OnSetup()
        {
            base.OnSetup();

            _originalColor = _spriteRenderer.color;
        }

        private async UniTask PlayHitAnimationAsync()
        {
            _spriteRenderer.color = _hitColor;

            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));

            if (IsEnabled)
            {
                _spriteRenderer.color = _originalColor;
            }
        }
        
        private Vector3 ConvertProjectileDirectionToVector(ProjectileDirection projectileDirection)
        {
            switch (projectileDirection)
            {
                case ProjectileDirection.Up:
                {
                    return new Vector3(0f, 0f, 0f);
                }
                case ProjectileDirection.Down:
                {
                    return new Vector3(0f, 0f, 180f);
                }
                case ProjectileDirection.Left:
                {
                    return new Vector3(0f, 0f, 90f);
                }
                case ProjectileDirection.Right:
                {
                    return new Vector3(0f, 0f, -90f);
                }
            }

            Quaternion headRotation = _headTransform.rotation;

            return headRotation.eulerAngles;
        }
    }
}