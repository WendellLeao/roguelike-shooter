using DownShooter.Gameplay.Weapons.Projectiles;

namespace DownShooter.Gameplay
{
    public interface ICanShoot
    {
        public void Shoot(ProjectileDirection projectileDirection);
    }
}
