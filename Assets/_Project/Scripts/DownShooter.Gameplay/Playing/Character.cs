using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Playing
{
    public sealed class Character : Entity
    {
        protected override void OnBegin()
        {
            base.OnBegin();

            Debug.Log("Character has initialized!");
        }
    }
}
