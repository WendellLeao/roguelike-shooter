using UnityEngine;
using Leaosoft;
using System;

namespace DownShooter.Gameplay.Enemies
{
    public sealed class Enemy : Entity, IDamageable
    {
        public event Action<Enemy> OnEnemyDead;
        
        [SerializeField] private HealthController _healthController;

        public void TakeDamage(int damage)
        {
            _healthController.RemoveHealth(damage);
        }

        protected override void OnBegin()
        {
            base.OnBegin();

            _healthController.OnDead += HandleDead;
        }

        private void HandleDead()
        {
            OnEnemyDead?.Invoke(this);
        }
    }
}
