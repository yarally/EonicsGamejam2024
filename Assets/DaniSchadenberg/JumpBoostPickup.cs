using Lib.Player;
using UnityEngine;
using Levels.DaniSchadenberg;

namespace Levels.DaniSchadenberg
{
    /**
     * if (Player touches) then: Player dies!
     */
    [RequireComponent(typeof(Collider2D))]
    public class JumpBoostPickup : MonoBehaviour
    {
        protected virtual void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().setJumpheight(5);
                Destroy(gameObject);
            }
        }
    }

}
