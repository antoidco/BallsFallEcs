using System.Collections.Generic;
using Game.Customizable;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Game {
    public class SceneData : MonoBehaviour {
        public void Awake() {
            // this is so funny and stupid
            if (SceneManager.GetActiveScene().name != "Menu") { // srsly, how to pass data between scenes?
                if (StartGame.withBot) {
                    players[1].bot = true;
                }
                else {
                    players[1].bot = false;
                }
            }
        }

        public GameObject scoreText;
        public GameObject finishText;
        public List<GameObject> shootingMachines;
        public List<Player> players;
        public GameObject pool;
    }
}