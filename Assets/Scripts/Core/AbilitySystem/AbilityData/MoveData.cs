using UnityEngine;

namespace Scripts.Core.AbilitySystem.AbilityData
{
    [CreateAssetMenu]
    public class MoveData : ScriptableObject
    {
        public Vector3 Offset;
        public float SpeedMultiplier;
    }
}
