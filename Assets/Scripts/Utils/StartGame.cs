using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils {
    public class StartGame : MonoBehaviour {
        public static bool withBot = false;

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Game1v1") {
                withBot = false;
                SceneManager.LoadScene("Main");
            }

            if (other.tag == "GameWithBot") {
                withBot = true;
                SceneManager.LoadScene("Main");
            }
        }
    }
}