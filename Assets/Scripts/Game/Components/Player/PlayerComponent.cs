using UnityEngine;

namespace Game.Components.Player
{
    public struct PlayerComponent
    {
        public KeyCode MoveLeftKey;
        public KeyCode MoveRightKey;
        public float Speed;
        public float Power;
        public int Score;
        public int Id;
    }
}