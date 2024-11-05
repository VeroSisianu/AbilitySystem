using Scripts.Core.AbilitySystem;
using Scripts.Core.AbilitySystem.AbilityData;
using UnityEngine;

namespace Assets.Scripts.Core.AbilitySystem
{
    public sealed class SingleAnimationAbility : AbilityComponent
    {
        [SerializeField] private SingleAnimationData _data;
        private Animator animator;

        protected override void Awake()
        {
            animator = GetComponentInParent<Animator>();
            base.Awake();
        }
        public override bool Activate()
        {
            if (base.Activate() == false)
                return false;

            if (animator == null)
            {
                Debug.LogWarning("You don't have an animator component on the player.");
                Deactivate();
                return false;
            }
            if (_data.Clip == null)
            {
                Debug.LogWarning("You don't have an animation set in the data.");
                Deactivate();
                return false;
            }
            animator.Play(_data.Clip.name, 0);
            Invoke(nameof(Deactivate), _data.Clip.length);
            return true;
        }
        public override bool Deactivate()
        {
            if (base.Deactivate() == false)
                return false;

            if (animator != null)
            {  
                animator.Play("Default"); 
            }

            _isActive = false;
            Debug.Log($"Ability {this.GetType().Name} is deactivated.");
            return true;
        }
    }
}
