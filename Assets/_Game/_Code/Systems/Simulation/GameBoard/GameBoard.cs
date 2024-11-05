using Life.Services.Save;
using System;
using UnityEngine;
using VContainer.Unity;

namespace Life.Systems.Simulation
{
    internal class GameBoard : IStartable, IDisposable
    {
        private readonly GameBoardView view;
        private readonly SimulationSettings simulationSettings;
        private readonly GameBoardConfig config;
        private readonly ISaveService saveService;

        public GameBoard(GameBoardView view, SimulationSettings simulationSettings, GameBoardConfig config, ISaveService saveService)
        {
            this.view = view;
            this.simulationSettings = simulationSettings;
            this.config = config;
            this.saveService = saveService;

            saveService.Saving += OnSaving;
        }

        private ISaveData OnSaving() =>
            simulationSettings;

        void IStartable.Start()
        {
            view.MeshFilter.sharedMesh = GenerateMesh();
            view.NavMeshSurface.tileSize = (int)(simulationSettings.Size * 18);
            view.NavMeshSurface.BuildNavMesh();
        }

        Mesh GenerateMesh()
        {
            Mesh mesh = new Mesh();

            uint size = simulationSettings.Size;
            float offset = size / 2f;

            Vector3[] verts = new Vector3[]
            {
                new Vector3(-offset, 0, -offset),
                new Vector3(-offset, 0, offset),
                new Vector3(offset, 0, offset),
                new Vector3(offset, 0, -offset)
            };

            int[] tris = new int[] { 2, 3, 0, 0, 1, 2 };

            mesh.SetVertices(verts);
            mesh.SetTriangles(tris, 0);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            return mesh;
        }

        void IDisposable.Dispose()
        {
            saveService.Saving -= OnSaving;
        }
    }
}
