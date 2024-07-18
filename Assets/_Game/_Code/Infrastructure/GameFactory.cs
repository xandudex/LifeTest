using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Xandudex.LifeGame
{
    internal class GameFactory
    {
        private readonly LifetimeScope scope;

        public GameFactory(LifetimeScope scope)
        {
            this.scope = scope;
        }

        /*        public Food CreateFood(GameObject foodObject)
                {
                    var childScope =
                    scope.CreateChild(o =>
                    {
                        o.RegisterInstance(foodObject);
                        o.RegisterEntryPoint<Food>().AsSelf();
                    });

                    return childScope.Container.Resolve<Food>();
                }

                public Food CreateAnimal(GameObject foodObject)
                {
                    var childScope =
                    scope.CreateChild(o =>
                    {
                        o.RegisterInstance(foodObject);
                        o.RegisterEntryPoint<Food>().AsSelf();
                    });

                    return childScope.Container.Resolve<Food>();
                }*/

        public T Create<T>(params object[] args)
        {
            var childScope =
                scope.CreateChild(o =>
                {
                    foreach (object arg in args)
                        o.RegisterInstance(arg).As(arg.GetType());

                    o.RegisterEntryPoint<T>().AsSelf();
                });

            return childScope.Container.Resolve<T>();
        }

        internal GameObject Instantiate(GameObject prefab, Vector3 position, Transform parent)
        {
            return scope.Container.Instantiate(prefab, position, Quaternion.identity, parent);
        }
    }
}
