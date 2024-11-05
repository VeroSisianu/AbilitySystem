using Assets.Scripts.Core.AbilitySystem.AbilityData;
using Scripts.Core.AbilitySystem;
using UnityEngine;
using CharacterController = Scripts.Core.Character.CharacterController;


namespace Assets.Scripts.Core.AbilitySystem
{
    public sealed class ParticleEffectsAbility : AbilityComponent
    {
        [SerializeField] private ParticleEffectData _data;

        private ParticleSystem[] _particleSystems;
        private GameObject _instantiatedEffect;
        private CharacterController _characterController;


        protected override void Awake()
        {
            _characterController = GetComponentInParent<CharacterController>();
        }
        public override bool Activate()
        {
            if (base.Activate() == false)
                return false;
            _instantiatedEffect = Instantiate(_data.EffectPrefab, _characterController.transform.position, Quaternion.identity);
            _particleSystems = _instantiatedEffect.GetComponentsInChildren<ParticleSystem>();
            foreach (var particleSystem in _particleSystems)
            {
                particleSystem.Play();
            }
            Invoke(nameof(Deactivate), _data.EffectTime);
            return true;
        }
        public override bool Deactivate()
        {
            if (base.Deactivate() == false)
                return false;
           
            if (_instantiatedEffect != null)
                Destroy(_instantiatedEffect);
            
            _isActive = false;
            Debug.Log($"Ability {this.GetType().Name} is deactivated.");
            return true;
        }
    }
}
