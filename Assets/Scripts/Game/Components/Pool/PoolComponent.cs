using System.Collections.Generic;
using UnityEngine;

namespace Game.Components.Pool {
    public struct PoolComponent {
        public List<Queue<GameObject>> PoolData;
        public string Tag;
    }
}