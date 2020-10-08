using System;
using Game.Components;
using Game.Components.Player;
using Game.UI;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace Game.Systems {
    sealed class LevelEndRun : IEcsRunSystem {
        readonly EcsWorld _world = null;
        private readonly SceneData _sceneData = null;
        private readonly Setup _gameData = null;
        private readonly InputManager _inputManager = null;

        private readonly EcsFilter<PlayerComponent, BodyComponent, FinishedFrame> _finishedEnitites =
            null;
        private readonly EcsFilter<PlayerComponent> _playerEnitites =
            null;

        private readonly EcsFilter<LevelStateComponent> _levelEntities = null;

        public void Run() {
            foreach (int level in _levelEntities) {
                ref var levelState = ref _levelEntities.Get1(level).LevelState;
                if (levelState == LevelState.GameEnded) {
                    // update win/loose text
                    foreach (int finish in _finishedEnitites) {
                        var playerComponent = _finishedEnitites.Get1(finish);
                        bool win = _finishedEnitites.Get3(finish).Win;
                        Debug.Log($"Player {playerComponent.Id} {(win ? "win" : "loose")}");

                        _sceneData.finishText.GetComponent<TextMeshPro>().text =
                            $"Player {playerComponent.Id} {(win ? "win" : "loose")}"
                            ;//+ $"{Environment.NewLine}Press {_gameData.level.restartKey.ToString()} to restart";
                        _inputManager.RestartButton.gameObject.SetActive(true);
                    }

                    // update score text
                    if (!_finishedEnitites.IsEmpty()) {
                        string scoreText = "";
                        foreach (int player in _playerEnitites) {
                            var playerComponent = _playerEnitites.Get1(player);
                            scoreText += $"Player {playerComponent.Id}: {playerComponent.Score} {Environment.NewLine}";
                        }
                        _sceneData.scoreText.GetComponent<TextMeshPro>().text = scoreText;
                    }
                }
            }
        }
    }
}