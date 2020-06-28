using Game.Components;
using Game.Components.Player;
using Leopotam.Ecs;

namespace Game.Systems {
    public class PlayerMoveRun : IEcsRunSystem {
        
        private readonly EcsFilter<BodyComponent, PlayerComponent, MoveComponent> _playerEntities =
            null;
        
        public void Run() {
            foreach (int player in _playerEntities) {
                ref var bodyComponent = ref _playerEntities.Get1(player);
                ref var moveComponent = ref _playerEntities.Get3(player);
                bodyComponent.Body.AddForce(moveComponent.Force);
                bodyComponent.Body.AddTorque(moveComponent.Tourqe);
            }
        }
    }
}