using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace AphroditeFightCode
{
    public class HeartController : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private Image img;
        public float shakeDuration = 0.5f;
        public float shakeStrength = 10f;
        public float fadeDuration = 1.0f;
        public int heartID;
        public bool heartDead = false;
        [SerializeField] private float heartHealth;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            img = GetComponent<Image>();
            if (GameData.onHeart <= heartID)
            {
                // heart still active
                // (i.e. if on 1st heart and this heart is 1 | 1 <= 1 T)
                // (on 2, this 1 | 2 <= 1 F => not on this heart, this heart is dead)
                heartHealth = 20;
                
            }
            else
            {
                heartHealth = 0;
            }
            anim.SetFloat("Health", heartHealth);
        }

        private void Update()
        {
            //check the current health of player
            // id = 1; health = 50
            // 
            
            if (heartHealth >= 0f)
            {
                heartHealth = GameData.playerHeath - (20 * (3 - heartID));
                //Debug.Log("Heart" + heartID + " has __" + heartHealth + "__ left | total health = " + GameData.playerHeath);
                anim.SetFloat("Health", heartHealth);
            }
            else if (heartHealth <= 0f && !heartDead)
            {
                heartDead = true;
                HeartDeath();
                //Color faded = img.color;
                //faded.a = 0.45f;
                //img.color = faded;

            }
        }

        private void HeartDeath()
        {
            LeanTween.moveLocalX(gameObject, transform.localPosition.x + shakeStrength, shakeDuration)
            .setEase(LeanTweenType.easeShake)
            .setLoopPingPong(1) // Shake back and forth once
            .setOnComplete(() =>
            {
                // Fade out animation
                LeanTween.color(img.rectTransform, new Color(img.color.r, img.color.g, img.color.b, 0f), fadeDuration)
                    .setEase(LeanTweenType.easeOutQuad)
                    .setOnComplete(() =>
                    {
                        Color faded = img.color;
                        faded.a = 0.45f;
                        img.color = faded;
                    });
            });
        }
    }
}
