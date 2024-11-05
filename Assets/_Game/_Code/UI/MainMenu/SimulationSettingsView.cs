using UnityEngine;
using UnityEngine.UI;

namespace Life.UI.MainMenu
{
    internal class SimulationSettingsView : MonoBehaviour
    {
        [field: SerializeField]
        public ExtendedSlider SizeSlider { get; private set; }

        [field: SerializeField]
        public ExtendedSlider AnimalsAmountSlider { get; private set; }

        [field: SerializeField]
        public ExtendedSlider SpeedSlider { get; private set; }

        [field: SerializeField]
        public Button SimulateButton { get; private set; }

        [field: SerializeField]
        public Button LoadButton { get; private set; }
    }
}
