using System.Collections;
using System.Collections.Generic;
using Lib.Hazard;
using Lib.Interactable;
using UnityEngine;

namespace Levels.HBLGamejam
{
    public class DestroyController : AbstractInteractable
    {

        private bool clicked;
        [SerializeField] private Sprite altSprite;
        // Start is called before the first frame update
        void Start()
        {
            this.clicked = false;
        }
    
        protected override void OnInteract()
        {
            if (clicked) return;
            StartCoroutine(KillJps());
            clicked = true;
            GetComponent<SpriteRenderer>().sprite = altSprite;
        }

        private IEnumerator KillJps()
        {
            Jp[] jps = FindObjectsByType<Jp>(sortMode: FindObjectsSortMode.None);

            GetComponent<AudioSource>().Play();
            var jpObjects = new List<GameObject>();
            foreach (var jp in jps)
            {
                Destroy(jp);
                jpObjects.Add(jp.gameObject);
            }
            yield return new WaitForSeconds(2f);
            foreach (var jpObject in jpObjects)
            {
                Destroy(jpObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
