using Cysharp.Threading.Tasks;
using UnityEngine;
using Leaosoft;
using System;

namespace Roguelike.Gameplay.Enemies
{
    public sealed class EnemyView : EntityView
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _hitColor;
        
        private Color _originalColor;

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
    }
}