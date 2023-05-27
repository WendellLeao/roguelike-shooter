using System.Collections.Generic;
using DownShooter.Gameplay.Maps;
using Leaosoft.Services;
using Leaosoft.Events;
using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Enemies
{
    public sealed class EnemiesManager : Manager
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private int _enemiesAmount;

        private List<Enemy> _enemies;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _enemies = new List<Enemy>();
            
            IEventService eventService = ServiceLocator.GetService<IEventService>();
            
            eventService.AddEventListener<CharacterCollideDoorEvent>(HandleCharacterCollideDoor);
            
            SpawnEnemiesAndRandomizePosition();
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            DestroyEnemies();
        }
        
        private void HandleCharacterCollideDoor(ServiceEvent serviceEvent)
        {
            if (serviceEvent is CharacterCollideDoorEvent)
            {
                DestroyEnemies();
            
                SpawnEnemiesAndRandomizePosition();
            }
        }
        
        private void SpawnEnemiesAndRandomizePosition()
        {
            for (int i = 0; i < _enemiesAmount; i++)
            {
                Enemy enemy = SpawnEnemy();

                enemy.OnEnemyDead += HandleEnemyDead;
            
                RandomizeEnemyPosition(enemy);
                
                enemy.Begin();

                _enemies.Add(enemy);
            }
        }

        private void DestroyEnemies()
        {
            Enemy[] enemiesToDestroy = new Enemy[_enemies.Count];
            
            for (int i = 0; i < enemiesToDestroy.Length; i++)
            {
                enemiesToDestroy[i] = _enemies[i];
            }
            
            for (int i = 0; i < enemiesToDestroy.Length; i++)
            {
                Enemy enemy = enemiesToDestroy[i];

                DestroyEnemy(enemy);
            }

            _enemies.Clear();
        }

        private Enemy SpawnEnemy()
        {
            Enemy enemy = Instantiate(_enemyPrefab, transform);

            return enemy;
        }

        private void DestroyEnemy(Enemy enemy)
        {
            _enemies.Remove(enemy);
            
            enemy.OnEnemyDead -= HandleEnemyDead;
            
            enemy.Stop();
                
            Destroy(enemy.gameObject);//TODO: Implement pooling
        }

        private void HandleEnemyDead(Enemy enemy)
        {
            DestroyEnemy(enemy);
        }

        private void RandomizeEnemyPosition(Enemy enemy)
        {
            Vector3 initialPosition = new Vector3(GetRandomValue(), GetRandomValue(), 0f);

            enemy.transform.position = initialPosition;
        }

        private float GetRandomValue()//TODO: implement this
        {
            float minimumValue = -4f;
            float maximumValue = 4f;

            float randomValue = Random.Range(minimumValue, maximumValue);
            
            return randomValue;
        }
    }
}
