using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Enemies
{
    public sealed class EnemiesManager : Manager
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private int _enemiesAmount;
        
        private Enemy _enemy;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            for (int i = 0; i < _enemiesAmount; i++)
            {
                Enemy enemy = SpawnEnemy();
                
                RandomizeEnemyPosition(enemy);
            }
        }

        private Enemy SpawnEnemy()
        {
            Enemy enemy = Instantiate(_enemyPrefab, transform);

            enemy.Begin();

            return enemy;
        }

        private void RandomizeEnemyPosition(Enemy enemy)
        {
            Vector3 initialPosition = new Vector3(GetRandomValue(), GetRandomValue(), 0f);

            enemy.transform.position = initialPosition;
        }

        private float GetRandomValue()
        {
            float minimumValue = -4f;
            float maximumValue = 4f;

            float randomValue = Random.Range(minimumValue, maximumValue);
            
            return randomValue;
        }
    }
}
