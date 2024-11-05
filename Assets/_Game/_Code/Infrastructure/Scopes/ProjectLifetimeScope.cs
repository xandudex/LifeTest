using Life.Data.GameResources;
using Life.Factories;
using Life.Services.Save;
using Life.Services.SceneLoading;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Life.Scopes
{
    public class ProjectLifetimeScope : LifetimeScope
    {
        [SerializeField]
        GameResourcesConfig gameResourcesConfig;
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterMessagePipe(builder);

            RegisterGameFactory(builder);
            RegisterSceneLoader(builder);

            RegisterGameResources(builder);

            RegisterSaveService(builder);
        }

        void RegisterMessagePipe(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe();
            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
        }

        void RegisterGameResources(IContainerBuilder builder) =>
            builder.RegisterInstance(gameResourcesConfig);

        void RegisterGameFactory(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<GameFactory>().AsSelf();

        void RegisterSaveService(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<SaveService>();

        void RegisterSceneLoader(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<SceneLoaderService>();
    }
}