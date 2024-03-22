using UnityEngine;

namespace DownShooter.Gameplay.Maps
{
    public sealed class MapSpawnPoints : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _spawnPoints;

        public void ShuffleSpawnPoints()
        {
            
        }
        
        public Transform GetAvailableSpawnPoint()
        {
            return _spawnPoints[0];
        }
    }
}