using MessagePipe;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
#nullable enable

namespace Xandudex.LifeGame
{
    internal class SceneLoader : ISceneLoader
    {
        AsyncOperation? asyncOperation;
        LifetimeScope.ExtraInstallationScope extraInstallationScope;

        private readonly IPublisher<SceneLoading> sceneLoadingPub;
        private readonly IPublisher<SceneLoadingFinished> sceneLoadingFinishedPub;
        private readonly IPublisher<SceneLoaded> sceneLoadedPub;

        public SceneLoader(IPublisher<SceneLoading> sceneLoadingPub,
                           IPublisher<SceneLoadingFinished> sceneLoadingFinishedPub,
                           IPublisher<SceneLoaded> sceneLoadedPub)
        {
            this.sceneLoadingPub = sceneLoadingPub;
            this.sceneLoadingFinishedPub = sceneLoadingFinishedPub;
            this.sceneLoadedPub = sceneLoadedPub;
        }

        public void Load(string name, bool manual = false)
        {
            _ = LoadAsync(name);
        }

        public async void Load<T>(string name, T payload, bool manual = false)
        {

            using (extraInstallationScope = LifetimeScope.Enqueue(b => b.RegisterInstance(payload).As(typeof(T))))
            {
                await LoadAsync(name);
            }
        }

        async Task LoadAsync(string name, bool manual = false)
        {
            sceneLoadingPub.Publish(new());

            asyncOperation = SceneManager.LoadSceneAsync(name);
            asyncOperation.allowSceneActivation = false;

            while (asyncOperation.progress < 0.9f)
                await Awaitable.NextFrameAsync();

            if (!manual)
                asyncOperation.allowSceneActivation = true;

            sceneLoadingFinishedPub.Publish(new());

            while (!asyncOperation.isDone)
                await Awaitable.NextFrameAsync();

            sceneLoadedPub.Publish(new());
        }

        public void FinishLoading()
        {
            if (asyncOperation == null)
                return;

            asyncOperation.allowSceneActivation = true;
        }
    }
}
