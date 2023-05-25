using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Weapons.Projectiles
{
    public sealed class Projectile : Entity
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _force;
        
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
    }
}