using Leaosoft.UI.Screens;
using UnityEngine;

namespace Roguelike.UI
{
    public class TestScreen : UIScreen
    {
        protected override void OnOpen()
        {
            base.OnOpen();

            Debug.Log("On Open Test Screen");
        }
    }
}
