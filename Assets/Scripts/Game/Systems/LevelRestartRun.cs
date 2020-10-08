using Game.Components;
using Game.Components.Player;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace Game.Systems {
    public class LevelRestartRun : IEcsRunSystem {
        readonly EcsWorld _world = null;
        private readonly SceneData _sceneData = null;
        private readonly EcsFilter<LevelStateComponent, RestartFrame> _levelEntities = null;
        private readonly EcsFilter<BodyComponent, PlayerComponent> _playerEntities = null;
        
        public void Run() {
            foreach (int level in _levelEntities) {
                ref var levelState = ref _levelEntities.Get1(level).LevelState;
                if (levelState == LevelState.Restarting) {
                    // clear finish message
                    _sceneData.finishText.GetComponent<TextMeshProUGUI>().text = "";
                    // move player entities to their spawn positions
                    foreach (int player in _playerEntities) {
                        var playerComponent = _playerEntities.Get2(player);
                        ref var bodyComponent = ref _playerEntities.Get1(player);
                        
                        bodyComponent.Body.position = _sceneData.players[playerComponent.Id].spawnPosition;
                        bodyComponent.Body.velocity = new Vector3(0, 0, 0);
                    }

                    // change level state
                    levelState = LevelState.GameInProgress;
                }
            }
        }
    }
}