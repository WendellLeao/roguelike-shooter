using DownShooter.Gameplay.Weapons.Projectiles;
using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Weapons
{
    public sealed class Weapon : Entity, ICanShoot
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _spawnPoint;
        
        public void Shoot(ProjectileDirection projectileDirection)
        {
            Projectile projectile = Instantiate(_projectilePrefab);

            projectile.transform.position = _spawnPoint.position;

            projectile.AddForceTowards(projectileDirection);
        }
    }
}
