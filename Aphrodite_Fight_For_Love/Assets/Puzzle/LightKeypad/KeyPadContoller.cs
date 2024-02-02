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
        [Header("Keys Objects")]
        [SerializeField] private GameObject currentButton;
        public GameObject firstButton;
        [Header("Code Tracker")]
        [SerializeField] private int idx = 0;
        public int codeID = 0;
        //[SerializeField] private int[] code;
        [SerializeField] private List<int> code;
        [SerializeField] public Queue<int> codeQ = new Queue<int>();
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
        }

        private void OnDisable()
        {
            actionMap.Player.Disable();
            actionMap.UI.Enable();
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

        private void StartSequenceHint(float duration)
        {
            return;
        }

        private void ResetCode()
        {
            return;
        }

        public void OnReturn()
        {
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
