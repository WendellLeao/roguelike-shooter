using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Weapons.Projectiles
{
    public sealed class ProjectileView : EntityView
    {
        [SerializeField] private ParticleSystem _particlesPrefab;
        
        public void SpawnDestructionParticles()
        {
            ParticleSystem particles = Instantiate(_particlesPrefab);

            particles.transform.position = transform.position;
            
            particles.Play();
        }
    }
}