using DownShooter.Gameplay.Playing;
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

        private IEventService _eventService;
        private List<Enemy> _enemies;
        private Character _character;
        private Transform _characterTransform;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _enemies = new List<Enemy>();
            
            _eventService = ServiceLocator.GetService<IEventService>();
            
            _eventService.AddEventListener<CharacterSpawnedEvent>(HandleCharacterSpawned);
            _eventService.AddEventListener<CharacterCollideDoorEvent>(HandleCharacterCollideDoor);
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            _eventService.RemoveEventListener<CharacterSpawnedEvent>(HandleCharacterSpawned);
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
                Character character = characterSpawnedEvent.Character;

                _characterTransform = character.transform;
                
                SpawnEnemiesAndRandomizePosition();
            }
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

                RandomizeEnemyPosition(enemy);
                
                BeginEnemy(enemy);

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
        
        private void BeginEnemy(Enemy enemy)
        {
            enemy.OnEnemyDead += HandleEnemyDead;

            enemy.Begin(_characterTransform);
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
