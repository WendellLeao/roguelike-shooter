using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Gameplay.Weapons
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = PathUtility.WeaponsCreateMenuPath + "/WeaponData")]
    public sealed class WeaponData : ScriptableObject
    {
        [SerializeField]
        private float _fireRate;
        [SerializeField]
        private int _maximumAmmo;
        [SerializeField]
        private bool _infiniteAmmo;

        public float FireRate => _fireRate;
        public int MaximumAmmo => _maximumAmmo;
        public bool InfiniteAmmo => _infiniteAmmo;
    }
}