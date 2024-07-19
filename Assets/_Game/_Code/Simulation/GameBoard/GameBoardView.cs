using Unity.AI.Navigation;
using UnityEngine;

namespace Xandudex.LifeGame
{
    internal class GameBoardView : MonoBehaviour
    {
        [field: SerializeField]
        public MeshFilter MeshFilter { get; private set; }

        [field: SerializeField]
        public NavMeshSurface NavMeshSurface { get; private set; }
    }
}
