using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils {
    public class StartGameFromMenu : MonoBehaviour {
        private GameData _gameData;

        private void Start() {
            _gameData = FindObjectOfType<GameData>();
        }
        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Game1v1") {
                _gameData.GameWithBot = false;
                SceneManager.LoadScene("Main");
            }

            if (other.tag == "GameWithBot") {
                _gameData.GameWithBot = true;
                SceneManager.LoadScene("Main");
            }
        }
    }
}