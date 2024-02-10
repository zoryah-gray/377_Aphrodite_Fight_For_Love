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
        [Header("Keys Objects")]
        [SerializeField] private GameObject currentButton;
<<<<<<< Updated upstream
        public Button firstButton;
=======
        private Color originalBtnColor = Color.white;
        private Color flashColor = new Color(1f, 0.97f, 0.6f, 1f); //#FFF899
        public GameObject firstButton;
        public List<Button> keypadButtons;
<<<<<<< Updated upstream:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/KeyPadContoller.cs
>>>>>>> Stashed changes
=======



>>>>>>> Stashed changes:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/Scripts/KeyPadContoller.cs
        [Header("Code Tracker")]
        public int codeID = 0;
        //[SerializeField] private int[] code;
        [SerializeField] private List<int> code;
        [SerializeField] public Queue<int> codeQ = new Queue<int>();
<<<<<<< Updated upstream
        [SerializeField] private List<int> currentInput;
=======
        public List<int> currentInput;
<<<<<<< Updated upstream:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/KeyPadContoller.cs
=======



>>>>>>> Stashed changes:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/Scripts/KeyPadContoller.cs
        [Header("Puzzle Settings")]
        public float flashDuration = 0.25f;
        public float delayBtFlashes = 0.42f;
<<<<<<< Updated upstream:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/KeyPadContoller.cs
>>>>>>> Stashed changes
        [Header("Action Map")]
        [SerializeField] private ModifiedActionMap actionMapI;

        private void OnEnable()
        {
            if (actionMapI == null)
            {
                actionMapI = new ModifiedActionMap();
            }
<<<<<<< Updated upstream
            actionMapI.Player.Disable();
            actionMapI.UI.Enable();
            currentButton = EventSystem.current.currentSelectedGameObject;

            Debug.Log(InputSystem.FindControls("UI"));
=======
            actionMap.Player.Disable();
            actionMap.UI.Enable();
            EventSystem.current.SetSelectedGameObject(null);
            Initalize();

            //EventSystem.current.SetSelectedGameObject(firstButton);
            //currentButton = firstButton;
>>>>>>> Stashed changes
=======

        [Header("Puzzle Unlocks")]
        public GameObject unlocksObj;
        // event to unlock the door
        public delegate void UnlockHandler();
        public event UnlockHandler Unlocked;

        

        private void OnEnable()
        {
            
            EventSystem.current.SetSelectedGameObject(null);
            Initalize();

>>>>>>> Stashed changes:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/Scripts/KeyPadContoller.cs
        }

        private void OnDisable()
        {
<<<<<<< Updated upstream:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/KeyPadContoller.cs
<<<<<<< Updated upstream
            actionMapI.Player.Disable();
            actionMapI.UI.Enable();
=======
            actionMap.Player.Enable();
            actionMap.UI.Disable();
=======

>>>>>>> Stashed changes:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/Scripts/KeyPadContoller.cs
            StopAllCoroutines();
>>>>>>> Stashed changes
        }


        // Start is called before the first frame update
        void Start()
        {

<<<<<<< Updated upstream:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/KeyPadContoller.cs
            Debug.Log("In KeypadCtrler");
            //StartCoroutine(WaitForKeyCodesDictionary());
            //populateKeyCode();
            AddKeyCodeToDictionary();
            FlashPuzzleAnswer();
            //currentButton = EventSystem.current.currentSelectedGameObject;
            
=======
            currentInput.Clear();
            GameData.freezePlayer = false;
            GameData.inKeypadPuzzle = false;
            StopAllCoroutines();

            gameObject.SetActive(false);
>>>>>>> Stashed changes:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/Scripts/KeyPadContoller.cs
        }

        public void AddKeyPress(int keyID)
        {
            currentInput.Add(keyID);
        }

<<<<<<< Updated upstream
        private void StartSequenceHint(float duration)
=======
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


            }
            else
            {
                // all buttons flash green and set unlocked to true
                Debug.Log("Code is the same! ");
                FlashAllButtons(flashCount, flashColorCorrect);
<<<<<<< Updated upstream:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/KeyPadContoller.cs
=======
                currPuzzle.unlocked = true;
                Unlock();
                //UnlockObj();
                Invoke("OnReturn", 3.5f);
>>>>>>> Stashed changes:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/Scripts/KeyPadContoller.cs
            }
            currentInput.Clear();
        }

        private void FlashPuzzleAnswer()
>>>>>>> Stashed changes
        {
            

            foreach (var i in code)
            {
                
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
                        //btnImg.color = currentColor;
                    })
                    .setOnComplete(() =>
                    {
                        if (i == code[code.Count - 1])
                        {
                            EventSystem.current.SetSelectedGameObject(firstButton);
                            currentButton = firstButton;
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

<<<<<<< Updated upstream:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/KeyPadContoller.cs

        public void OnReturn()
        {
<<<<<<< Updated upstream
=======
            actionMap.Player.Enable();
            actionMap.UI.Disable();
            GameData.freezePlayer = false;
            GameData.inKeypadPuzzle = false;
            StopAllCoroutines();
>>>>>>> Stashed changes
            gameObject.SetActive(false);
        }

        
=======
        //private void UnlockObj()
        //{
        //    Vector3 originalScale = unlocksObj.transform.localScale;
        //    Vector3 originalPos = unlocksObj.transform.position;
        //    unlocksObj.GetComponent<SpriteRenderer>().color = Color.green;

        //    LeanTween.scaleX(unlocksObj, originalScale.x, 1f).setEase(LeanTweenType.easeOutQuint);
        //    LeanTween.moveX(unlocksObj, originalPos.x + originalScale.x, 1f).setEase(LeanTweenType.easeOutQuint);


        //}

        public void Unlock()
        {
            // door has been unlocked
            Unlocked?.Invoke();
        }

>>>>>>> Stashed changes:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/Scripts/KeyPadContoller.cs
        private void Initalize()
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

        private void AddKeyCodeToDictionary()
        {
            
            GameData.AddKeyCodeToDictionary(codeID, code);
            foreach (var c in code)
            {
                codeQ.Enqueue(c);
            }
        }


        private void populateKeyCode()
        {
            Debug.Log("populating codes for " + codeID);

            code = GameData.GetPuzzleCodeList(codeID);
            //GameData.keypadPuzzleCodesL[codeID] = codeL;
            foreach (var c in code)
            {
                codeQ.Enqueue(c);
            }

            Debug.Log(code.Count);
            //Debug.Log(codeL.Count);
            Debug.Log(codeQ.Count);
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
