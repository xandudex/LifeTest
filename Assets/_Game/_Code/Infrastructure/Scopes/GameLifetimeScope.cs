using Life.Data.Effects;
using Life.Factories;
using Life.Services.Effects;
using Life.Systems.Simulation;
using Life.UI.SimulationHud;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Life.Scopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        Camera camera;

        [SerializeField]
        SimulationUiView simulationSpeedView;

        [SerializeField]
        GameBoardView gameBoardView;

        [SerializeField]
        GameBoardConfig gameBoardConfig;

        [SerializeField]
        AnimalSpawnerConfig animalConfig;

        [SerializeField]
        FoodSpawnerConfig foodConfig;

        [SerializeField]
        EffectsConfig effectsConfig;
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterCancellationToken(builder);
            RegisterCamera(builder);
            RegisterGameFactory(builder);
            RegisterGameBoard(builder);
            RegisterAnimals(builder);
            RegisterFood(builder);
            RegisterSpeedControl(builder);
            RegisterEffects(builder);
        }

        private void RegisterEffects(IContainerBuilder builder)
        {
            builder.RegisterInstance(effectsConfig);
            builder.RegisterEntryPoint<EffectsService>();
        }

        private void RegisterCancellationToken(IContainerBuilder builder)
        {
            builder.RegisterInstance(destroyCancellationToken);
            builder.RegisterEntryPoint<CameraMovement>();
        }

        private void RegisterCamera(IContainerBuilder builder)
        {
            builder.RegisterInstance(camera);
        }

        private void RegisterGameFactory(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<GameFactory>().AsSelf();
        private void RegisterGameBoard(IContainerBuilder builder)
        {
            builder.RegisterInstance(gameBoardConfig);
            builder.RegisterInstance(gameBoardView);
            builder.RegisterEntryPoint<GameBoard>().AsSelf();
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

        private void RegisterSpeedControl(IContainerBuilder builder)
        {
            builder.RegisterInstance(simulationSpeedView);
            builder.RegisterEntryPoint<SimulationUiPresenter>().AsSelf();
        }
    }
}
