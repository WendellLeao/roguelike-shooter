using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DownShooter.Gameplay.Maps
{
    public sealed class MapSpawnPoints : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _spawnPoints;

        private Dictionary<Transform, bool> _spawnPointDictionary = new();

        public void ShuffleSpawnPoints()
        {
            System.Random random = new System.Random();
            _spawnPointDictionary = _spawnPointDictionary.OrderBy(x => random.Next())
                .ToDictionary(item => item.Key, item => item.Value);
        }
        
        public Transform GetAvailableSpawnPoint()
        {
            foreach (KeyValuePair<Transform,bool> keyValuePair in _spawnPointDictionary)
            {
                Transform spawnPoint = keyValuePair.Key;
                bool isAvailable = keyValuePair.Value;

                if (isAvailable)
                {
                    _spawnPointDictionary[spawnPoint] = false;
                    return spawnPoint;
                }
            }

            Debug.LogError("There's no available SpawnPoint!");
            return null;
        }

        public Transform GetRandomAvailableSpawnPoint()
        {
            ShuffleSpawnPoints();

            return GetAvailableSpawnPoint();
        }

        private void Awake()
        {
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                Transform spawnPoint = _spawnPoints[i];
                
                _spawnPointDictionary.Add(spawnPoint, true);
            }
        }
    }
}