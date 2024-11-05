using Life.Data.GameResources;
using Life.Factories;
using Life.Services.Save;
using MessagePipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Life.Systems.Simulation
{
    internal record AnimalSpawned(Animal Animal);
    internal record AnimalSpawnedFromSave(Animal Animal, AnimalSpawner.AnimalSaveData Data);
    internal class AnimalSpawner : IStartable, IDisposable
    {
        List<Animal> Animals = new();

        private readonly AnimalSpawnerConfig animalSpawnerConfig;
        private readonly SimulationSettings settings;
        private readonly GameResourcesConfig gameResourcesConfig;
        private readonly GameFactory gameFactory;
        private readonly CancellationToken token;
        private readonly ISaveService saveService;
        private readonly IPublisher<AnimalSpawned> animalSpawnedPub;
        private readonly IPublisher<AnimalSpawnedFromSave> animalSpawnedFromSavePub;

        public AnimalSpawner(AnimalSpawnerConfig animalSpawnerConfig,
                             SimulationSettings settings,
                             GameResourcesConfig gameResourcesConfig,
                             GameFactory gameFactory,
                             CancellationToken token,
                             ISaveService saveService,
                             IPublisher<AnimalSpawned> animalSpawnedPub,
                             IPublisher<AnimalSpawnedFromSave> animalSpawnedFromSavePub)
        {
            this.animalSpawnerConfig = animalSpawnerConfig;
            this.settings = settings;
            this.gameResourcesConfig = gameResourcesConfig;
            this.gameFactory = gameFactory;
            this.animalSpawnedPub = animalSpawnedPub;
            this.animalSpawnedFromSavePub = animalSpawnedFromSavePub;
            this.token = token;
            this.saveService = saveService;

            saveService.Saving += OnSaving;
        }

        async void IStartable.Start()
        {
            GameObject animalObject;
            Animal animal;
            Rect worldBorders = settings.WorldBorders;
            AnimalSpawnerSaveData saveData;
            bool preloaded = saveService.TryLoad(out saveData);

            for (int i = 0; i < settings.AnimalsCount; i++)
            {
                Vector3 position = preloaded && i < saveData.Animals.Count ? saveData.Animals[i].AnimalPosition : new()
                {
                    x = Random.Range(worldBorders.xMin, worldBorders.xMax),
                    z = Random.Range(worldBorders.yMin, worldBorders.yMax)
                };

                AnimalSaveData animalData = null;

                if (preloaded && i < saveData.Animals.Count)
                    animalData = saveData.Animals[i];

                animalObject = gameFactory.Instantiate(gameResourcesConfig.AnimalPrefab, position, animalSpawnerConfig.Parent);
                animal = gameFactory.Create<Animal>(animalObject);
                Animals.Add(animal);

                if (preloaded && animalData != null)
                    animalSpawnedFromSavePub.Publish(new(animal, animalData));

                animalSpawnedPub.Publish(new(animal));
                await Awaitable.WaitForSecondsAsync(0.2f, token);
            }
        }


        private ISaveData OnSaving()
        {
            return new AnimalSpawnerSaveData
            {
                Animals = Animals.Select(x => new AnimalSaveData
                {
                    AnimalPosition = x.Position,
                    FoodPosition = x.Food != null ? x.Position : Vector3.negativeInfinity,
                }).ToList()
            };
        }

        void IDisposable.Dispose()
        {
            saveService.Saving -= OnSaving;
        }

        internal class AnimalSpawnerSaveData : ISaveData
        {
            public List<AnimalSaveData> Animals;

        }

        [System.Serializable]
        internal class AnimalSaveData
        {
            public Vector3 AnimalPosition;
            public Vector3 FoodPosition;
        }
    }
}
