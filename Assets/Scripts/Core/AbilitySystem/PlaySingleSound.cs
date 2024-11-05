using Scripts.Core.AbilitySystem;
using Scripts.Core.AbilitySystem.AbilityData;
using Scripts.Core.Managers;
using UnityEngine;

namespace Assets.Scripts.Core.AbilitySystem
{
    public sealed class PlaySingleSound : AbilityComponent
    {
        [SerializeField] private SingleSoundData _data;
        private SoundManager _soundManager;

        protected override void Awake()
        {
            base.Awake();
            _soundManager = FindObjectOfType<SoundManager>();
        }
        public override bool Activate()
        {
            if (base.Activate() == false)
                return false;
            _soundManager.OnSoundEnd.AddListener(OnSoundEnd);
            _soundManager.PlaySound(_data.Clip);
            return true;
        }
        public override bool Deactivate()
        {
            if (base.Deactivate() == false)
                return false;

            _soundManager.StopPlaying(_data.Clip);

            _isActive = false;
            Debug.Log($"Ability {this.GetType().Name} is deactivated.");
            return true;
        }

        private void OnSoundEnd()
        {
            _soundManager.OnSoundEnd.RemoveListener(OnSoundEnd);
            Deactivate();
        }
    }
}
