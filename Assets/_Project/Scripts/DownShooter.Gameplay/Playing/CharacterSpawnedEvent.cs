using Leaosoft.Events;

namespace DownShooter.Gameplay.Playing
{
    public sealed class CharacterSpawnedEvent : ServiceEvent
    {
        public Character Character { get; }
        
        public CharacterSpawnedEvent(Character character)
        {
            Character = character;
        }
    }
}