using Lib.Door;
using UnityEngine;

namespace Levels.HBL_Expert
{
    public class DoorController : AbstractDoorController
    {
        [SerializeField] private Sprite closedSprite;
        private bool _opened;

        public void CloseDoor()
        {
            if (!_opened) return;
            GetComponent<AudioSource>().Play();
            _opened = true;
            GetComponent<SpriteRenderer>().sprite = closedSprite;
        }


        protected override bool CanOpen()
        {
            return base.CanOpen();
        }
    }
}