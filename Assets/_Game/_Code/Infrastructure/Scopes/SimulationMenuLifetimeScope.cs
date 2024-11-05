using Life.Data.Simulation;
using Life.UI.MainMenu;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Life.Scopes
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
