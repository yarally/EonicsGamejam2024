using Lib.Player;
using UnityEngine;

namespace Lib.Hazard
{
    [RequireComponent(typeof(Collider2D))]
    public class TouchAndDie : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
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
