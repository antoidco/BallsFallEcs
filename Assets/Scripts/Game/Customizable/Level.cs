using UnityEngine;

namespace Game.Customizable {
    [CreateAssetMenu(menuName = "Customizable/" + nameof(Level))]
    public sealed class Level : ScriptableObject {
        [Range(50, 200)] public int sizeHorizontal = 80;
        [Range(10, 200)] public int sizeVertical = 100;
        [Range(0, 0.2f)] public float wallDensity = 0.01f;
        [Range(10, 20)] public int wallLengthAverage = 10;
        [Range(0, 9)] public int wallLengthDelta = 3;
        [Range(0, 5)] public int finishCount = 2;
        public GameObject wallPrefab;
        public GameObject finishPrefab;
        public KeyCode restartKey;
    }
}