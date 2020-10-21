using UnityEngine;

namespace Game {
    public class GameData : MonoBehaviour {
        public bool GameWithBot = false;
        void Start() {
            DontDestroyOnLoad(gameObject);
        }
    }
}