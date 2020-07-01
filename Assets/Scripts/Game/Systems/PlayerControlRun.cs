using Game.Components;
using Leopotam.Ecs;
using Game.Components.Player;
using UnityEngine;

namespace Game.Systems {
    sealed class PlayerControlRun : IEcsRunSystem {
        readonly EcsWorld _world = null;

        private readonly EcsFilter<BodyComponent, PlayerComponent, MoveComponent>.Exclude<AIComponent> _playerEntities =
            null;

        private Vector3 _force = new Vector3(0, 0, 0);
        private Vector3 _torque = new Vector3(0, 0, 0);

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
            //_torque.y = 0;
            if (Input.GetKey(playerComponent.MoveLeftKey)) {
                _force.x -= (isMaxSpeedLeft ? 0 : 100f);
                //_torque.y -= (isMaxSpeedLeft ? 0 : 100f);
            }

            if (Input.GetKey(playerComponent.MoveRightKey)) {
                _force.x += (isMaxSpeedRight ? 0 : 100f);
                //_torque.y += (isMaxSpeedRight ? 0 : 100f);
            }

            moveComponent.Force = _force * Time.fixedDeltaTime * playerComponent.Power;
            //moveComponent.Tourqe = _force * Time.fixedDeltaTime * playerComponent.Power;
            moveComponent.Tourqe = Vector3.zero;
        }
    }
}