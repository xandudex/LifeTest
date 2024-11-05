using Life.Systems.Simulation;
using R3;

namespace Life.UI.MainMenu
{
    internal class SimulationSettingsModel
    {
        public readonly ReactiveProperty<int> Size = new();
        public readonly ReactiveProperty<int> Animals = new();
        public readonly ReactiveProperty<float> Speed = new();

        public SimulationSettings AsSimulationSettings()
        {
            return new(Size.Value, Animals.Value, Speed.Value);
        }
    }
}
