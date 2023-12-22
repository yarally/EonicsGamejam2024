using UnityEngine;

namespace Lib.Door
{
    public abstract class AbstractDoorController : MonoBehaviour
    {
        [SerializeField] private GameObject darkPanel;
        private bool _light;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && CanOpen())
            {
                Debug.Log("Level end!");
            }
        }

        protected virtual bool CanOpen()
        {
            return _light;
        }

        /**
         * This method is called by the LightSwitch if the player interacts with it. This also opens the door to the
         * next level.
         */
        public void TurnOffLight()
        {
            if (_light) return;
            _light = true;
            darkPanel.SetActive(true);
        }
    }
}