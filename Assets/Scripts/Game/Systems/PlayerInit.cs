using Game.Components;
using Game.Components.Player;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Systems {
    sealed class PlayerInit : IEcsInitSystem {
        readonly EcsWorld _world = null;
        readonly SceneData _sceneData = null;

        public void Init() {
            int id = 0;
            var cameraFollow = Camera.main.transform.GetComponent<CameraFollow>();
            foreach (var player in _sceneData.players) {
                //var playerObject = GameObject.Instantiate(player.playerPrefab);
                var playerObject = player;
                player.gameObject.SetActive(true);
                playerObject.transform.position = new Vector3(_sceneData.players[id].spawnPosition.x,
                    _sceneData.players[id].spawnPosition.y, 0);
                cameraFollow.players.Add(playerObject.transform);

                EcsEntity playerEntity = _world.NewEntity();
                playerEntity.Get<PlayerComponent>().Id = id;
                playerEntity.Get<PlayerComponent>().MoveLeftKey = _sceneData.players[id].moveLeftKey;
                playerEntity.Get<PlayerComponent>().MoveRightKey = _sceneData.players[id].moveRightKey;
                playerEntity.Get<PlayerComponent>().Speed = _sceneData.players[id].speed;
                playerEntity.Get<PlayerComponent>().Power = _sceneData.players[id].power;
                playerEntity.Get<PlayerComponent>().Score = 0;
                playerEntity.Get<ObjectComponent>().Transform = playerObject.transform;
                playerEntity.Get<MoveComponent>().Force = new Vector3();
                playerEntity.Get<MoveComponent>().Tourqe = new Vector3();
                playerEntity.Get<BodyComponent>().Body = playerObject.GetComponent<Rigidbody>();

                if (_sceneData.players[id].bot) playerEntity.Get<AIComponent>();
                id++; // too bad !
            }
        }
    }
}