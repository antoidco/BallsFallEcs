using UnityEngine;
using System.Collections.Generic;
using Game.Customizable;

namespace Game {
    [CreateAssetMenu(menuName = "Customizable/" + nameof(Setup))]
    public sealed class Setup : ScriptableObject {
        public Level level;
        public Pool pool;
    }
}