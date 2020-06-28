using Game.Components;
using Game.Systems;
using Leopotam.Ecs;
using UnityEngine;

namespace Game {
    sealed class Startup : MonoBehaviour {
        EcsWorld _world;
        EcsSystems _updateSystems;
        EcsSystems _fixedUpdateSystems;

        public Setup gameSetup;
        public SceneData sceneData;

        void Start() {
            _world = new EcsWorld();
            _fixedUpdateSystems = new EcsSystems(_world);
            _updateSystems = new EcsSystems(_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedUpdateSystems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_updateSystems);
#endif
            _fixedUpdateSystems
                //.Add(new PlayerInit()) // if I move it here, player entities are not filtered in update systems 
                .Add(new PlayerControlRun())
                .Add(new AIControlRun())
                .Add(new PlayerMoveRun())
                .Inject(gameSetup)
                .Init();

            _updateSystems
                .Add(new PlayerInit()) // but here it is okay for both update and fixedUpdate systems (why?)
                .Add(new LevelInit())
                .Add(new LevelRun())
                .Add(new LevelEndRun())
                .Add(new LevelRestartRun())
                .OneFrame<FinishedFrame>()
                .OneFrame<RestartFrame>()
                .Inject(gameSetup)
                .Inject(sceneData)
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
    }
}