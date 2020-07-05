using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils {
    public class StartGame : MonoBehaviour {
        public static bool withBot = false;
        public MenuStartup menuStartup;

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Game1v1") {
                withBot = false;
                Destroy(menuStartup);
                menuStartup = null;
                SceneManager.LoadScene("Main");
            }

            if (other.tag == "GameWithBot") {
                withBot = true;
                Destroy(menuStartup);
                menuStartup = null;
                SceneManager.LoadScene("Main");
            }
        }
    }
}