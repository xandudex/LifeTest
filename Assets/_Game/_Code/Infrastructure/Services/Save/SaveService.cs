using System;
using System.Collections.Generic;
using System.Linq;

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
            var list = Saving.GetInvocationList().Cast<Func<ISaveData>>().ToArray();

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

        public bool TryLoad<T>(out T data) where T : ISaveData
        {
            Type type = typeof(T);

            if (!SavedData.TryGetValue(type, out ISaveData stored))
            {
                data = default;
                return false;
            }

            data = (T)stored;
            return true;
        }

        void SaveToFile()
        {

        }

        void LoadFromFile()
        {

        }
    }
}
