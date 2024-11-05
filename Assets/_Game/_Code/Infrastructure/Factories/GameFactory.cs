using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Life.Factories
{
    internal class GameFactory
    {
        private readonly LifetimeScope scope;

        public GameFactory(LifetimeScope scope)
        {
            this.scope = scope;
        }

        public T Create<T>(params object[] args)
        {
            return scope.Container.CreateScope(o =>
            {
                foreach (object arg in args)
                    o.RegisterInstance(arg).As(arg.GetType());

                o.RegisterEntryPoint<T>().AsSelf();
            }).Resolve<T>();
        }

        internal GameObject Instantiate(GameObject prefab) =>
            scope.Container.Instantiate(prefab);

        internal GameObject Instantiate(GameObject prefab, Transform parent) =>
            Instantiate(prefab, Vector3.zero, parent);

        internal GameObject Instantiate(GameObject prefab, Vector3 position, Transform parent)
        {
            return scope.Container.Instantiate(prefab, position, Quaternion.identity, parent);
        }
    }
}
