using System;
using System.Collections.Generic;

namespace Xandudex.LifeGame
{
    internal class SaveService : ISaveService
    {
        public event Func<ISaveData> Saving;
        public event Action Saved;
        public event Action Loading;
        public event Action Loaded;

        Dictionary<Type, ISaveData> SavedData = new();

        public SaveService()
        {
        }

        public void Save()
        {
            SavedData = new();
            var list = (Func<ISaveData>[])Saving.GetInvocationList();

            for (int i = 0; i < list.Length; i++)
            {
                ISaveData data = list[i]();
                SavedData.TryAdd(data.GetType(), data);
            }

            SaveToFile();

            Saved?.Invoke();
        }
        public void Load()
        {
            LoadFromFile();
            Loading?.Invoke();
            Loaded?.Invoke();
        }

        public bool TryLoad<T>(out ISaveData data) where T : ISaveData
        {
            Type type = typeof(T);
            return SavedData.TryGetValue(type, out data);
        }

        void SaveToFile()
        {

        }

        void LoadFromFile()
        {

        }
    }
}
