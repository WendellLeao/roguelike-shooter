using Leaosoft.Events;

namespace Roguelike.Gameplay.Playing
{
    public sealed class CharacterSpawnedEvent : GameEvent
    {
        public Character Character { get; }
        
        public CharacterSpawnedEvent(Character character)
        {
            Character = character;
        }
    }
}