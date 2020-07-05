using Game.Systems;
using Leopotam.Ecs;
using UnityEngine;

namespace Game {
    public sealed class MenuStartup : MonoBehaviour {
        EcsWorld _world;
        EcsSystems _systems;
        public SceneData sceneData;

        void Start () {

            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            _systems
                .Add(new PlayerInit())
                .Add(new PlayerControlRun())
                .Add(new PlayerMoveRun())
                .Inject(sceneData)
                .Init ();
        }

        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}