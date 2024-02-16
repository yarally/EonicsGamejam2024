using System.Collections;
using UnityEngine;

namespace Lib.Hazard
{
    public class Jp : MonoBehaviour
    {
        [SerializeField] private float delay;
        [SerializeField] private GameObject laser;
        [SerializeField] private float interval;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Sprite altSprite;
        private SpriteRenderer _sr;

        private void Start()
        {
            _sr = GetComponent<SpriteRenderer>();
            StartCoroutine(Shoot());
        }

        private IEnumerator Shoot()
        {
            yield return new WaitForSeconds(delay);
            while (true)
            {
                _sr.sprite = altSprite;
                yield return new WaitForSeconds(interval * 0.1f);
                Instantiate(laser, transform);
                yield return new WaitForSeconds(interval * 0.2f);
                _sr.sprite = sprite;
                yield return new WaitForSeconds(interval * 0.7f);
            }
        }
    
    }
}