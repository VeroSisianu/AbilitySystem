using Scripts.Core.AbilitySystem;
using Scripts.Core.AbilitySystem.AbilityData;
using UnityEngine;

namespace Assets.Scripts.Core.AbilitySystem
{
    public sealed class AreaDamageAbility : AbilityComponent

    {
        [SerializeField] private AreaDamageData _data;
        public override bool Activate()
        {
            if (base.Activate() == false)
                return false;
            
            Debug.Log("Choose area to damage.");
            Deactivate();
            return true;
        }

        public override bool Deactivate()
        {
            if(base.Deactivate() == false) 
                return false;
           
            _isActive = false;
            Debug.Log($"Ability {this.GetType().Name} is deactivated.");
            return true;
        }
    }
}
