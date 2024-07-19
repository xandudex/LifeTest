using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Xandudex.LifeGame
{
    internal class ProjectLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterMessagePipe(builder);
            RegisterGameResources(builder);
            RegisterGameFactory(builder);
            RegisterSaveService(builder);
            RegisterSceneLoader(builder);
        }

        private void RegisterMessagePipe(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe();
            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
        }

        private void RegisterGameResources(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<GameResources>();

        private void RegisterGameFactory(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<GameFactory>().AsSelf();

        private void RegisterSaveService(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<SaveService>();

        private void RegisterSceneLoader(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<SceneLoader>();
    }
}