using UnityEngine;

namespace Game.Customizable {
    [CreateAssetMenu(menuName = "Customizable/" + nameof(Player))]
    public sealed class Player : ScriptableObject {
        [Range(1, 10)] public float speed = 3;
        [Range(5, 20)] public float power = 10;
        public KeyCode moveLeftKey;
        public KeyCode moveRightKey;
        public bool bot = false;
        public GameObject playerPrefab;
        public Vector2 spawnPosition;
    }
}