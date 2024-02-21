using System.Collections;
using System.Collections.Generic;
using Lib.Hazard;
using Lib.Player;
using UnityEngine;

namespace Levels.HBL_Expert
{
    public class ErrorController : MonoBehaviour
    {
        [SerializeField] private Sprite messageSprite;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(waitToPop());
            GetComponent<BoxCollider2D>().enabled = false;
        }
        private IEnumerator waitToPop()
        {
            var sr = GetComponent<SpriteRenderer>();
            yield return new WaitForSeconds(1f);
            sr.sprite = messageSprite;
            GetComponent<AudioSource>().Play();
            var tr = transform;
            GetComponent<BoxCollider2D>().enabled = true;
            // var myHitbox = Instantiate(_hitbox, tr.position, tr.rotation);
            yield return new WaitForSeconds(1f);
            // Destroy(myHitbox);
            Destroy(gameObject);
        }
    }
}