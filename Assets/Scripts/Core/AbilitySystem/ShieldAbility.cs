using Scripts.Core.AbilitySystem.AbilityData;
using UnityEngine;
using CharacterController = Scripts.Core.Character.CharacterController;


namespace Scripts.Core.AbilitySystem
{
    public sealed class ShieldAbility : AbilityComponent
    {
        [SerializeField] private ShieldDefenceData _data;
        private CharacterController _characterController;

        protected override void Awake()
        {
            _characterController = GetComponentInParent<CharacterController>();
        }
        public override bool Activate()
        {
            if (base.Activate() == false)
                return false;
            _characterController.AddDefence(_data.DefencePoints);
            Deactivate();
            return true;
        }

        public override bool Deactivate()
        {
            if (base.Deactivate() == false)
                return false;

            _characterController.RemoveDefence(_data.DefencePoints);
            _isActive = false;
            Debug.Log($"Ability {this.GetType().Name} is deactivated.");
            return true;
        }
    }
}
