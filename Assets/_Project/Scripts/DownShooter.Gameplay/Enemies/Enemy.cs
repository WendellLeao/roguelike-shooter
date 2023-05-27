using UnityEngine;
using Leaosoft;
using System;

namespace DownShooter.Gameplay.Enemies
{
    public sealed class Enemy : Entity, IDamageable
    {
        public event Action<Enemy> OnEnemyDead;
        
        [SerializeField] private HealthController _healthController;
        [SerializeField] private EnemyRotation _enemyRotation;
        [SerializeField] private EnemyView _enemyView;
        
        private Transform _targetTransform;

        public void Begin(Transform targetTransform)
        {
            _targetTransform = targetTransform;
            
            Begin();
        }
        
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
            _enemyRotation.Begin(_targetTransform);
            _enemyView.Setup();
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            _healthController.OnDead -= HandleDead;
            
            _healthController.Stop();
            _enemyRotation.Stop();
            _enemyView.Dispose();
        }

        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);

            _enemyRotation.Tick(deltaTime);
        }

        private void HandleDead()
        {
            OnEnemyDead?.Invoke(this);
        }
    }
}
