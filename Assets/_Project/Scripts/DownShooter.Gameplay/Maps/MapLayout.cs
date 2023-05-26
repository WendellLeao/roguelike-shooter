using DownShooter.Gameplay.Playing;
using UnityEngine;
using System;

namespace DownShooter.Gameplay.Maps
{
    public sealed class MapLayout : MonoBehaviour
    {
        public event Action OnCharacterCollideDoor;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out Character character))
            {
                OnCharacterCollideDoor?.Invoke();
            }
        }
    }
}