using Cysharp.Threading.Tasks;
using Life.Data.Effects;
using Life.Utilities;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

namespace Life.Services.Effects
{
    internal class EffectsService : Service, IEffectsService
    {
        readonly ObjectPool<VisualEffect> visualEffectsPool;

        readonly EffectsConfig effectsConfig;
        public EffectsService(EffectsConfig effectsConfig)
        {
            this.effectsConfig = effectsConfig;

            visualEffectsPool = new(
                () => GameObject.Instantiate(effectsConfig.EffectPrefab),
                x => x.gameObject.SetActive(true),
                x => x.gameObject.SetActive(false));
        }

        public async UniTask PlayAt(VisualEffectAsset effectAsset, Vector3 position)
        {
            VisualEffect visualEffect = visualEffectsPool.Get();

            visualEffect.visualEffectAsset = effectAsset;
            visualEffect.transform.position = position;

            visualEffect.Play();

            //todo:find better way to deal with effect lifetime
            while (visualEffect.aliveParticleCount != 0)
                await UniTask.Yield(DisposeCancellationToken);

            visualEffectsPool.Release(visualEffect);
        }

        protected override void Dispose()
        {
            visualEffectsPool.Dispose();
        }
    }
}
