using System.Collections.Generic;
using Game.Customizable;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Game {
    public class SceneData : MonoBehaviour {
        public GameObject scoreText;
        public GameObject finishText;
        public List<GameObject> shootingMachines;
        public List<Player> players;
        public GameObject pool;
    }
}