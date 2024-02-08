using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine;
//using LeanTween;

namespace AphroditeFightCode
{
    public class KeyPadContoller : MonoBehaviour
    {

        [Header("Current Puzzle")]
        public KeypadPuzzleTriggerSO currPuzzle;

        [Header("Keys Objects")]
        [SerializeField] private GameObject currentButton;
  
        public Button firstButton;
     
        private Color originalBtnColor = Color.white;
        private Color flashColor = new Color(1f, 0.97f, 0.6f, 1f); //#FFF899
        
        public List<Button> keypadButtons;
        
        [Header("Code Tracker")]
        public int codeID = 0;
        //[SerializeField] private int[] code;
        [SerializeField] private List<int> code;

        public List<int> currentInput;

        [Header("Puzzle Settings")]
        public float puzzleStartDelay = 1.5f;
        public float flashDuration = 0.25f;
        public float delayBtFlashes = 0.42f;

        [Header("Puzzle Unlocks")]
        public GameObject unlocksObj;

        [Header("Action Map")]
        [SerializeField] private ModifiedActionMap actionMap;

        [Header("Input Mappings - Syncing")]
        private PlayerInputs input = null;

        private void OnEnable()
        {
            // syncing to playerinputs mapping
            if (input == null)
            {
                input = new PlayerInputs();
            }
  
            currentButton = EventSystem.current.currentSelectedGameObject;

            // syncing to playerinputs mapping
            input.Player.Disable();
            input.UI.Enable();
            Debug.Log("ui enabled?: " + input.UI.enabled + " | player enabled?: " + input.Player.enabled);

            EventSystem.current.SetSelectedGameObject(null);
            Initalize();

            //EventSystem.current.SetSelectedGameObject(firstButton);
            //currentButton = firstButton;

        }

        private void OnDisable()
        {
     
            //actionMap.Player.Enable();
            //actionMap.UI.Disable();

            // syncing to playerinputs mapping
            input.Player.Enable();
            input.UI.Disable();

            StopAllCoroutines();
        
        }

        public void OnReturn()
        {


            //actionMap.Player.Enable();
            //actionMap.UI.Disable();

            // syncing to playerinputs mapping
            input.Player.Enable();
            input.UI.Disable();

            currentInput.Clear();
            GameData.freezePlayer = false;
            GameData.inKeypadPuzzle = false;
            StopAllCoroutines();

            gameObject.SetActive(false);
        }



        public void AddKeyPress(int keyID)
        {
            currentInput.Add(keyID);
        }

  
     
        public void SubmitPattern()
        {
            int flashCount = 3;
            Color flashColorCorrect = new Color(0f, 1f, 0.0157f, 1f); // #00FF04 => green
            Color flashColorIncorrect = new Color(1f, 0.3f, 0.23f, 1f); // #FF4C3B => red
            // evaluate if the pattern and code are the same
            Debug.Log("code count " + code.Count + " | " + currentInput.Count);
            if (currentInput.Equals(code) || currentInput.Count != code.Count)
            {
                Debug.Log("Code is not the same! " + currentInput + " != " + code);
                // all buttons flash red
                FlashAllButtons(flashCount, flashColorIncorrect);
                Invoke("FlashPuzzleAnswer", 1.5f);

            }
            else
            {

                // all buttons flash green and set unlocked to true
                Debug.Log("Code is the same/correct!");
                FlashAllButtons(flashCount, flashColorCorrect);
                currPuzzle.unlocked = true;
                UnlockObj();
                Invoke("OnReturn", 3.5f);
            }
            currentInput.Clear();
        }

        private void FlashPuzzleAnswer()
        
