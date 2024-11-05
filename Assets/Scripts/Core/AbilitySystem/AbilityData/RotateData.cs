using UnityEngine;

namespace Scripts.Core.AbilitySystem.AbilityData
{
    [CreateAssetMenu]
    public class RotateData : ScriptableObject
    {
        public Vector3 TargetRotation;
        public float SpeedMultiplier;
    }
}