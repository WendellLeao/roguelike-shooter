using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Enemies
{
    public sealed class EnemyRotation : EntityComponent
    {
        [SerializeField]
        private Transform _weaponTransform;
        [SerializeField]
        private float _rotationSpeed = 200f;

        private Transform _targetTransform;

        public void Begin(Transform targetTransform)
        {
            _targetTransform = targetTransform;

            base.Begin();
        }

        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);

            if (_targetTransform != null)
            {
                RotateTowards(_targetTransform, deltaTime);
            }
        }

        private void RotateTowards(Transform targetTransform, float deltaTime)
        {
            Vector3 targetPosition = targetTransform.position;
            Vector3 weaponPosition = _weaponTransform.position;

            Vector3 newRotation = new Vector3(
                targetPosition.x - weaponPosition.x,
                targetPosition.y - weaponPosition.y, 
                0f
            );
            
            float angle = Mathf.Atan2(newRotation.y, newRotation.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            _weaponTransform.rotation 
                = Quaternion.RotateTowards(_weaponTransform.rotation, targetRotation, maxDegreesDelta: _rotationSpeed * deltaTime);
        }
    }
}