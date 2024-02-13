using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace AphroditeFightCode
{
    public class QuestKeyController : MonoBehaviour
    {
        [Header("Quest Key SO")]
        public ClickableKey key;

        [Header("Sprite Controls")]
        private SpriteRenderer keySprite;
        private Color spriteOriginalColor;
        [SerializeField] private Color hoverColor = new Color(255,255, 153, 154);

        [Header("Key Animation")]
        private Animator anim;
        public float bounceHeight = 1.25f;
        public float bounceDuration = 0.6f;




        private void Awake()
        {
            keySprite = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
            spriteOriginalColor = keySprite.color;
            Intialize();
        }

        private void Intialize()
        {
            if (key != null)
            {
                keySprite.sprite = key.keySprite;
            }
            else
            {
                Debug.LogWarning("A ClickableKey object has not been assigned");
            }

            if (key.hasAnimation)
            {
                //assign the animation
                anim.enabled = true;
                anim.SetBool("isSpinning", true);
                
            }
            else
            {
                BounceAnim();
            }
        }

        void OnMouseOver()
        {
            //change the alpha/color of object
            
            keySprite.color = hoverColor;
            GUITextManager.instance.PrintToGUI(key.info, key.instructions ,key.keySprite);

        }

        private void OnMouseExit()
        {
            keySprite.color = spriteOriginalColor;
            GUITextManager.instance.SetActive(false);

        }

        private void BounceAnim()
        {
            LeanTween.scale(gameObject, new Vector3(1f, 1f, gameObject.transform.position.z) * bounceHeight, bounceDuration)
                .setEase(LeanTweenType.easeOutQuad)
                .setOnComplete(() =>
                {
                    LeanTween.scale(gameObject, Vector3.one, bounceDuration)
                    .setEase(LeanTweenType.easeInQuad)
                    .setOnComplete(BounceAnim);

                });
        }


    }
}
