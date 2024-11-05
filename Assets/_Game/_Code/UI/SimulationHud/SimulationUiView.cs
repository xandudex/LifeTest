using UnityEngine;
using UnityEngine.UI;

namespace Life.UI.SimulationHud
{
    internal class SimulationUiView : MonoBehaviour
    {
        [field: SerializeField]
        public ExtendedSlider SpeedSlider { get; private set; }

        [field: SerializeField]
        public Button SaveButton { get; private set; }

        [field: SerializeField]
        public Button LeaveButton { get; private set; }
    }
}
