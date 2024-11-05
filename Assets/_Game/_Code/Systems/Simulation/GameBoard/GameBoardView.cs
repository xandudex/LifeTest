using Unity.AI.Navigation;
using UnityEngine;

namespace Life.Systems.Simulation
{
    internal class GameBoardView : MonoBehaviour
    {
        [field: SerializeField]
        public MeshFilter MeshFilter { get; private set; }

        [field: SerializeField]
        public NavMeshSurface NavMeshSurface { get; private set; }
    }
}
