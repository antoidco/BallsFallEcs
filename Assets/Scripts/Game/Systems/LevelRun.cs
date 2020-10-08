using Game.Components;
using Game.Components.Map.Walls;
using Game.Components.Player;
using Game.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems {
    sealed class LevelRun : IEcsRunSystem {
        readonly EcsWorld _world = null;
        private readonly Setup _gameData = null;
        private readonly InputManager _inputManager = null;
        private readonly EcsFilter<BodyComponent, PlayerComponent> _playerEntities = null;
        private readonly EcsFilter<PositionComponent, FinishComponent> _finishEntities = null;
        private readonly EcsFilter<LevelStateComponent> _levelEntities = null;

        private float _finishEpsilon = 1f;

        public void Run() {
            foreach (int level in _levelEntities) { // this is strange: level should be singleton?
                ref var levelState = ref _levelEntities.Get1(level).LevelState;
                // if game is in progress, check for finish event create
                if (levelState == LevelState.GameInProgress) {
                    foreach (int player in _playerEntities) {
                        ref var bodyComponent = ref _playerEntities.Get1(player);
                        // player win
                        foreach (int finishEntity in _finishEntities) {
                            if ((bodyComponent.Body.position - (Vector3)_finishEntities.Get1(finishEntity).Position).magnitude <
                                _finishEpsilon) {
                                ref var finishedPlayer = ref _playerEntities.GetEntity(player);
                                finishedPlayer.Get<FinishedFrame>().Win = true;
                                finishedPlayer.Get<PlayerComponent>().Score++;
                                levelState = LevelState.GameEnded;
                            }
                        }

                        // player loose
                        if (bodyComponent.Body.position.y < -_gameData.level.sizeVertical * 1.25f) {
                            ref var finishedPlayer = ref _playerEntities.GetEntity(player);
                            finishedPlayer.Get<FinishedFrame>().Win = false;
                            finishedPlayer.Get<PlayerComponent>().Score--;
                            levelState = LevelState.GameEnded;
                        }
                    }
                } // if game is ended, check for restart event create
                else if (levelState == LevelState.GameEnded) {
                    if (Input.GetKey(_gameData.level.restartKey)
                    || _inputManager.RestartButton.Pressed) {
                        _inputManager.RestartButton.Pressed = false;
                        _inputManager.RestartButton.gameObject.SetActive(false);
                        levelState = LevelState.Restarting;
                        ref var levelEntity = ref _levelEntities.GetEntity(level);
                        levelEntity.Get<RestartFrame>();
                    }
                }
            }
        }
    }
}