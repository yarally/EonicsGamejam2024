using Lib.Player;
using UnityEngine;

namespace Lib.Hazard
{
    /**
     * if (Player touches) then: Player dies!
     */
    [RequireComponent(typeof(Collider2D))]
    public class TouchAndDie : MonoBehaviour
    {
        protected virtual void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<AbstractPlayerController>().Hurt();
            }
        }
    }
}
