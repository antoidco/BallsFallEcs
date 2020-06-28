using UnityEngine;
using System.Collections.Generic;
using Game.GameSetup;

namespace Game {
    [CreateAssetMenu(menuName = "GameSetup/" + nameof(Setup))]
    public sealed class Setup : ScriptableObject {
        public List<Player> players;
        public Level level;
    }
}