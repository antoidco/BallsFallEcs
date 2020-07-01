using System.Collections.Generic;
using Game.Components;
using Game.Components.Map.Bombs;
using Game.Components.Pool;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems {
    sealed class ShootMachinesRun : IEcsRunSystem {
        readonly EcsWorld _world = null;
        private readonly EcsFilter<MachineComponent, PositionComponent> _machineEntities = null;

        public void Run() {
            foreach (int machine in _machineEntities) {
                var machineComponent = _machineEntities.Get1(machine);
                var framesPerMachineFreq = (int) ((1.0 / machineComponent.frequency) / Time.deltaTime);
                if (Time.frameCount % framesPerMachineFreq == 0) {
                    var positionComponent = _machineEntities.Get2(machine);
                    ref var machineEntity = ref _machineEntities.GetEntity(machine);
                    SpawnItem spawnItem;
                    spawnItem.Position = positionComponent.Position;
                    spawnItem.Rotation = Random.rotation;
                    float angle1 = Random.Range(-machineComponent.angle, machineComponent.angle);
                    float angle2 = Random.Range(-machineComponent.angle, machineComponent.angle);
                    var velocityX = machineComponent.power * Mathf.Sin(Mathf.Deg2Rad * angle1);
                    var velocityY = machineComponent.power * Mathf.Sin(Mathf.Deg2Rad * angle2);
                    var velocityZ = -machineComponent.power;
                    spawnItem.Velocity = new Vector3(velocityX, velocityY, velocityZ);
                    spawnItem.SetNewPosition = true;
                    spawnItem.SetNewRotation = true;
                    spawnItem.SetNewVelocity = true;
                    spawnItem.Tag = "Bomb";
                    spawnItem.PrefabIndex = -1;
                    machineEntity.Get<PoolSpawnFrame>().ToSpawn = new List<SpawnItem> {spawnItem};
                }
            }
        }
    }
}