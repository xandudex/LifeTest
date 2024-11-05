using Cysharp.Threading.Tasks;
using Life.Utilities;
using MessagePipe;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Life.Services.SceneLoading
{
    internal class SceneLoaderService : Service, ISceneLoaderService
    {
        AsyncOperation asyncOperation;
        LifetimeScope.ExtraInstallationScope extraInstallationScope;

        [Inject] readonly IPublisher<SceneLoaderServiceMessages.SceneLoading> sceneLoadingMessage;
        [Inject] readonly IPublisher<SceneLoaderServiceMessages.SceneLoadingFinished> sceneLoadingFinishedMessage;
        [Inject] readonly IPublisher<SceneLoaderServiceMessages.SceneLoaded> sceneLoadedMessage;

        public void Load(string name, bool manual = false)
        {
            LoadAsync(name).Forget();
        }

        public async void Load<T>(string name, T payload, bool manual = false)
        {

            using (extraInstallationScope = LifetimeScope.Enqueue(b => b.RegisterInstance(payload).As(typeof(T))))
            {
                await LoadAsync(name);
            }
        }

        async UniTask LoadAsync(string name, bool manual = false)
        {
            sceneLoadingMessage.Publish(new());

            asyncOperation = SceneManager.LoadSceneAsync(name);
            asyncOperation.allowSceneActivation = false;

            while (asyncOperation.progress < 0.9f)
                await Awaitable.NextFrameAsync();

            if (!manual)
                asyncOperation.allowSceneActivation = true;

            sceneLoadingFinishedMessage.Publish(new());

            while (!asyncOperation.isDone)
                await UniTask.Yield();

            sceneLoadedMessage.Publish(new());
        }

        public void FinishLoading()
        {
            if (asyncOperation == null)
                return;

            asyncOperation.allowSceneActivation = true;
        }
    }
}
