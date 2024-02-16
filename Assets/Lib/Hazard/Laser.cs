using Lib.Player;
using UnityEngine;

namespace Lib.Hazard
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private float speed;
        private float _direction;

        void Start()
        {
            _direction = FindFirstObjectByType<AbstractPlayerController>().transform.position.x < transform.position.x
                ? -1f
                : 1f;
            Destroy(gameObject, 5f);
        }

        private void FixedUpdate()
        {
            transform.position += Vector3.right * (_direction * Time.fixedDeltaTime * speed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<AbstractPlayerController>().Hurt();
            }
        }
    }
}