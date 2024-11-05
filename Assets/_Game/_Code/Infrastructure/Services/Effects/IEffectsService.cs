using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

namespace Life.Services.Effects
{

    internal interface IEffectsService
    {
        UniTask PlayAt(VisualEffectAsset effectAsset, Vector3 position);
    }
}
