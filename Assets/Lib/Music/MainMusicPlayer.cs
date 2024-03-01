using UnityEngine;

namespace Lib.Music
{
    public class MainMusicPlayer : MonoBehaviour
    {

        [SerializeField] private AudioClip main;
        [SerializeField] private AudioClip alt;
        private AudioSource _as;
        public static MainMusicPlayer Instance;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            Instance = this;
            _as = GetComponent<AudioSource>();
            _as.clip = main;
            _as.Play();
        }

        public void AltMusic()
        {
            var currTime = _as.time;
            _as.Pause();
            _as.clip = alt;
            _as.time = currTime;
            _as.volume = 0.2f;
            _as.Play();
        }
        
        public void MainMusic()
        {
            var currTime = _as.time;
            _as.Pause();
            _as.clip = main;
            _as.time = currTime;
            _as.volume = 0.4f;
            _as.Play();
        }
    }
}
