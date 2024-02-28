using Lib.StaticEnvironment;
using Unity.VisualScripting;
using UnityEngine;

namespace Levels.RensHijdra
{
    
    public class DisapearingPlatformController : MonoBehaviour
    {
        private static Color RED = Color.red;
        private static Color RED_TRANSPARANT = new Color(1, 0, 0, 0);
        
        private SpriteRenderer _spriteRenderer;

        public float disappearSpeedup = 1;

        Color lerpedColor;
        private bool touched = false;
        private float touchTime;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
        }
        
        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _yPos = transform.position.y;
            
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            lerpedColor = _spriteRenderer.material.color;

        }

        private void Update()
        {
            _collider.enabled = (_player.transform.position.y > _yPos + 0.45f);

            if (touched)
            {
                var passedTime = Time.time - touchTime;
                lerpedColor = Color.Lerp(RED, RED_TRANSPARANT, passedTime * disappearSpeedup);
                _spriteRenderer.color = lerpedColor;
                if (lerpedColor == RED_TRANSPARANT)
                {
                    Destroy(gameObject);
                }
            }
        }


        private BoxCollider2D _collider;
        private Transform _player;
        private float _yPos;
        

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.transform.parent = transform;
                if (!touched && other.rigidbody.velocity.y == 0 && other.transform.position.y > transform.position.y)
                {
                    touched = true;
                    touchTime = Time.time;
                }
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.transform.parent = null;
            }
        }

    }
}
