using System.Collections;
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
            GetComponent<AudioSource>().Play();
            FindFirstObjectByType<LevelController>().TurnOffLights();
            Invoke(nameof(OpenDoor), 0.5f);
            isOn = false;
            GetComponent<SpriteRenderer>().sprite = altSprite;
        }

        protected virtual void OpenDoor()
        {
            FindFirstObjectByType<AbstractDoorController>().OpenDoor();
        }
    }
}