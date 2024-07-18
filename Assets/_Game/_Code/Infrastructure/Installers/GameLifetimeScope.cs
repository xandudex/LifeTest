using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Xandudex.LifeGame
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        GameBoardSettings gameBoardSettings;

        [SerializeField]
        AnimalSpawnerConfig animalConfig;

        [SerializeField]
        FoodSpawnerConfig foodConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterBoardSettings(builder);
            RegisterAnimals(builder);
            RegisterFood(builder);
        }

        private void RegisterBoardSettings(IContainerBuilder builder)
        {
            builder.RegisterInstance(gameBoardSettings);
        }

        private void RegisterAnimals(IContainerBuilder builder)
        {
            builder.RegisterInstance(animalConfig);
            builder.RegisterEntryPoint<AnimalSpawner>();
        }

        private void RegisterFood(IContainerBuilder builder)
        {
            builder.RegisterInstance(foodConfig);
            builder.RegisterEntryPoint<FoodSpawner>();
        }
    }
}
