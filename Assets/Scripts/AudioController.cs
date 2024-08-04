using System.Threading.Tasks;
using UnityEngine;

namespace HlStudio
{
    public class AudioController : MonoBehaviour, IInitializable
    {
        public bool Initialized { get; set; }
        
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        public Task Init()
        {
            _audioSource.clip = _audioClip;
            _audioSource.loop = true;

            StartMusic();
            
            return Task.CompletedTask;
        }

        private void StartMusic()
        {
            _audioSource.Play();
        }
    }
}