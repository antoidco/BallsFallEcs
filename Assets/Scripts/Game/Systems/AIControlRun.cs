using Game.Components;
using Game.Components.Map.Walls;
using Game.Components.Player;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems {
    sealed class AIControlRun : IEcsRunSystem {
        readonly EcsWorld _world = null;

        private readonly EcsFilter<BodyComponent, PlayerComponent, MoveComponent, AIComponent> _botEntities =
            null;

        private readonly EcsFilter<PositionComponent, FinishComponent> _finishEntities =
            null;

        private Vector3 _force = new Vector3(0, 0, 0);

        public void Run() {
            foreach (int player in _botEntities) {
                Vector2 finishPosition = new Vector2();
                if (!_finishEntities.IsEmpty()) {
                    finishPosition = _finishEntities.Get1(0).Position;
                }

                BotControl(ref _botEntities.Get1(player), _botEntities.Get2(player),
                    ref _botEntities.Get3(player), finishPosition);
            }
        }

        private void BotControl(ref BodyComponent bodyComponent, PlayerComponent playerComponent,
            ref MoveComponent moveComponent, Vector2 finishPosition) {
            Vector2 vel = bodyComponent.Body.velocity;
            var velX = vel.x;
            var velY = vel.y;
            bool isMaxSpeedLeft = velX <= -playerComponent.Speed;
            bool isMaxSpeedRight = velX >= playerComponent.Speed;

            _force.x = 0;

            float botX = bodyComponent.Body.position.x;

            bool wereMovingToLeft = velX < 0; // were moving to the left

            bool goalFromTheLeft = botX > finishPosition.x; // goal is from the left
            bool toLeft = goalFromTheLeft;

            if (Mathf.Abs(velY) < 0.5f && Mathf.Abs(finishPosition.y - bodyComponent.Body.position.y) > 0.5f)
                toLeft = wereMovingToLeft;

            if (toLeft) {
                _force.x -= (isMaxSpeedLeft ? 0 : 1f);
            }
            else {
                _force.x += (isMaxSpeedRight ? 0 : 1f);
            }

            moveComponent.Force = _force * playerComponent.Power;
            moveComponent.Tourqe = Vector3.zero;
        }
    }
}