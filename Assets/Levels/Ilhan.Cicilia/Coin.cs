using Lib.Player;
using UnityEngine;

namespace Levels.IlhanCicilia
{
    /**
     * if (Player touches) then: Player dies!
     */
    [RequireComponent(typeof(Collider2D))]
    public class Coin : MonoBehaviour
    {
        // protected virtual void Awake()
        // {
        //     GetComponent<Collider2D>().isTrigger = true;
        // }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {   
                other.collider.GetComponent<PlayerController>().gainJumpHeight();
                Destroy(gameObject);
            }
        }
    }
}
