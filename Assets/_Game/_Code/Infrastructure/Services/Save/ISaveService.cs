using System;

namespace Life.Services.Save
{
    internal interface ISaveService
    {
        event Func<ISaveData> Saving;
        event Action Saved;
        event Action Loading;
        event Action Loaded;

        void Save();
        void Load();
        bool TryLoad<T>(out T data) where T : ISaveData;
    }
}
