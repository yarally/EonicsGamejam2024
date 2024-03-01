using UnityEngine;

namespace Lib.Interactable
{
    /**
     * This script can be implemented for objects that your player should be able to click. It spawns a green indicator
     * above the object when the player is near, and provides a handle for the interaction event when the player presses
     * the interact button.
     */
    public abstract class AbstractInteractable : MonoBehaviour
    {
        private GameObject _indicator;
        private GameObject _indicatorRef;
        private float _indicatorOffset;

        private void Awake()
        {
            _indicator = Resources.Load<GameObject>("Indicator");
            _indicatorOffset = GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerTrigger"))
            {
                OnInteract();
            }

            if (other.CompareTag("Player"))
            {
                _indicatorRef = Instantiate(_indicator, transform);
                _indicatorRef.transform.position += new Vector3(0, _indicatorOffset + 0.25f, 0);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Destroy(_indicatorRef);
            }
        }

        /**
         * This method is called if the Player interaction object touches this object's collider.
         * The interaction object is spawned when the action button (LMB) is pressed.
         */
        protected abstract void OnInteract();
    }
}