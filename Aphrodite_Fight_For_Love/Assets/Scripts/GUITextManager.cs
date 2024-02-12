using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AphroditeFightCode
{
    public class GUITextManager : MonoBehaviour
    {
        // THIS IS A SINGLETON CLASS - THERE SHOULD ONLY BE ONE IN A SCENE!!
        public static GUITextManager instance { get; private set; }
        [Header("GUI Elements")]
        public GameObject GUITextBox;
        public TMP_Text GUIText;
        public TMP_Text instructions;
        public Image GUIImg;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }


        public void PrintToGUI(string text, string textInstr, Sprite image)
        {
            SetActive(true);
            GUIText.text = text;
            instructions.text = textInstr;
            GUIImg.sprite = image;

        }

        public void SetActive(bool active)
        {
            if (active)
            {
                GUITextBox.SetActive(true);

            }
            else
            {
                GUITextBox.SetActive(false);
            }
        }
    }
}
