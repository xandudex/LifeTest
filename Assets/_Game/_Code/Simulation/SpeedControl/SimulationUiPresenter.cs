using System;
using UnityEngine;
using VContainer.Unity;

namespace Xandudex.LifeGame
{
    internal class SimulationUiPresenter : IStartable, IDisposable
    {
        private const string MenuSceneName = "Menu Scene";
        private readonly SimulationUiView view;
        private readonly ISceneLoader sceneLoader;
        private readonly ISaveService saveService;

        public SimulationUiPresenter(SimulationUiView view, ISceneLoader sceneLoader, ISaveService saveService)
        {
            this.view = view;
            this.sceneLoader = sceneLoader;
            this.saveService = saveService;
        }

        void IStartable.Start()
        {
            view.SpeedSlider.ValueChanged += SpeedChanged;
            view.SpeedSlider.MinValue = 0;
            view.SpeedSlider.Value = 1;
            view.SpeedSlider.MaxValue = 1000;
            view.LeaveButton.onClick.AddListener(() => sceneLoader.Load(MenuSceneName));
            view.SaveButton.onClick.AddListener(() => saveService.Save());
        }

        private void SpeedChanged(float speed)
        {
            Time.timeScale = speed;
        }

        void IDisposable.Dispose()
        {
            Time.timeScale = 1;
            view.SpeedSlider.ValueChanged -= SpeedChanged;
            view.LeaveButton.onClick.RemoveAllListeners();
            view.SaveButton.onClick.RemoveAllListeners();
        }
    }
}
