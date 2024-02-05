using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine;

namespace AphroditeFightCode
{
    public class KeyPadContoller : MonoBehaviour
    {
        [Header("Button Objects")]
        [SerializeField] private GameObject currentButton;
<<<<<<< Updated upstream
        public GameObject firstButton;
        public Button[] buttons;
=======
  
        public Button firstButton;
     
        private Color originalBtnColor = Color.white;
        private Color flashColor = new Color(1f, 0.97f, 0.6f, 1f); //#FFF899
        
        public List<Button> keypadButtons;
        
>>>>>>> Stashed changes
        [Header("Code Tracker")]
//        [SerializeField] private int idx = 0;
        public int codeID = 0;
        //[SerializeField] private int[] code;
        [SerializeField] private List<int> code;
        [SerializeField] public Queue<int> codeQ = new Queue<int>();
<<<<<<< Updated upstream
        public List<int> currentInput;
        [Header("Action Map")]
        [SerializeField] private ModifiedActionMap actionMapI;
        [SerializeField] private PlayerInputs actionMap = null;

        private void OnEnable()
        {
            //if (actionMapI == null)
            //{
            //    actionMapI = new ModifiedActionMap();
            //}
            //actionMapI.Player.Disable();
            //actionMapI.UI.Enable();
            //currentButton = EventSystem.current.currentSelectedGameObject;

            if (actionMap == null)
            {
                actionMap = new PlayerInputs();
            }
            actionMap.Player.Disable();
            actionMap.UI.Enable();
            EventSystem.current.SetSelectedGameObject(firstButton);
            currentButton = firstButton;
            Debug.Log(InputSystem.FindControls("UI"));
=======
  
        
     
        public List<int> currentInput;
        [Header("Puzzle Settings")]
        public float flashDuration = 0.25f;
        public float delayBtFlashes = 0.42f;
        
        [Header("Action Map")]
        [SerializeField] private ModifiedActionMap actionMap;

        private void OnEnable()
        {
            if (actionMap == null)
            {
                actionMap = new ModifiedActionMap();
            }
  
            currentButton = EventSystem.current.currentSelectedGameObject;

          
     
            actionMap.Player.Disable();
            actionMap.UI.Enable();
            EventSystem.current.SetSelectedGameObject(null);
            Initalize();

            //EventSystem.current.SetSelectedGameObject(firstButton);
            //currentButton = firstButton;
        
>>>>>>> Stashed changes
        }

        private void OnDisable()
        {
<<<<<<< Updated upstream
            actionMap.Player.Enable();
            actionMap.UI.Disable();
=======
     
            actionMap.Player.Enable();
            actionMap.UI.Disable();
            StopAllCoroutines();
        
>>>>>>> Stashed changes
        }


        // Start is called before the first frame update
        void Start()
        {

            Debug.Log("In KeypadCtrler");
            //StartCoroutine(WaitForKeyCodesDictionary());
            //populateKeyCode();
            AddKeyCodeToDictionary();

            currentButton = EventSystem.current.currentSelectedGameObject;
            
        }

        // Update is called once per frame
        void Update()
        {

            while (codeQ.Count > 0)
            {
                Debug.Log(codeQ.Dequeue());
            }

        }

        public void AddKeyPress(int keyID)
        {
            currentInput.Add(keyID);
        }

<<<<<<< Updated upstream
=======
  
        private void StartSequenceHint(float duration)
        {
            return;
        }
     
>>>>>>> Stashed changes
        public void SubmitPattern()
        {
            // evaluate if the pattern and code are the same
            if (currentInput != code)
            {
                Debug.Log("Code is not the same! " + currentInput + " != " + code);
            }
            currentInput.Clear();
        }

<<<<<<< Updated upstream
        private void StartSequenceHint(float duration)
        {
            return;
=======
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
            
>>>>>>> Stashed changes
        }

        private void ResetCode()
        {
            return;
        }

        public void OnReturn()
        {
<<<<<<< Updated upstream
=======
  
     
>>>>>>> Stashed changes
            actionMap.Player.Enable();
            actionMap.UI.Disable();
            GameData.freezePlayer = false;
            GameData.inKeypadPuzzle = false;
<<<<<<< Updated upstream
=======
            StopAllCoroutines();
        
>>>>>>> Stashed changes
            gameObject.SetActive(false);
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
