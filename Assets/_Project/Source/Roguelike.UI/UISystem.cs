using Leaosoft.UI;
using UnityEngine;

namespace Roguelike.UI
{
    public sealed class UISystem : Leaosoft.System
    {
        [SerializeField]
        private ScreensManager _screensManager;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            _screensManager.Initialize();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            _screensManager.Dispose();
        }
    }
}
