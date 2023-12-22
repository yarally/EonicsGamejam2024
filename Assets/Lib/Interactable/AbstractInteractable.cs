using UnityEngine;

namespace Lib.Interactable
{
    public abstract class AbstractInteractable : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerTrigger"))
            {
                OnInteract();
            }
        }

        /**
         * This method is called if the Player interaction object touches this object's collider.
         * The interaction object is spawned when the action button (LMB) is pressed.
         */
        protected abstract void OnInteract();
    }
}