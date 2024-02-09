using System;
using UnityEngine;

namespace Lib.Movement
{
    public class MoveBetween : MonoBehaviour
    {
        [SerializeField] private Transform obj;
        [SerializeField] private Transform from;
        [SerializeField] private Transform to;
        [SerializeField] private float speed;

        private Transform _target;

        private void Awake()
        {
            _target = to;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (Vector3.Distance(obj.position, _target.position) < 0.01f)
            {
                _target = _target == from ? to : from;
            }

            obj.position = Vector3.MoveTowards(obj.position, _target.position, speed * Time.deltaTime);
        }
    }
}
