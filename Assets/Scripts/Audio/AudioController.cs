using UnityEngine;

namespace SpaceGame.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioController : MonoBehaviour
    {
        private static AudioController _instance;
        private AudioSource _audioSource;

        private void Start()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();
        }

        public static void Play(AudioClip audioClip)
        {
            if (audioClip == null)
                return;
            if (_instance == null)
                return;
            _instance._audioSource.PlayOneShot(audioClip);
        }
    }
}