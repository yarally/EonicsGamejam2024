using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lib.Interactable;
using Lib.Door;
using Lib.Player;
using TMPro;
using UnityEditor.UI;
using UnityEngine.UI;
using UnityEngine.Video;
using Random = UnityEngine.Random;

namespace Levels.HBL_Expert
{
    public class WindowsInteracter : AbstractInteractable
    {

        [SerializeField] private int percentagePerClick;
        [SerializeField] private Sprite damagedSprite;
        [SerializeField] private Sprite BSODSprite;
        [SerializeField] private AudioSource crashSound;
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private float hoverPeriod;
        [SerializeField] private float hoverAmplitude;
        private Transform tf;
        private float clock;
        private int percentage;
        private bool isInHurtAnimation;
        private string displayText;
        public bool IsDying;
        
        public string DisplayText
        {
            get { return displayText; }
        }
        
        public int Percentage
        {
            get { return percentage; }
        }


        // Start is called before the first frame update
        void Start()
        {
            IsDying = false;
            percentage = 0;
            clock = 0;
            isInHurtAnimation = false;
            tf = transform;
            displayText = "Installing updates 1/1. Please click to continue.";
        }

        private void Update()
        {
            if (IsDying)
            {
                var sr = GetComponent<SpriteRenderer>();
                sr.sprite = sr.sprite == BSODSprite ? sr.sprite : BSODSprite;
                return;
            }
                
            var y = tf.localPosition.y;
            y = hoverAmplitude * (float) Math.Sin(clock * (2 * Math.PI / hoverPeriod));
            tf.localPosition = new Vector3(0, y, 0);
            clock = clock >= hoverPeriod * 2 * Math.PI ? 0 : clock + Time.deltaTime;
        }

        protected override void OnInteract()
        {
            if (percentage >= 100)
                return;
            if (percentage == 0)
                GetComponent<AudioSource>().Play();
            percentage += percentagePerClick;
            displayText = $"Windows Updates: {percentage}%";
            if (percentage >= 99)
                return;
            if (!isInHurtAnimation)
                StartCoroutine(hurtAnimation());
        }

        private IEnumerator hurtAnimation()
        {
            isInHurtAnimation = true;
            var sr = GetComponent<SpriteRenderer>();
            sr.sprite = damagedSprite;
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.1f);
                sr.enabled = false;
                yield return new WaitForSeconds(0.1f);
                sr.enabled = true;
            }
            sr.sprite = normalSprite;
            isInHurtAnimation = false;
        }

    }

    
}