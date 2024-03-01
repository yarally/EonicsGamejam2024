using System.Collections;
using UnityEngine;

namespace Lib.Hazard
{
    /**
     * Jan Pixel is here to kill you! This enemy will spawn [Laser] game objects on an interval that you can adjust
     * inside the object inspector after dropping the prefab in your scene.
     */
    public class Jp : MonoBehaviour
    {
        [SerializeField] protected float delay;
        [SerializeField] protected GameObject laser;
        [SerializeField] protected float interval;
        [SerializeField] protected Sprite sprite;
        [SerializeField] protected Sprite altSprite;
        protected SpriteRenderer _sr;

        protected virtual void Start()
        {
            _sr = GetComponent<SpriteRenderer>();
            StartCoroutine(Shoot());
        }

        protected virtual IEnumerator Shoot()
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