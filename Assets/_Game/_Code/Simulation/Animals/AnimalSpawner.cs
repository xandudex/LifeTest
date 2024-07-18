using MessagePipe;
using UnityEngine;
using VContainer.Unity;

namespace Xandudex.LifeGame
{
    internal class AnimalSpawner : IStartable
    {
        private readonly AnimalSpawnerConfig animalSpawnerConfig;
        private readonly GameBoardSettings settings;
        private readonly IGameResources gameResources;
        private readonly GameFactory gameFactory;
        private readonly IPublisher<AnimalSpawned> animalSpawnedPub;

        public AnimalSpawner(AnimalSpawnerConfig animalSpawnerConfig,
                             GameBoardSettings settings,
                             IGameResources gameResources,
                             GameFactory gameFactory,
                             IPublisher<AnimalSpawned> animalSpawnedPub)
        {
            this.animalSpawnerConfig = animalSpawnerConfig;
            this.settings = settings;
            this.gameResources = gameResources;
            this.gameFactory = gameFactory;
            this.animalSpawnedPub = animalSpawnedPub;
        }

        void IStartable.Start()
        {
            for (uint i = 0; i < settings.AnimalsCount; i++)
            {
                //todo: calculate position
                Vector2 positionOnCircle = Random.insideUnitCircle.normalized * 50;
                Vector3 position = new(positionOnCircle.x, 0, positionOnCircle.y);

                GameObject animalObject = gameFactory.Instantiate(gameResources.AnimalPrefab, position, animalSpawnerConfig.Parent);
                Animal animal = gameFactory.Create<Animal>(animalObject);

                animalSpawnedPub.Publish(new(animal));
            }
        }
    }
}
