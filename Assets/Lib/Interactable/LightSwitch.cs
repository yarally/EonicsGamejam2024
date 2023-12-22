using Lib.Door;
using UnityEngine;

namespace Lib.Interactable
{
    public class LightSwitch : AbstractInteractable
    {
        [SerializeField] private Sprite altSprite;
        private bool isOn = true;
        protected override void OnInteract()
        {
            if (!isOn) return;
            FindFirstObjectByType<AbstractDoorController>().TurnOffLight();
            isOn = false;
            GetComponent<SpriteRenderer>().sprite = altSprite;
        }
    }
}