using System;
using UnityEngine;
using VContainer.Unity;

namespace Xandudex.LifeGame
{
    internal class GameBoard : IStartable, IDisposable
    {
        private readonly GameBoardView view;
        private readonly SimulationSettings settings;
        private readonly GameBoardConfig config;
        private readonly ISaveService saveService;

        public GameBoard(GameBoardView view, SimulationSettings settings, GameBoardConfig config, ISaveService saveService)
        {
            this.view = view;
            this.settings = settings;
            this.config = config;
            this.saveService = saveService;

            saveService.Saving += OnSaving;
        }

        private ISaveData OnSaving() =>
            settings;

        void IStartable.Start()
        {
            view.MeshFilter.sharedMesh = GenerateMesh();
            view.NavMeshSurface.tileSize = (int)(settings.Size * 18);
            view.NavMeshSurface.BuildNavMesh();
        }

        Mesh GenerateMesh()
        {
            Mesh mesh = new Mesh();

            uint size = settings.Size;
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
