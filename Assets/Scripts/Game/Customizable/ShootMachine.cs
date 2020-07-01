using System.Collections.Generic;
using UnityEngine;

namespace Game.Customizable {
    public class ShootMachine : MonoBehaviour
    {
        public List<GameObject> bombPrefabs;
        public float angle = 90; // [degrees]
        public float frequency = 2; // [Hz]
        public float power = 3; // bomb velocity
    }
}
