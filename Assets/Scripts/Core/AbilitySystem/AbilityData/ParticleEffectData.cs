using UnityEngine;

namespace Assets.Scripts.Core.AbilitySystem.AbilityData
{
    [CreateAssetMenu]
    public class ParticleEffectData : ScriptableObject
    {
        public GameObject EffectPrefab;
        public float EffectTime;
    }
}
