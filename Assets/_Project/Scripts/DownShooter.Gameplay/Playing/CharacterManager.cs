using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Playing
{
    public sealed class CharacterManager : Manager
    {
        [SerializeField] private Character _characterPrefab;
        
        private Character _character;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _character = Instantiate(_characterPrefab);
            
            _character.Begin();
        }
    }
}
