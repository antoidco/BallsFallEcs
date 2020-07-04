using UnityEngine;

namespace Game.Customizable {
    public class Player : MonoBehaviour {
        [Range(1, 10)] public float speed = 6;
        [Range(5, 20)] public float power = 10;
        public KeyCode moveLeftKey;
        public KeyCode moveRightKey;
        public bool bot = false;
        public Vector2 spawnPosition;
    }
}