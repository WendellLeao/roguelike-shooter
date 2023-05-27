using DownShooter.Gameplay.Weapons.Projectiles;
using UnityEngine;

namespace DownShooter.Gameplay
{
    public interface ICanShoot
    {
        void Shoot(ProjectileDirection projectileDirection);

        Transform GetTransform();
    }
}
