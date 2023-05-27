using UnityEngine;
using Leaosoft;
using System;

namespace DownShooter.Gameplay.Enemies
{
    public sealed class Enemy : Entity, IDamageable
    {
        public event Action<Enemy> OnEnemyDead;
        
        [SerializeField] private HealthController _healthController;
        [SerializeField] private EnemyView _enemyView;

        public void TakeDamage(int damage)
        {
            _enemyView.PlayHitAnimation();
                
            _healthController.RemoveHealth(damage);
        }

        protected override void OnBegin()
        {
            base.OnBegin();

            _healthController.OnDead += HandleDead;
            
            _healthController.Begin();
            _enemyView.Setup();
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            _healthController.Stop();
            _enemyView.Dispose();
        }

        private void HandleDead()
        {
            OnEnemyDead?.Invoke(this);
        }
    }
}
