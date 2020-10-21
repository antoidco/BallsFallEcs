using System.Collections.Generic;
using Game.Components.Pool;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems {
    class PoolSpawnRun : IEcsRunSystem {
        private readonly EcsFilter<PoolComponent> _poolEntities = null;
        private readonly EcsFilter<PoolSpawnFrame> _spawnEntities = null;

        public void Run() {
            foreach (var i in _poolEntities) {
                ref PoolComponent poolComponent = ref _poolEntities.Get1(i);
                foreach (var j in _spawnEntities) {
                    var poolSpawnEvent = _spawnEntities.Get1(j);
                    for (int k = 0; k < poolSpawnEvent.ToSpawn.Count; ++k) {
                        if (poolSpawnEvent.ToSpawn[k].Tag == poolComponent.Tag) {
                            Spawn(ref poolComponent.PoolData, poolSpawnEvent.ToSpawn[k]);
                        }
                    }
                }
            }
        }
        private GameObject Spawn(ref List<Queue<GameObject>> poolData, SpawnItem item) {
            if (poolData.Count > 0) {
                if (item.PrefabIndex < 0) item.PrefabIndex = Random.Range(0, poolData.Count);
                GameObject instance = poolData[item.PrefabIndex].Dequeue();
                instance.SetActive(true);
                if (item.SetNewPosition) instance.transform.position = item.Position;
                if (item.SetNewRotation) instance.transform.rotation = item.Rotation;
                if (item.SetNewVelocity) instance.GetComponent<Rigidbody>().velocity = item.Velocity; // too bad!
                poolData[item.PrefabIndex].Enqueue(instance);

                return instance;
            }

            return null;
        }
    }
}