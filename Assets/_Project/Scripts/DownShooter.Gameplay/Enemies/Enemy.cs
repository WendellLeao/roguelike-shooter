using DownShooter.Gameplay.Weapons.Projectiles;
using DownShooter.Gameplay.Weapons;
using UnityEngine;
using Leaosoft;
using System;

namespace DownShooter.Gameplay.Enemies
{
    public sealed class Enemy : Entity, IDamageable, ICanShoot
    {
        public event Action<Enemy> OnEnemyDead;
        
        [Header("Components")]
        [SerializeField] private HealthController _healthController;
        [SerializeField] private EnemyRotation _enemyRotation;
        
        [Header("Objects")]
        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private Weapon _currentWeapon;

        private Transform _targetTransform;
        
        public void Begin(Transform targetTransform)
        {
            _targetTransform = targetTransform;

            base.Begin();
        }
        
        public void TakeDamage(int damage)
        {
            _enemyView.PlayHitAnimation();
                
            _healthController.RemoveHealth(damage);
        }
        
        public void Shoot(ProjectileDirection projectileDirection)
        {
            _currentWeapon.Shoot(projectileDirection);
        }
        
        protected override void OnBegin()
        {
            base.OnBegin();

            _healthController.OnDead += HandleDead;
            
            _healthController.Begin();
            _enemyRotation.Begin(_targetTransform);
            
            _enemyView.Setup();
            
            _currentWeapon.Begin(owner: this);

            Shoot(ProjectileDirection.Up);
        }
        
        protected override void OnStop()
        {
            base.OnStop();
            
            _healthController.OnDead -= HandleDead;
            
            _healthController.Stop();
            _enemyRotation.Stop();
            
            _enemyView.Dispose();
            
            _currentWeapon.Stop();
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

        public Transform GetTransform()
        {
            return transform;
        }
    }
}
