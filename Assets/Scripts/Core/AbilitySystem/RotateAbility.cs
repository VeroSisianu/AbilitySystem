using Scripts.Core.AbilitySystem;
using Scripts.Core.AbilitySystem.AbilityData;
using UnityEngine;
using CharacterController = Scripts.Core.Character.CharacterController;


namespace Assets.Scripts.Core.AbilitySystem
{
    public sealed class RotateAbility : AbilityComponent
    {
        [SerializeField] private RotateData _data;
        private CharacterController _characterController;


        protected override void Awake()
        {
            _characterController = GetComponentInParent<CharacterController>();
        }
        public override bool Activate()
        {
            if (base.Activate() == false)
                return false;
            _characterController.OnRotateEnded.AddListener(OnRotateEnded);
            _characterController.RotateCharacter(_data.TargetRotation, _data.SpeedMultiplier);
            OnAbilityStartedEvent?.Invoke(this);
            return true;
        }

        private void OnRotateEnded()
        {
            _characterController.OnRotateEnded.RemoveListener(OnRotateEnded);
            Deactivate();
        }

        public override bool Deactivate()
        {
            if (base.Deactivate() == false)
                return false;
            
            _characterController.StopRotating();
            
            _isActive = false;
            Debug.Log($"Ability {this.GetType().Name} is deactivated.");
            return true;
        }
    }
}
