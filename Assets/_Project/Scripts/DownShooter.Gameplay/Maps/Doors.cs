﻿using DownShooter.Gameplay.Playing;
using Leaosoft.Services;
using Leaosoft.Events;
using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Maps
{
    public sealed class Doors : Entity
    {
        private IEventService _eventService;
        private bool _hasCollideWithCharacter;

        protected override void OnBegin()
        {
            base.OnBegin();

            _eventService = ServiceLocator.GetService<IEventService>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_hasCollideWithCharacter)
            {
                return;
            }
            
            if (col.transform.TryGetComponent(out Character character))
            {
                Debug.Log("Character collide");

                _eventService.DispatchEvent(new CharacterCollideDoorEvent());
                
                _hasCollideWithCharacter = true;
            }
        }
    }
}