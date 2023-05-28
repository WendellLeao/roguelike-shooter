using UnityEngine;

namespace DownShooter.Gameplay.Weapons
{
    [CreateAssetMenu(menuName = "DownShooter/Weapons/WeaponData", fileName = "NewWeaponData")]
    public sealed class WeaponData : ScriptableObject
    {
        [SerializeField] private float _fireRate;
        [SerializeField] private int _maximumAmmo;
        [SerializeField] private bool _infiniteAmmo;

        public float FireRate => _fireRate;
        public int MaximumAmmo => _maximumAmmo;
        public bool InfiniteAmmo => _infiniteAmmo;
    }
}