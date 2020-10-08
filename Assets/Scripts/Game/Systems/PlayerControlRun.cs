using Game.Components;
using Leopotam.Ecs;
using Game.Components.Player;
using Game.UI;
using UnityEngine;

namespace Game.Systems {
    sealed class PlayerControlRun : IEcsRunSystem {
        readonly EcsWorld _world = null;
        private readonly InputManager _inputManager = null;

        private readonly EcsFilter<BodyComponent, PlayerComponent, MoveComponent>.Exclude<AIComponent> _playerEntities =
            null;

        private Vector3 _force = new Vector3(0, 0, 0);

        public void Run() {
            foreach (int player in _playerEntities) {
                PlayerControl(ref _playerEntities.Get1(player), _playerEntities.Get2(player),
                    ref _playerEntities.Get3(player));
            }
        }

        private void PlayerControl(ref BodyComponent bodyComponent, PlayerComponent playerComponent,
            ref MoveComponent moveComponent) {
            var velX = bodyComponent.Body.velocity.x;
            bool isMaxSpeedLeft = velX <= -playerComponent.Speed;
            bool isMaxSpeedRight = velX >= playerComponent.Speed;

            _force.x = 0;
            if (Input.GetKey(playerComponent.MoveLeftKey) 
                || _inputManager.UIControls[playerComponent.Id].LeftButton.Pressed) {
                _force.x -= (isMaxSpeedLeft ? 0 : 1f);
            }

            if (Input.GetKey(playerComponent.MoveRightKey)
                || _inputManager.UIControls[playerComponent.Id].RightButton.Pressed) {
                _force.x += (isMaxSpeedRight ? 0 : 1f);
            }

            moveComponent.Force = _force * playerComponent.Power;
            moveComponent.Tourqe = Vector3.zero;
        }
    }
}