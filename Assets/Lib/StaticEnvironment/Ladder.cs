using System;
using Lib.Player;
using UnityEngine;

namespace Lib.StaticEnvironment
{
    public class Ladder : MonoBehaviour
    {
        private Rigidbody2D _playerRb;
        private float _gravity;
        private bool _active;

        private void Awake()
        {
            _playerRb = FindFirstObjectByType<AbstractPlayerController>().GetComponent<Rigidbody2D>();
            _gravity = _playerRb.gravityScale;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            _playerRb.gravityScale = 0;
            _playerRb.velocity = Vector2.zero;
            _active = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            _playerRb.gravityScale = _gravity;
            _active = false;
        }

        private void FixedUpdate()
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