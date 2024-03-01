using System;
using Lib.Player;
using UnityEngine;

namespace Lib.StaticEnvironment
{
    /**
     * Disables gravity and manages the player velocity to simulate the behaviours of a ladder.
     */
    public class Ladder : MonoBehaviour
    {
        protected Rigidbody2D _playerRb;
        protected float _gravity;
        protected bool _active;

        protected virtual void Awake()
        {
            _playerRb = FindFirstObjectByType<AbstractPlayerController>().GetComponent<Rigidbody2D>();
            _gravity = _playerRb.gravityScale;
        }

        protected virtual void OnTriggerStay2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            _playerRb.gravityScale = 0;
            _playerRb.velocity = Vector2.zero;
            _active = true;
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            _playerRb.gravityScale = _gravity;
            _active = false;
        }

        protected virtual void FixedUpdate()
        {
            if (!_active) return;
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                _playerRb.velocity = new Vector2(_playerRb.velocity.x, 5f);
            }
            else
            {
                _playerRb.velocity = new Vector2(_playerRb.velocity.x, -2f);
            }
        }
    }
}