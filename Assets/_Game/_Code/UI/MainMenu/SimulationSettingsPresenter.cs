using Life.Data.Simulation;
using Life.Services.Save;
using Life.Services.SceneLoading;
using Life.Systems.Simulation;
using System;
using UnityEngine;
using VContainer.Unity;

namespace Life.UI.MainMenu
{
    internal class SimulationSettingsPresenter : IStartable, IDisposable
    {
        private const string SimulationScene = "Game Scene";
        SimulationSettings preloadedSettings;

        private readonly SimulationConfig config;
        private readonly SimulationSettingsModel model;
        private readonly SimulationSettingsView view;
        private readonly ISceneLoaderService sceneLoader;
        private readonly ISaveService saveService;

        public SimulationSettingsPresenter(SimulationConfig config,
                                           SimulationSettingsModel model,
                                           SimulationSettingsView view,
                                           ISceneLoaderService sceneLoader,
                                           ISaveService saveService)
        {
            this.config = config;
            this.model = model;
            this.view = view;
            this.sceneLoader = sceneLoader;
            this.saveService = saveService;
        }

        void IStartable.Start()
        {
            saveService.TryLoad(out preloadedSettings);

            view.SizeSlider.ValueChanged += OnSizeValueChanged;
            view.AnimalsAmountSlider.ValueChanged += OnAnimalsValueChanged;
            view.SpeedSlider.ValueChanged += OnSpeedValueChanged;
            view.SimulateButton.onClick.AddListener(() => sceneLoader.Load(SimulationScene, model.AsSimulationSettings()));

            bool hasPreload = preloadedSettings != null;
            view.LoadButton.interactable = hasPreload;
            if (hasPreload)
            {
                view.LoadButton.onClick.AddListener(() =>
                {
                    sceneLoader.Load(SimulationScene, preloadedSettings);
                });
            }

            ApplyConfig();
        }

        private void OnSpeedValueChanged(float x)
        {
            model.Speed.Value = x;
        }

        private void OnAnimalsValueChanged(float x)
        {
            model.Animals.Value = (int)x;
        }

        private void OnSizeValueChanged(float x)
        {
            model.Size.Value = (int)x;
            UpdateAnimalsAmount();
        }

        void UpdateAnimalsAmount()
        {
            int maxAnimals = (int)(model.Size.Value * model.Size.Value / 2f);
            int currentAnimals = Mathf.Clamp(model.Animals.Value, config.AnimalsCount.Min, maxAnimals);

            view.AnimalsAmountSlider.MaxValue = maxAnimals;
            view.AnimalsAmountSlider.Value = currentAnimals;
        }

        void ApplyConfig()
        {
            {
                view.SizeSlider.Slider.wholeNumbers = true;
                view.SizeSlider.MinValue = config.TerrainSize.Min;
                view.SizeSlider.MaxValue = config.TerrainSize.Max;
                view.SizeSlider.Value = config.TerrainSize.Value;
            }
            {
                view.AnimalsAmountSlider.Slider.wholeNumbers = true;
                view.AnimalsAmountSlider.MinValue = config.AnimalsCount.Min;
                view.AnimalsAmountSlider.MaxValue = config.AnimalsCount.Max;
                view.AnimalsAmountSlider.Value = config.AnimalsCount.Value;
            }
            {
                view.SpeedSlider.Slider.wholeNumbers = false;
                view.SpeedSlider.MinValue = config.AnimalsSpeed.Min;
                view.SpeedSlider.MaxValue = config.AnimalsSpeed.Max;
                view.SpeedSlider.Value = config.AnimalsSpeed.Value;
            }
        }

        void IDisposable.Dispose()
        {
            view.SizeSlider.ValueChanged -= OnSizeValueChanged;
            view.AnimalsAmountSlider.ValueChanged -= OnAnimalsValueChanged;
            view.SpeedSlider.ValueChanged -= OnSpeedValueChanged;
            view.SimulateButton.onClick.RemoveAllListeners();
        }
    }
}
