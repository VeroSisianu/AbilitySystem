using Scripts.Core.AbilitySystem;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.AbilitySystem
{
    public class AbilityController : MonoBehaviour
    {
        private AbilityComponent[] _abilityComponents;

        private void Awake()
        {
            _abilityComponents = GetComponentsInChildren<AbilityComponent>();
        }

        public void ActivateAbility()
        {
            EnableAbilityComponents();
            foreach (AbilityComponent abilityComponent in _abilityComponents)
            {
                abilityComponent.Activate();
            }
        }
        public void EnableAbilityComponents()
        {
            foreach (AbilityComponent abilityComponent in _abilityComponents)
            {
                abilityComponent.IsEnabled = true;
                Debug.Log($"Ability component {abilityComponent.GetType().Name} has been enabled.");
            }
        }
        public void DisableAbilityComponents()
        {
            foreach (AbilityComponent abilityComponent in _abilityComponents)
            {
                abilityComponent.IsEnabled = false;
                Debug.Log($"Ability component {abilityComponent.GetType().Name} has been disabled.");
            }
        }
        public void TestActivation(AbilityComponent ability)
        {
            if (_abilityComponents != null)
                foreach (var abilityComponent in _abilityComponents)
                {
                    if (abilityComponent.GetType() == ability.GetType())
                        abilityComponent.Activate();
                }
        }

        public void TestDeactivation(AbilityComponent ability)
        {
            if (_abilityComponents != null)
                foreach (var abilityComponent in _abilityComponents)
                {
                    if (abilityComponent.GetType() == ability.GetType())
                        abilityComponent.Deactivate();
                }
        }
    }
}
