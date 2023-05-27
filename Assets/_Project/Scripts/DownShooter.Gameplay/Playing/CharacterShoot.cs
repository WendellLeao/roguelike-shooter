using DownShooter.Gameplay.Weapons.Projectiles;
using DownShooter.Gameplay.Weapons;
using Leaosoft.Input;
using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Playing
{
    public sealed class CharacterShoot : EntityComponent, ICanShoot
    {
        private IInputService _inputService;
        
        private Weapon _currentWeapon;
        private Vector2 _shoot;

        public void Begin(IInputService inputService, Weapon weapon)
        {
            _inputService = inputService;
            _currentWeapon = weapon;
            
            Begin();
        }

        protected override void OnBegin()
        {
            base.OnBegin();

            _inputService.OnReadInputs += HandleReadInputs;
            
            _currentWeapon.Begin(owner: this);
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            _inputService.OnReadInputs -= HandleReadInputs;
            
            _currentWeapon.Stop();
        }

        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);

            if (IsShooting(_shoot))
            {
                ProjectileDirection projectileDirection = GetProjectileDirection(_shoot);
                
                Shoot(projectileDirection);
            }
        }

        private void HandleReadInputs(InputsData inputsData)
        {
            _shoot = inputsData.Shoot;
        }
        
        private bool IsShooting(Vector2 shoot)
        {
            return shoot != Vector2.zero;
        }
        
        private ProjectileDirection GetProjectileDirection(Vector2 shoot)
        {
            if (shoot == Vector2.up)
            {
                return ProjectileDirection.Up;
            }
            
            if (shoot == Vector2.down)
            {
                return ProjectileDirection.Down;
            }
            
            if (shoot == Vector2.left)
            {
                return ProjectileDirection.Left;
            }
            
            if (shoot == Vector2.right)
            {
                return ProjectileDirection.Right;
            }
            
            return ProjectileDirection.Up;
        }

        public void Shoot(ProjectileDirection projectileDirection)
        {
            _currentWeapon.Shoot(projectileDirection);
                
            _shoot = Vector2.zero;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}