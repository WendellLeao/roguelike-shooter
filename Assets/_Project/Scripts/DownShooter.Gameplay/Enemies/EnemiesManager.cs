using System.Collections.Generic;
using DownShooter.Gameplay.Maps;
using DownShooter.Gameplay.Playing;
using Leaosoft.Services;
using Leaosoft.Events;
using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Enemies
{
    public sealed class EnemiesManager : Manager
    {
        [SerializeField] private Enemy _enemyPrefab;

        private IEventService _eventService;
        private List<Enemy> _enemies;
        private Transform _targetTransform;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _enemies = new List<Enemy>();
            
            _eventService = ServiceLocator.GetService<IEventService>();
            
            _eventService.AddEventListener<CharacterSpawnedEvent>(HandleCharacterSpawned);
            _eventService.AddEventListener<MapSpawnedEvent>(HandleMapSpawned);
            _eventService.AddEventListener<CharacterCollideDoorEvent>(HandleCharacterCollideDoor);
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            _eventService.RemoveEventListener<CharacterSpawnedEvent>(HandleCharacterSpawned);
            _eventService.RemoveEventListener<MapSpawnedEvent>(HandleMapSpawned);
            _eventService.RemoveEventListener<CharacterCollideDoorEvent>(HandleCharacterCollideDoor);
            
            DestroyEnemies();
        }

        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);

            for (int i = 0; i < _enemies.Count; i++)
            {
                Enemy enemy = _enemies[i];
                
                enemy.Tick(deltaTime);
            }
        }

        private void HandleCharacterSpawned(ServiceEvent serviceEvent)
        {
            if (serviceEvent is CharacterSpawnedEvent characterSpawnedEvent)
            {
                _targetTransform = characterSpawnedEvent.Character.transform;
            }
        }
        
        private void HandleMapSpawned(ServiceEvent serviceEvent)
        {
            if (serviceEvent is MapSpawnedEvent mapSpawnedEvent)
            {
                MapLayout mapLayout = mapSpawnedEvent.MapLayout;

                MapLayoutData mapLayoutData = mapLayout.Data;
                MapSpawnPoints mapSpawnPoints = mapLayout.MapSpawnPoints; // TODO: spawn enemies in positions

                if (mapLayoutData.EnemiesPrefab != null)
                {
                    SpawnEnemiesAndRandomizePosition(mapLayoutData.EnemiesPrefab);
                }
            }
        }
        
        private void HandleCharacterCollideDoor(ServiceEvent serviceEvent)
        {
            if (serviceEvent is CharacterCollideDoorEvent)
            {
                DestroyEnemies();
            }
        }
        
        private void SpawnEnemiesAndRandomizePosition(GameObject[] enemiesPrefab)
        {
            for (int i = 0; i < enemiesPrefab.Length; i++)
            {
                GameObject enemyPrefab = enemiesPrefab[i];

                Enemy enemy = SpawnEnemy(enemyPrefab);
                
                RandomizeEnemyPosition(enemy);
                
                BeginEnemy(enemy, _targetTransform);
            
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

        private Enemy SpawnEnemy(GameObject enemyPrefab)
        {
            GameObject enemyClone = Instantiate(enemyPrefab, transform);

            Enemy enemy = enemyClone.GetComponent<Enemy>();
            
            return enemy;
        }
        
        private void BeginEnemy(Enemy enemy, Transform targetTransform)
        {
            enemy.OnEnemyDead += HandleEnemyDead;

            enemy.Begin(targetTransform);
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

            enemy.transform.localPosition = initialPosition;
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
