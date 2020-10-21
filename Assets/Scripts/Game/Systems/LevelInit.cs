using Game.Components;
using Leopotam.Ecs;
using System.Collections.Generic;
using Game.Components.Map.Walls;
using UnityEngine;

namespace Game.Systems {
    sealed class LevelInit : IEcsInitSystem {
        readonly EcsWorld _world = null;
        readonly Setup _gameData = null;

        public void Init() {
            // create level entity
            var levelEntity = _world.NewEntity();
            levelEntity.Get<LevelStateComponent>().LevelState = LevelState.GameInProgress;
            
            // create walls
            var levelSetup = _gameData.level;
            var wallPositions = GenerateLevelWalls(levelSetup.sizeHorizontal, levelSetup.sizeVertical,
                levelSetup.wallDensity,
                levelSetup.wallLengthAverage, levelSetup.wallLengthDelta);

            var levelObject = new GameObject("Level");
            for (int i = 0; i < wallPositions.Count; ++i) {
                var instance = GameObject.Instantiate(levelSetup.wallPrefab, levelObject.transform);
                instance.transform.position = new Vector3(wallPositions[i].x, wallPositions[i].y, 0);
            }

            // create finish entities
            for (int i = 0; i < levelSetup.finishCount; ++i) {
                int finishIdx = 0;
                while (finishIdx == 0
                       || wallPositions[finishIdx].x < -levelSetup.sizeHorizontal * 0.45f
                       || wallPositions[finishIdx].x > levelSetup.sizeHorizontal * 0.45f) {
                    finishIdx = Random.Range((int) (wallPositions.Count * 0.75), wallPositions.Count);
                }

                Vector2 deltaUp = new Vector2(0, 1);
                var finishEntity = _world.NewEntity();
                finishEntity.Get<PositionComponent>().Position = wallPositions[finishIdx] + deltaUp;
                finishEntity.Get<FinishComponent>();

                var instance = GameObject.Instantiate(levelSetup.finishPrefab, levelObject.transform);
                instance.transform.position = wallPositions[finishIdx] + deltaUp;
            }
        }

        List<Vector2> GenerateLevelWalls(int sizeX, int sizeY, float wallDensity, int wallLen, int wallLenDelta) {
            var result = new List<Vector2>();

            // todo: more flexible solution
            for (int i = 0; i < sizeY; i += 3) {
                int y = -i - 5;
                for (int j = 0; j < sizeX; ++j) {
                    int x = j - sizeX / 2;
                    float push = Random.Range(0, 1f);
                    if (push < wallDensity) {
                        int lenDelta = Random.Range(0, wallLenDelta);
                        int len = wallLen - lenDelta;
                        result.Add(new Vector2(x, y));
                        for (int k = 1; k < len; ++k) {
                            if (x + k < sizeX / 2) result.Add(new Vector2(x + k, y));
                            if (x - k > -sizeX / 2) result.Add(new Vector2(x - k, y));
                        }

                        j += 2 * len;
                    }
                }

                result.Add(new Vector2(sizeX * 0.5f, y));
                result.Add(new Vector2(sizeX * 0.5f, y + 1));
                result.Add(new Vector2(sizeX * 0.5f, y + 2));
                result.Add(new Vector2(-sizeX * 0.5f, y));
                result.Add(new Vector2(-sizeX * 0.5f, y + 1));
                result.Add(new Vector2(-sizeX * 0.5f, y + 2));
            }

            return result;
        }
    }
}