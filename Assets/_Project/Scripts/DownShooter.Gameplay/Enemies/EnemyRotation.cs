using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Enemies
{
    public sealed class EnemyRotation : EntityComponent
    {
        [SerializeField] private float _rotationSpeed = 200f;
        
        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);

            // if (_targetTransform != null)
            // {
            //     RotateTowards(_targetTransform, deltaTime);
            // }
        }

        private void RotateTowards(Transform targetTransform, float deltaTime)
        {
            Vector3 targetPosition = targetTransform.position;
            Vector3 enemyPosition = transform.position;

            Vector3 newRotation = new Vector3(
                targetPosition.x - enemyPosition.x,
                targetPosition.y - enemyPosition.y, 
                0f
            );
            
            float angle = Mathf.Atan2(newRotation.y, newRotation.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            transform.rotation 
                = Quaternion.RotateTowards(transform.rotation, targetRotation, maxDegreesDelta: _rotationSpeed * deltaTime);
        }
    }
}