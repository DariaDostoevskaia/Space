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
                _instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();
        }

        public static void Play(AudioClip audioClip)
        {
            _instance._audioSource.PlayOneShot(audioClip);
        }
    }
}