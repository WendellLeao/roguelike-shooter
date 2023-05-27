using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Weapons.Projectiles
{
    public sealed class Projectile : Entity
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _force;
        [SerializeField] private int _damage;

        private Entity _currentOwner;
        private bool _hasHitSomething;

        public void Begin(Entity owner)
        {
            _currentOwner = owner;
            
            Begin();
        }
        
        public void AddForceTowards(ProjectileDirection projectileDirection)
        {
            switch (projectileDirection)
            {
                case ProjectileDirection.Up:
                {
                    _rigidbody.AddForce(Vector2.up * _force);
                    
                    break;
                }
                case ProjectileDirection.Down:
                {
                    _rigidbody.AddForce(Vector2.down * _force);
                    
                    break;
                }
                case ProjectileDirection.Left:
                {
                    _rigidbody.AddForce(Vector2.left * _force);
                    
                    break;
                }
                case ProjectileDirection.Right:
                {
                    _rigidbody.AddForce(Vector2.right * _force);
                    
                    break;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_hasHitSomething)
            {
                return;
            }

            if (_currentOwner.transform != null && col.transform == _currentOwner.transform)//TODO: Clean this
            {
                return;
            }
            
            _hasHitSomething = true;
            
            if (col.transform.TryGetComponent(out IDamageable damageable))
            {
                Debug.Log("hit DAMAGEABLE");
                
                damageable.TakeDamage(_damage);
            }
        }
    }
}