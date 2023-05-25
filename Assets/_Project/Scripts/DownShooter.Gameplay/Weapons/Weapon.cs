using DownShooter.Gameplay.Weapons.Projectiles;
using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Weapons
{
    public sealed class Weapon : Entity, ICanShoot
    {
        [SerializeField] private Projectile _projectilePrefab;
        
        public void Shoot(ProjectileDirection projectileDirection)
        {
            Debug.Log("Shoot direction: " + projectileDirection);

            Projectile projectile = Instantiate(_projectilePrefab, transform);

            projectile.AddForceTowards(projectileDirection);
        }
    }
}
