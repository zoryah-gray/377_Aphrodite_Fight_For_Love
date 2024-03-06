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
        [Header("GUI Menu Elements")]
        public GameObject menu;


        [Header("GUI Quest Bar Elements")]
        public GameObject questCanvas;
        public GameObject questBar;
        public Image questBarIcon;
        public TMP_Text questBarText;

        [Header("GUI Quest Side Header Elements")]
        //public GameObject questHeader;
        public TMP_Text sideQuestBarText;

        public int keyTotal;
        public int keyCollected;
        public float popDuration = 0.5f;
        public float popScale = 1.2f;

        [Header("GUI Elements Temp Dialouge")]
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

        public void ToggleMenu()
        {
            if (menu.activeSelf)
            {
                // menu open => close it
                menu.SetActive(false);
            }
            else
            {
                //menu not open => open it
                menu.SetActive(true);
            }
        }

        public void InitalizeQuestBar(int total, Sprite img, string questGoal)
        {
            questCanvas.SetActive(true);
            if (!menu.activeSelf)
            {
                // quest bar not open => open it
                
                sideQuestBarText.text = questGoal;
                //questBar.SetActive(true);
                RectTransform questBarRT = questBar.GetComponent<RectTransform>();
                Vector3 originalPos = questBar.transform.position;
                questBarRT.localScale = Vector3.zero;

                LeanTween.scale(questBarRT, Vector3.one * popScale, popDuration)
                    .setEase(LeanTweenType.easeOutBack)
                    .setOnStart(() => questBar.SetActive(true)) // Activate the UI element at the start of the animation
                    .setOnComplete(() => {
                        questBar.SetActive(true);
                        questBar.transform.position = originalPos;
                       });

                
            }
            keyTotal = total;
            keyCollected = 0;
            string format = keyCollected.ToString() + " / " + keyTotal.ToString();
            questBarText.text = format;
            questBarIcon.sprite = img;
        }

        public void UpdateQuestBar()
        {
            //public GameObject questBar;
            //public Image questBarIcon;
            //public TMP_Text questBarText;
            
            keyCollected += 1;
            string format = keyCollected.ToString() + " / " + keyTotal.ToString();
            questBarText.text = format;

        }

        public void DeactivateQuestBar()
        {
            float flashDuration = 0.4f;
            RectTransform questBarRT = questBar.GetComponent<RectTransform>();

            LeanTween.value(questBar, Color.white, Color.green, flashDuration)
            .setEase(LeanTweenType.easeInOutQuad)
            .setLoopPingPong(2)
            .setOnUpdate((Color val) =>
            {
                // Update the color of the UI element during the tween
                questBarIcon.color = val;
                questBarText.color = val;
            });
            sideQuestBarText.text = "None";

            // Pop out
            LeanTween.scale(questBarRT, new Vector3(popScale, popScale, 1f), popDuration)
                .setEase(LeanTweenType.easeOutBack)
                .setDelay(flashDuration * 2)  // Delay to start popping after the flashes
                .setOnComplete(() =>
                {
                    questBar.SetActive(false);
                    questCanvas.SetActive(false);
                });
        }
    }
}
