using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Core.Managers
{
    public class SoundManager : MonoBehaviour
    {
        private AudioSource mainSound;
        public UnityEvent OnSoundEnd;

        private void Awake()
        {
            mainSound = GetComponent<AudioSource>();
            if (mainSound == null)
                mainSound = gameObject.AddComponent<AudioSource>();
        }
        public void PlaySound(AudioClip clip)
        {
            if (clip == null)
            {
                Debug.LogWarning("You haven't assigned an audioclip to play");
                return;
            }
            if (mainSound != null)
            {
                mainSound.clip = clip;
                mainSound.Play();
                StartCoroutine(nameof(CheckAudioclipEnd));
            }
            else
            {
                Debug.LogWarning("You don't have a main sound audiosource");
            }
        }

        public void StopPlaying(AudioClip clip)
        {
            if (mainSound != null && clip == mainSound.clip)
            {
                mainSound.Stop();
            }
        }

        private IEnumerator CheckAudioclipEnd()
        {
            while (mainSound.time < mainSound.clip.length)
            {
                yield return new WaitForEndOfFrame();
            }
            OnSoundEnd?.Invoke();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
