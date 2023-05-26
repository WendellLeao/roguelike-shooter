using UnityEngine;
using Leaosoft;

namespace DownShooter.Gameplay.Maps
{
    public sealed class MapLayout : Entity
    {
        [SerializeField] private Doors _doors;

        protected override void OnBegin()
        {
            base.OnBegin();
            
            _doors.Begin();
        }

        protected override void OnStop()
        {
            base.OnStop();

            _doors.Stop();
        }
    }
}