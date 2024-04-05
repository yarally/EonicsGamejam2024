using Lib.Door;
using UnityEngine;

namespace Levels.RensHijdra2
{
    public class DoorController : AbstractDoorController
    {
        protected override bool CanOpen()
        {
            return base.CanOpen();
        }

        private void Update()
        {
            transform.position = transform.position + new Vector3(0, -0.005f, 0);
        }
    }
}