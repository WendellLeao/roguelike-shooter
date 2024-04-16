using Roguelike.Gameplay.Weapons.Projectiles;
using UnityEngine;

namespace Roguelike.Gameplay
{
    public interface ICanShoot
    {
        void Shoot(ProjectileDirection projectileDirection);

        Transform GetTransform();
    }
}
