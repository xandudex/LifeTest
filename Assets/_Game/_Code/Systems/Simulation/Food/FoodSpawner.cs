using Life.Data.GameResources;
using Life.Factories;
using MessagePipe;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;
using VContainer.Unity;

namespace Life.Systems.Simulation
{
    internal record FoodSpawned(Food Food, Animal Animal);

    internal class FoodSpawner : IInitializable, IDisposable
    {
        Dictionary<Food, Vector2Int> occupiedPositions = new();
        ObjectPool<Food> objectPool;
        CompositeDisposable disposable = new();

        Dictionary<Animal, AnimalSpawner.AnimalSaveData> preloadedData = new();

        private readonly SimulationSettings settings;
        private readonly FoodSpawnerConfig config;
        private readonly GameResourcesConfig gameResourcesConfig;
        private readonly GameFactory gameFactory;
        private readonly ISubscriber<AnimalStateChanged> animalStateChangedSub;
        private readonly ISubscriber<AnimalSpawnedFromSave> animalSpawnedFromSaveSub;
        private readonly IPublisher<FoodSpawned> foodSpawnedPub;
        private readonly CancellationToken token;

        public FoodSpawner(SimulationSettings settings,
                           FoodSpawnerConfig config,
                           GameResourcesConfig gameResourcesConfig,
                           GameFactory gameFactory,
                           CancellationToken token,
                           ISubscriber<AnimalStateChanged> animalStateChangedSub,
                           ISubscriber<AnimalSpawnedFromSave> animalSpawnedFromSaveSub,
                           IPublisher<FoodSpawned> foodSpawnedPub)
        {
            this.settings = settings;
            this.config = config;
            this.gameResourcesConfig = gameResourcesConfig;
            this.gameFactory = gameFactory;
            this.animalStateChangedSub = animalStateChangedSub;
            this.animalSpawnedFromSaveSub = animalSpawnedFromSaveSub;
            this.foodSpawnedPub = foodSpawnedPub;
            this.token = token;
        }

        void IInitializable.Initialize()
        {
            objectPool = new ObjectPool<Food>(
                createFunc: () =>
                {
                    GameObject foodObject = gameFactory.Instantiate(gameResourcesConfig.FoodPrefab, config.Parent);
                    return gameFactory.Create<Food>(foodObject, objectPool);
                },
                actionOnRelease: x => occupiedPositions.Remove(x)
            );

            animalSpawnedFromSaveSub
                .Subscribe(x => preloadedData.Add(x.Animal, x.Data))
                .AddTo(disposable);

            animalStateChangedSub.Subscribe(
                x => SpawnFor(x.Animal.CurrentState as Animal.SearchFoodAnimalState),
                x => x.Animal.CurrentState is Animal.SearchFoodAnimalState)
            .AddTo(disposable);
        }

        private async void SpawnFor(Animal.SearchFoodAnimalState animalState)
        {
            Rect worldBorders = settings.WorldBorders;
            Vector3 position;

            if (preloadedData.TryGetValue(animalState.Animal, out var Data))
            {
                position = Data.FoodPosition;
                preloadedData.Remove(animalState.Animal);
            }
            else
            {
                Vector3 animalPosition = animalState.Animal.Position;

                //generating position around target animal, and doing litle edge-check
                Vector2 positionOnCircle = UnityEngine.Random.insideUnitCircle * settings.AnimalsSpeed * config.MaxSecondsToTraverse;

                if (Mathf.Abs(animalPosition.x + positionOnCircle.x) > worldBorders.xMax)
                    positionOnCircle.x = -positionOnCircle.x;

                if (Mathf.Abs(animalPosition.z + positionOnCircle.y) > worldBorders.yMax)
                    positionOnCircle.y = -positionOnCircle.y;

                //creating actual position around animal
                position = new()
                {
                    x = animalPosition.x + positionOnCircle.x,
                    z = animalPosition.z + positionOnCircle.y
                };
            }

            //sticking position to integer cell of the world
            Vector2Int cellPosition = new Vector2Int((int)position.x, (int)position.z);

            //getting the emptiest cell around targeting cell
            cellPosition = await GetNearestEmpty(cellPosition);

            //and positioning on the center of the world cell
            position = new()
            {
                x = cellPosition.x + Mathf.Sign(cellPosition.x) * 0.5f,
                z = cellPosition.y + Mathf.Sign(cellPosition.y) * 0.5f
            };

            //clamp position
            position = new()
            {
                x = Mathf.Clamp(position.x, worldBorders.xMin, worldBorders.xMax),
                z = Mathf.Clamp(position.z, worldBorders.yMin, worldBorders.yMax)
            };

            Food food = objectPool.Get();
            food.Position = position;
            food.Activate();
            occupiedPositions.Add(food, cellPosition);

            foodSpawnedPub.Publish(new(food, animalState.Animal));

        }


        async Awaitable<Vector2Int> GetNearestEmpty(Vector2Int rootCell)
        {
            Vector2Int cell = rootCell;

            while (occupiedPositions.Any(x => x.Value == cell))
            {
                await Awaitable.NextFrameAsync(token);

                cell = rootCell + new Vector2Int()
                {
                    x = Mathf.RoundToInt(UnityEngine.Random.value * 2 - 1f),
                    y = Mathf.RoundToInt(UnityEngine.Random.value * 2 - 1f),
                };
            }

            return cell;
        }

        void IDisposable.Dispose()
        {
            disposable?.Dispose();
        }
    }
}