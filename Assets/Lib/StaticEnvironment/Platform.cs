using UnityEngine;

namespace Lib.StaticEnvironment
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Platform : MonoBehaviour
    {
        private BoxCollider2D _collider;
        private Transform _player;
        private float _yPos;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _yPos = transform.position.y;
        }

        private void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            _collider.enabled = (_player.transform.position.y > _yPos + 0.45f);
        }
    }
}