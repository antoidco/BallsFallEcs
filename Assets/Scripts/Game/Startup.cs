using System;
using Game.Components;
using Game.Components.Pool;
using Game.Systems;
using Game.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Game {
    sealed class Startup : MonoBehaviour {
        EcsWorld _world;
        EcsSystems _updateSystems;
        EcsSystems _fixedUpdateSystems;

        public Setup gameSetup;
        public SceneData sceneData;
        public InputManager inputManager;
        private GameData _gameData;
        
        void Start() {
            _gameData = FindObjectOfType<GameData>() ?? throw new Exception("No GameData in Scene");
            SelectGameType();

            _world = new EcsWorld();
            _fixedUpdateSystems = new EcsSystems(_world);
            _updateSystems = new EcsSystems(_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedUpdateSystems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_updateSystems);
#endif
            _fixedUpdateSystems
                .Add(new PlayerMoveRun())
                .Inject(gameSetup)
                .Inject(sceneData)
                .Init();

            _updateSystems
                .Add(new PlayerInit())
                .Add(new PoolInit())
                .Add(new ShootMachinesInit())
                .Add(new LevelInit())
                .Add(new PlayerControlRun())
                .Add(new AIControlRun())
                .Add(new LevelRun())
                .Add(new LevelEndRun())
                .Add(new LevelRestartRun())
                .Add(new ShootMachinesRun())
                .Add(new PoolSpawnRun())
                .OneFrame<FinishedFrame>()
                .OneFrame<RestartFrame>()
                .OneFrame<PoolSpawnFrame>()
                .Inject(gameSetup)
                .Inject(sceneData)
                .Inject(inputManager)
                .Init();
        }

        void Update() {
            _updateSystems?.Run();
        }

        void FixedUpdate() {
            _fixedUpdateSystems?.Run();
        }

        void OnDestroy() {
            if (_fixedUpdateSystems != null) {
                _fixedUpdateSystems.Destroy();
                _fixedUpdateSystems = null;
            }

            if (_updateSystems != null) {
                _updateSystems.Destroy();
                _updateSystems = null;
            }

            _world?.Destroy();
            _world = null;
        }
        
        private void SelectGameType() { 
            sceneData.players[1].bot = _gameData.GameWithBot;    
        }
    }
}