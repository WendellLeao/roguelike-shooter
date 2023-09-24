using DownShooter.Utilities;
using UnityEngine;

namespace DownShooter.Gameplay.Maps
{
    [CreateAssetMenu(fileName = "MapLayoutData", menuName = PathUtility.MapLayoutsCreateMenuPath + "/MapLayoutData")]
    public sealed class MapLayoutData : ScriptableObject
    {
        [SerializeField] 
        private string _id;
        
        [SerializeField] 
        private GameObject[] _enemiesPrefab;
        
        public GameObject[] EnemiesPrefab => _enemiesPrefab;
    }
}