using System.Collections.Generic;
using UnityEngine;

namespace Game.Components.Pool {
    public struct PoolSpawnFrame {
        public List<SpawnItem> ToSpawn;
    }

    public struct SpawnItem {
        public string Tag;
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Velocity;
        public bool SetNewPosition;
        public bool SetNewVelocity;
        public bool SetNewRotation;
        public int PrefabIndex;
    }
}