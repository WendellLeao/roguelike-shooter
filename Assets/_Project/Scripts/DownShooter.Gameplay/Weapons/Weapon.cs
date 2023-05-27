using DownShooter.Gameplay.Weapons.Projectiles;
using System.Collections.Generic;
using Leaosoft.Services;
using Leaosoft.Pooling;
using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Weapons
{
    public sealed class Weapon : Entity
    {
        [Header("Objects")]
        [SerializeField] private PoolData _projectilesPool;
        [SerializeField] private Transform _spawnPoint;
        
        private List<Projectile> _activeProjectiles;
        private IPoolingService _poolingService;
        private ICanShoot _currentOwner;

        private string ProjectilesPool => _projectilesPool.Id;

        public void Begin(ICanShoot owner)
        {
            _currentOwner = owner;
            
            Begin();
        }

        public void Shoot(ProjectileDirection projectileDirection)
        {
            Projectile projectile = GetProjectileFromPool();

            projectile.OnCollided += HandleProjectileCollided;

            projectile.transform.localPosition = _spawnPoint.position;
            
            projectile.Begin(_currentOwner);

            projectile.AddForceTowards(projectileDirection);
        }

        protected override void OnBegin()
        {
            base.OnBegin();

            _activeProjectiles = new List<Projectile>();
            
            _poolingService = ServiceLocator.GetService<IPoolingService>();
        }

        protected override void OnStop()
        {
            base.OnStop();

            StopActiveProjectiles();
        }

        private void HandleProjectileCollided(Projectile projectile)
        {
            StopProjectile(projectile);
        }

        private void StopProjectile(Projectile projectile)
        {
            projectile.OnCollided -= HandleProjectileCollided;
            
            projectile.Stop(ProjectilesPool);
        }
        
        private void StopActiveProjectiles()
        {
            Projectile[] projectilesToDestroy = new Projectile[_activeProjectiles.Count];

            for (int i = 0; i < projectilesToDestroy.Length; i++)
            {
                projectilesToDestroy[i] = _activeProjectiles[i];
            }

            for (int i = 0; i < projectilesToDestroy.Length; i++)
            {
                Projectile projectile = projectilesToDestroy[i];

                StopProjectile(projectile);
            }
        }
        
        private Projectile GetProjectileFromPool()
        {
            GameObject projectileObject = _poolingService.GetObjectFromPool(ProjectilesPool);

            Projectile projectile = projectileObject.GetComponent<Projectile>();

            return projectile;
        }
    }
}
