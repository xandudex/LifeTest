namespace Life.Services.SceneLoading
{

    internal interface ISceneLoaderService
    {
        void Load(string name, bool manual = false);
        void Load<T>(string name, T payload, bool manual = false);
        void FinishLoading();
    }

    public static class SceneLoaderServiceMessages
    {
        internal record SceneLoading();
        internal record SceneLoadingFinished();
        internal record SceneLoaded();
    }
}
