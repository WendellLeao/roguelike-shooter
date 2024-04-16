using Leaosoft.Services;
using Leaosoft.Pooling;
using UnityEngine;
using Leaosoft;
using System;

namespace Roguelike.Gameplay.Weapons.Projectiles
{
    public sealed class Projectile : Entity
    {
        public event Action<Projectile> OnCollided;
        
        [Header("Objects")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private ProjectileView _projectileView;
        
        [Header("Settings")]
        [SerializeField] private float _force;
        [SerializeField] private int _damage;

        private ICanShoot _currentOwner;
        private IPoolingService _poolingService;
        private string _projectilesPool;
        private bool _hasOwner;

        public void Begin(ICanShoot owner)
        {
            _currentOwner = owner;
            
            _hasOwner = _currentOwner != null;
            
            Begin();
        }

        public void Stop(string projectilesPool)
        {
            _projectilesPool = projectilesPool;
            
            Stop();
        }

        protected override void OnBegin()
        {
            base.OnBegin();

            _poolingService = ServiceLocator.GetService<IPoolingService>();
            
            _projectileView.Setup();
        }

        protected override void OnStop()
        {
            base.OnStop();

            _projectileView.Dispose();

            _currentOwner = null;
            _hasOwner = false;
            
            _poolingService.ReturnObjectToPool(_projectilesPool, gameObject);
        }

        public void AddForceTowards(ProjectileDirection projectileDirection)
        {
            Vector2 forceDirection = GetForceDirection(projectileDirection);
            
            _rigidbody.AddForce(forceDirection * _force);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!ShouldCollideWith(col))
            {
                return;
            }
            
            _rigidbody.AddForce(Vector2.zero);
            
            if (col.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }

            _projectileView.SpawnDestructionParticles();

            OnCollided?.Invoke(this);
        }

        private bool ShouldCollideWith(Collider2D col)
        {
            if (!IsEnabled)
            {
                return false;
            }

            if (_hasOwner)
            {
                //If the projectile has collided with its owner.
                if (col.transform == _currentOwner.GetTransform())
                {
                    return false;
                }
            }
            
            return true;
        }
        
        private Vector2 GetForceDirection(ProjectileDirection projectileDirection)
        {
            switch (projectileDirection)
            {
                case ProjectileDirection.Up:
                {
                    return Vector2.up;
                }
                case ProjectileDirection.Down:
                {
                    return Vector2.down;
                }
                case ProjectileDirection.Left:
                {
                    return Vector2.left;
                }
                case ProjectileDirection.Right:
                {
                    return Vector2.right;
                }
            }

            return Vector2.up;
        }
    }
}