        {
            

            foreach (var i in code)
            {
                Debug.Log(i + " | " + keypadButtons[i - 1].gameObject.name);
                Image btnImg = keypadButtons[i - 1].GetComponent<Image>();


                LeanTween.value(btnImg.gameObject, 1f, 0.35f, flashDuration)
                    .setEase(LeanTweenType.easeInOutQuad)
                    .setLoopPingPong(1)
                    .setDelay(i * (flashDuration + delayBtFlashes))
                    .setOnUpdate((float alpha) =>
                    {
                        // Update button color alpha
                        Color currentColor = btnImg.color;
                        currentColor.a = alpha;
                        btnImg.color = currentColor;
                    })
                    .setOnComplete(() =>
                    {
                        if (i == code[code.Count - 1])
                        {
                            EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
                            currentButton = firstButton.gameObject;
                        }

                        
                    });
            }
            
        }

        private void FlashAllButtons(int flashCount, Color btnFlashColor)
        {
            for (int i = 0; i < flashCount; i++)
            {
                foreach (Button button in keypadButtons)
                {
                    Image buttonImage = button.GetComponent<Image>();

                    // Flash animation using LeanTween
                    LeanTween.value(buttonImage.gameObject, originalBtnColor, btnFlashColor, flashDuration)
                        .setEase(LeanTweenType.easeInOutQuad)
                        .setOnUpdate((Color color) =>
                        {
                                // Update button color during the flash
                                buttonImage.color = color;
                        })
                        .setOnComplete(() =>
                        {
                                // Return to the original color after the flash
                                LeanTween.value(buttonImage.gameObject, btnFlashColor, originalBtnColor, flashDuration)
                                        .setEase(LeanTweenType.easeInOutQuad)
                                        .setOnUpdate((Color color) =>
                                {
                                        // Update button color
                                        buttonImage.color = color;
                                });

                               
                        })
                        .setDelay(i * (flashDuration + delayBtFlashes));
                }
            }
        }

        private void UnlockObj()
        {
            float targetScaleXPos = 0.27f;
            Vector3 originalScale = unlocksObj.transform.localScale;
            Vector3 originalPos = unlocksObj.transform.position;
            unlocksObj.GetComponent<SpriteRenderer>().color = Color.green;
            //LeanTween.move(unlocksObj, new Vector3(2.664f, -0.4138f, 0f), 1f)
            //    .setEase(LeanTweenType.easeOutQuad);
            //LeanTween.value(unlocksObj, unlocksObj.transform.localScale.x, targetScaleXPos, 1.5f)
            //    .setEase(LeanTweenType.easeOutBounce)
            //    .setOnUpdate((float val) =>
            //        {
            //            Vector3 newScale = unlocksObj.transform.localScale;
            //            newScale.x = val;
            //            unlocksObj.transform.localScale = newScale;
            //        });

            LeanTween.scaleX(unlocksObj, originalScale.x, 1f).setEase(LeanTweenType.easeOutQuint);
            LeanTween.moveX(unlocksObj, originalPos.x + originalScale.x, 1f).setEase(LeanTweenType.easeOutQuint);


        }



        private void Initalize()
        {
            if (keypadButtons.Count == 0)
            {
                foreach (Transform child in transform)
                {
                    if (child.TryGetComponent<Button>(out Button btn))
                    {
                        //Debug.Log(child.gameObject.name + " | " + btn.gameObject.name);
                        keypadButtons.Add(btn);
                    }
                }
            }
            // assign all the variables from the curr Scriptable Obj to their respective variables
            code = currPuzzle.code;
            codeID = currPuzzle.codeID;
            unlocksObj = currPuzzle.unloackableObj;
            
            AddKeyCodeToDictionary();
            //FlashPuzzleAnswer();
            Invoke("FlashPuzzleAnswer", puzzleStartDelay);
            
        }

        private void AddKeyCodeToDictionary()
        {
            
            GameData.AddKeyCodeToDictionary(codeID, code);
        }


        private void populateKeyCode()
        {
            Debug.Log("populating codes for " + codeID);

            code = GameData.GetPuzzleCodeList(codeID);

            //Debug.Log(code.Count);
        }

        IEnumerator WaitForKeyCodesDictionary()
        {
            while (GameData.keypadPuzzleCodes.Count == 0)
            {
                Debug.Log("still waiting " + GameData.keypadPuzzleCodes.Count);
                yield return null;
            }
            populateKeyCode();
        }
    }
}
