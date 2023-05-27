using DownShooter.Gameplay.Weapons.Projectiles;
using System.Collections.Generic;
using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Weapons
{
    public sealed class Weapon : Entity
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _spawnPoint;

        private List<Projectile> _activeProjectiles;
        private ICanShoot _currentOwner;

        public void Begin(ICanShoot owner)
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

        protected override void OnBegin()
        {
            base.OnBegin();

            _activeProjectiles = new List<Projectile>();
        }

        protected override void OnStop()
        {
            base.OnStop();

            DestroyActiveProjectiles();
        }

        private void DestroyActiveProjectiles()
        {
            Projectile[] projectilesToDestroy = new Projectile[_activeProjectiles.Count];

            for (int i = 0; i < projectilesToDestroy.Length; i++)
            {
                projectilesToDestroy[i] = _activeProjectiles[i];
            }

            for (int i = 0; i < projectilesToDestroy.Length; i++)
            {
                Projectile projectile = projectilesToDestroy[i];

                projectile.Stop();

                Destroy(projectile.gameObject);
            }
        }
    }
}
