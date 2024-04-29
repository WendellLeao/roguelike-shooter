using System.Collections.Generic;
using Leaosoft.Services;
using Leaosoft.Events;
using UnityEngine;
using Leaosoft;
using Roguelike.Gameplay.Maps;
using Roguelike.Gameplay.Playing;

namespace Roguelike.Gameplay.Enemies
{
    public sealed class EnemiesManager : Manager
    {
        [SerializeField]
        private Enemy _enemyPrefab;

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

        private void HandleCharacterSpawned(GameEvent gameEvent)
        {
            if (gameEvent is CharacterSpawnedEvent characterSpawnedEvent)
            {
                _targetTransform = characterSpawnedEvent.Character.transform;
            }
        }
        
        private void HandleMapSpawned(GameEvent gameEvent)
        {
            if (gameEvent is MapSpawnedEvent mapSpawnedEvent)
            {
                MapLayout mapLayout = mapSpawnedEvent.MapLayout;

                MapLayoutData mapLayoutData = mapLayout.Data;
                MapSpawnPoints mapSpawnPoints = mapLayout.MapSpawnPoints;

                if (mapLayoutData.EnemiesPrefab != null)
                {
                    SpawnEnemiesAndRandomizePosition(mapLayoutData.EnemiesPrefab, mapSpawnPoints);
                }
            }
        }
        
        private void HandleCharacterCollideDoor(GameEvent gameEvent)
        {
            if (gameEvent is CharacterCollideDoorEvent)
            {
                DestroyEnemies();
            }
        }
        
        private void SpawnEnemiesAndRandomizePosition(GameObject[] enemiesPrefab, MapSpawnPoints mapSpawnPoints)
        {
            for (int i = 0; i < enemiesPrefab.Length; i++)
            {
                GameObject enemyPrefab = enemiesPrefab[i];

                Enemy enemy = SpawnEnemy(enemyPrefab);
                
                RandomizeEnemyPosition(enemy, mapSpawnPoints);
                
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

        private void RandomizeEnemyPosition(Enemy enemy, MapSpawnPoints mapSpawnPoints)
        {
            Transform randomSpawnPoint = mapSpawnPoints.GetRandomAvailableSpawnPoint();

            enemy.transform.localPosition = randomSpawnPoint.position;
        }
    }
}
