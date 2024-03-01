using System;
using UnityEngine;

namespace Lib.StaticEnvironment
{
    /**
     * The platform looks at the player y-position and disables its own collider when the player is below it.
     */
    [RequireComponent(typeof(BoxCollider2D))]
    public class Platform : MonoBehaviour
    {
        protected BoxCollider2D _collider;
        protected Transform _player;
        protected float _yPos;

        protected virtual void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _yPos = transform.position.y;
        }

        protected virtual void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
        }

        protected virtual void Update()
        {
            _collider.enabled = (_player.transform.position.y > _yPos + 0.45f);
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.transform.parent = transform;
            }
        }

        protected virtual void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.transform.parent = null;
            }
        }
    }
}