using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Life.UI
{
    internal class ExtendedSlider : MonoBehaviour
    {
        public event Action<float> ValueChanged;
        public event Action<float> MinValueChanged;
        public event Action<float> MaxValueChanged;

        [field: SerializeField]
        public Slider Slider { get; private set; }

        [field: SerializeField]
        public TMP_Text MinText { get; private set; }

        [field: SerializeField]
        public TMP_Text MaxText { get; private set; }

        [field: SerializeField]
        public TMP_Text ValueText { get; private set; }

        private void Awake()
        {
            Slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            Slider.onValueChanged.RemoveAllListeners();
        }

        public float Value
        {
            get => Slider.value;
            set
            {
                Slider.value = value;
            }
        }

        public float MinValue
        {
            get => Slider.minValue;
            set
            {
                Slider.minValue = value;
                MinText.text = Slider.minValue.ToString(".##");
                MinValueChanged?.Invoke(Slider.minValue);
            }
        }

        public float MaxValue
        {
            get => Slider.maxValue;
            set
            {
                Slider.maxValue = value;
                MaxText.text = Slider.maxValue.ToString(".##");
                MaxValueChanged?.Invoke(Slider.maxValue);
            }
        }

        private void OnValueChanged(float x)
        {
            ValueText.text = x.ToString(".##");
            ValueChanged?.Invoke(x);
        }
    }
}
