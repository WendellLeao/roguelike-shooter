using Roguelike.Gameplay.Weapons.Projectiles;
using UnityEngine;

namespace Roguelike.Gameplay
{
    public interface ICanShoot
    {
        public void Shoot(ProjectileDirection projectileDirection);

        public Transform GetTransform();
    }
}
