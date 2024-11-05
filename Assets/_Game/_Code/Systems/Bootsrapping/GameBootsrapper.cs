using UnityEngine;
using UnityEngine.SceneManagement;

namespace Life.Systems.Bootstrapping
{
    internal class GameBootstrapper : MonoBehaviour
    {
        [SerializeField]
        int startSceneIndex = 1;
        void Awake()
        {
            SceneManager.LoadSceneAsync(startSceneIndex);
        }
    }
}
