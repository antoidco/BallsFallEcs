using Game.Components;
using Game.Components.Map.Bombs;
using Game.Customizable;
using Leopotam.Ecs;

namespace Game.Systems {
    sealed class ShootMachinesInit : IEcsInitSystem {
        readonly EcsWorld _world = null;
        private readonly SceneData _sceneData = null;

        public void Init() {
            foreach (var machineObject in _sceneData.shootingMachines) {
                var machineEntity = _world.NewEntity();
                machineEntity.Get<MachineComponent>().angle = machineObject.GetComponent<ShootMachine>().angle;
                machineEntity.Get<MachineComponent>().frequency = machineObject.GetComponent<ShootMachine>().frequency;
                machineEntity.Get<MachineComponent>().power = machineObject.GetComponent<ShootMachine>().power;
                machineEntity.Get<PositionComponent>().Position = machineObject.transform.position;
            }
        }
    }
}