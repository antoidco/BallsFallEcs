using Game.Components;
using Game.Components.Player;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems {
    sealed class PlayerInit : IEcsInitSystem {
        readonly EcsWorld _world = null;
        readonly Setup _gameData = null;

        public void Init() {
            int id = 0;
            var cameraFollow = Camera.main.transform.GetComponent<CameraFollow>();
            foreach (var player in _gameData.players) {
                var playerObject = GameObject.Instantiate(player.playerPrefab);
                playerObject.transform.position = new Vector3(_gameData.players[id].spawnPosition.x,
                    _gameData.players[id].spawnPosition.y, 0);
                cameraFollow.players.Add(playerObject.transform);

                EcsEntity playerEntity = _world.NewEntity();
                playerEntity.Get<PlayerComponent>().Id = id;
                playerEntity.Get<PlayerComponent>().MoveLeftKey = _gameData.players[id].moveLeftKey;
                playerEntity.Get<PlayerComponent>().MoveRightKey = _gameData.players[id].moveRightKey;
                playerEntity.Get<PlayerComponent>().Speed = _gameData.players[id].speed;
                playerEntity.Get<PlayerComponent>().Power = _gameData.players[id].power;
                playerEntity.Get<PlayerComponent>().Score = 0;
                playerEntity.Get<ObjectComponent>().Transform = playerObject.transform;
                playerEntity.Get<MoveComponent>().Force = new Vector3();
                playerEntity.Get<MoveComponent>().Tourqe = new Vector3();
                playerEntity.Get<BodyComponent>().Body = playerObject.GetComponent<Rigidbody>();

                if (_gameData.players[id].bot) playerEntity.Get<AIComponent>();
                id++; // too bad !
            }
        }
    }
}