using System.Collections.Generic;
using Game.Components.Pool;
using Game.Customizable;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems {
    public class PoolInit : IEcsInitSystem {
        private readonly EcsWorld _ecsWorld = null;
        private readonly Setup _gameData = null;
        private readonly SceneData _sceneData = null;

        public void Init() {
            GameObject poolObject = _sceneData.pool;
            Pool pool = _gameData.pool;

            EcsEntity poolEntity = _ecsWorld.NewEntity();
            ref var poolComponent = ref poolEntity.Get<PoolComponent>();
            poolComponent.PoolData = new List<Queue<GameObject>>();
            poolComponent.Tag = pool.tag;

            foreach (var prefab in pool.prefabs) {
                var prefabQueue = new Queue<GameObject>();
                for (int i = 0; i < pool.size; ++i) {
                    var instance = GameObject.Instantiate(prefab, poolObject.transform);
                    instance.SetActive(false);
                    prefabQueue.Enqueue(instance);
                }

                poolComponent.PoolData.Add(prefabQueue);
            }
        }
    }
}