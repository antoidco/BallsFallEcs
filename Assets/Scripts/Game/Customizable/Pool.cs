using System.Collections.Generic;
using UnityEngine;

namespace Game.Customizable {
    [CreateAssetMenu(menuName = "Customizable/" + nameof(Pool))]
    public sealed class Pool : ScriptableObject {
        public string tag;
        public List<GameObject> prefabs;
        public int size;
    }
}