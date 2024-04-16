using UnityEngine;
using Leaosoft;
using System;

namespace Roguelike.Gameplay
{
    public sealed class HealthController : EntityComponent
    {
        public event Action OnDead;
        
        [SerializeField] private int _maximumHealth = 100;

        private int _currentHealth;

        protected override void OnBegin()
        {
            base.OnBegin();

            _currentHealth = _maximumHealth;
        }

        public void AddHealth(int amount)
        {
            _currentHealth += amount;

            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maximumHealth);
        }

        public void RemoveHealth(int amount)
        {
            _currentHealth -= amount;
            
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maximumHealth);

            if (_currentHealth <= 0)
            {
                OnDead?.Invoke();
            }
        }
    }
}