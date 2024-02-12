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




        private void Awake()
        {
            keySprite = GetComponent<SpriteRenderer>();
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
        }

        void OnMouseOver()
        {
            //change the alpha/color of object
            
            keySprite.color = hoverColor;
            GUITextManager.instance.PrintToGUI(key.info, key.keySprite);

        }

        private void OnMouseExit()
        {
            keySprite.color = spriteOriginalColor;
            GUITextManager.instance.SetActive(false);

        }


    }
}
