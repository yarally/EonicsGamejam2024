using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels.HBL_Expert
{
    public class WinCrashController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<AudioSource>().Play();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}