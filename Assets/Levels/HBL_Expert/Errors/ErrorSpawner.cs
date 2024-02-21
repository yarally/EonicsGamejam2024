using System.Collections;
using System.Collections.Generic;
using Lib.Player;
using UnityEngine;

namespace Levels.HBL_Expert
{
    public class ErrorSpawner : MonoBehaviour
    {
        [SerializeField] private Sprite messageSprite;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(waitToPop());
        }
        private IEnumerator waitToPop()
        {
            var sr = GetComponent<SpriteRenderer>();
            yield return new WaitForSeconds(1f);
            sr.sprite = messageSprite;
            GetComponent<AudioSource>().Play();
            
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<AbstractPlayerController>().Hurt();
            }
        }
    }
}