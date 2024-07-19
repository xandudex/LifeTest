namespace Xandudex.LifeGame
{
    internal record SceneLoading();
    internal record SceneLoadingFinished();
    internal record SceneLoaded();
    internal interface ISceneLoader
    {
        void Load(string name, bool manual = false);
        void Load<T>(string name, T payload, bool manual = false);
        void FinishLoading();
    }
}
