using UnityEngine;
using VContainer.Unity;

namespace Life.Systems.Simulation
{
    internal class CameraMovement : ITickable, IStartable
    {
        float speed = 10f;

        private readonly Transform cameraTransform;
        private readonly SimulationSettings settings;

        public CameraMovement(Camera camera, SimulationSettings settings)
        {
            this.settings = settings;
            cameraTransform = camera.transform;
        }

        void IStartable.Start()
        {
            cameraTransform.position = new Vector3(settings.Size / 2f, settings.Size / 4f, 0);
        }

        void ITickable.Tick()
        {
            if (Time.timeScale == 0)
                return;
            cameraTransform.RotateAround(Vector3.zero, Vector3.up, speed * (Time.deltaTime / Time.timeScale));
            cameraTransform.LookAt(Vector3.zero);
        }
    }
}
