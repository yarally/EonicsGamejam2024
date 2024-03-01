using UnityEngine;

namespace Lib.AudioUtils
{
    /**
     * This script can be placed on any GameObject that contains an AudioSource script. The volume of the audio will
     * then be dynamically updated base on the proximity to the player.
     */
    [RequireComponent(typeof(AudioSource))]
    public class ProximityAudio : MonoBehaviour
    {
        [SerializeField] private float range;
        private AudioSource _as;
        private Transform _player;
        private float _maxVolume;

        private void Awake()
        {
            _as = GetComponent<AudioSource>();
            _maxVolume = _as.volume;
            _player = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            var dist = Vector3.Distance(_player.position, transform.position);
            if (dist > range)
            {
                _as.volume = 0;
                return;
            }
            _as.volume = (range - dist) / range * _maxVolume;
        }
    }
}