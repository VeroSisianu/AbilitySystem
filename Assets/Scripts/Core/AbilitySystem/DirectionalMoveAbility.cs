using Scripts.Core.AbilitySystem;
using Scripts.Core.AbilitySystem.AbilityData;
using UnityEngine;
using CharacterController = Scripts.Core.Character.CharacterController;


namespace Assets.Scripts.Core.AbilitySystem
{
    public sealed class DirectionalMoveAbility : AbilityComponent
    {
        [SerializeField] private MoveData _data;
        private CharacterController _characterController;


        protected override void Awake()
        {
            _characterController = GetComponentInParent<CharacterController>();
        }
        public override bool Activate()
        {
            if (base.Activate() == false)
                return false;
            _characterController.OnMoveEnded.AddListener(OnMoveEnded);
            _characterController.MoveCharacter(_data.Offset, _data.SpeedMultiplier);
            OnAbilityStartedEvent?.Invoke(this);
            return true;
        }

        private void OnMoveEnded()
        {
            _characterController.OnMoveEnded.RemoveListener(OnMoveEnded);
            Deactivate();
        }

        public override bool Deactivate()
        {
            if (base.Deactivate() == false)
                return false;

            _characterController.StopMoving();

            _isActive = false;
            Debug.Log($"Ability {this.GetType().Name} is deactivated.");
            return true;
        }
    }
}
