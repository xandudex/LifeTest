using MessagePipe;
using System;
using UnityEngine;
using VContainer.Unity;

namespace Xandudex.LifeGame
{
    internal class FoodSpawner : IInitializable, IDisposable
    {
        IDisposable disposable;

        private readonly FoodSpawnerConfig config;
        private readonly IGameResources gameResources;
        private readonly GameFactory gameFactory;
        private readonly ISubscriber<AnimalStateChanged> animalStateChangedSub;
        private readonly IPublisher<FoodSpawned> foodSpawnedPub;
        public FoodSpawner(FoodSpawnerConfig config,
                           IGameResources gameResources,
                           GameFactory gameFactory,
                           ISubscriber<AnimalStateChanged> animalStateChangedSub,
                           IPublisher<FoodSpawned> foodSpawnedPub)
        {
            this.config = config;
            this.gameResources = gameResources;
            this.gameFactory = gameFactory;
            this.animalStateChangedSub = animalStateChangedSub;
            this.foodSpawnedPub = foodSpawnedPub;
        }

        void IInitializable.Initialize()
        {
            DisposableBagBuilder builder = DisposableBag.CreateBuilder();

            animalStateChangedSub.Subscribe(
                x => SpawnFor(x.Animal.CurrentState as Animal.SearchFoodAnimalState),
                x => x.Animal.CurrentState is Animal.SearchFoodAnimalState)
            .AddTo(builder);

            disposable = builder.Build();
        }

        private void SpawnFor(Animal.SearchFoodAnimalState animalState)
        {
            //todo: calculate position
            Vector2 positionOnCircle = UnityEngine.Random.insideUnitCircle * 50;
            Vector3 position = new(positionOnCircle.x, 0, positionOnCircle.y);

            GameObject foodObject = gameFactory.Instantiate(gameResources.FoodPrefab, position, config.Parent);
            Food food = gameFactory.Create<Food>(foodObject);

            foodSpawnedPub.Publish(new(food, animalState.Animal));

        }

        void IDisposable.Dispose()
        {
            disposable?.Dispose();
        }


    }
}