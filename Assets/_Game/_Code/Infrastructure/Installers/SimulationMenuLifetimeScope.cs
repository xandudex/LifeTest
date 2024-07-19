using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Xandudex.LifeGame
{
    public class SimulationMenuLifetimeScope : LifetimeScope
    {
        [SerializeField]
        SimulationConfig simulationConfig;

        [SerializeField]
        SimulationSettingsView simulationSettingsView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(simulationConfig);
            builder.RegisterInstance(simulationSettingsView);
            builder.Register<SimulationSettingsModel>(Lifetime.Singleton);
            builder.RegisterEntryPoint<SimulationSettingsPresenter>();
        }
    }
}
