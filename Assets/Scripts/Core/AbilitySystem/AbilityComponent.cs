using UnityEngine;
using UnityEngine.Events;


namespace Scripts.Core.AbilitySystem
{
    /// <summary>
    /// Common functionalities between all player abilities
    /// </summary>
    public abstract class AbilityComponent : MonoBehaviour
    {
        [HideInInspector] public UnityEvent<AbilityComponent> OnAbilityStartedEvent;
        [HideInInspector] public UnityEvent<AbilityComponent> OnAbilityEndedEvent;
        [HideInInspector] public UnityEvent<AbilityComponent> OnAbilityEnabledEvent;


        public bool IsEnabled { get; set; }

        protected bool _isActive = false;
        protected bool _isInited = false;


        #region Virtual Methods
        protected virtual void Awake() { }
        protected virtual void Start() { }
        protected virtual void OnDestroy() { }

        public virtual bool Activate()
        {
            if (_isActive)
            {
                Debug.LogWarning($"Ability {this.GetType().Name} is already active.");
                return false;
            }
            if (IsEnabled == false)
            {
                Debug.LogWarning($"Ability {this.GetType().Name} is not enabled.");
                return false;
            }
            if (_isInited == false)
            {
                Init();
            }
            _isActive = true;
            Debug.Log($"Ability {this.GetType().Name} is active.");
            return true;
        }
        public virtual bool Deactivate()
        {
            if (_isActive == false)
            {
                Debug.LogWarning($"Ability {this.GetType().Name} is not active. Cannot deactivate a non-active ability.");
                return false;
            }
            else
            {
                return true;
            }
        }

        protected virtual void Init()
        {
            Debug.Log("Initialized ability: " + this.GetType().Name);
        }
        #endregion
    }
}