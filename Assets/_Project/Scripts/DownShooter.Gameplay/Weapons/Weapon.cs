using DownShooter.Gameplay.Weapons.Projectiles;
using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Weapons
{
    public sealed class Weapon : Entity, ICanShoot
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _spawnPoint;
        
        private Entity _currentOwner;

        public void Begin(Entity owner)
        {
            _currentOwner = owner;
            
            Begin();
        }
        
        public void Shoot(ProjectileDirection projectileDirection)
        {
            Projectile projectile = Instantiate(_projectilePrefab);

            projectile.Begin(_currentOwner);
            
            projectile.transform.position = _spawnPoint.position;

            projectile.AddForceTowards(projectileDirection);
        }
    }
}
