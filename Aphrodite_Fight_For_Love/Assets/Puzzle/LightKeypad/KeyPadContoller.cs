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
        public Button firstButton;
        [Header("Code Tracker")]
        [SerializeField] private int idx = 0;
        public int codeID = 0;
        //[SerializeField] private int[] code;
        [SerializeField] private List<int> code;
        [SerializeField] public Queue<int> codeQ = new Queue<int>();
        [SerializeField] private List<int> currentInput;


        // Start is called before the first frame update
        void Start()
        {

            Debug.Log("In KeypadCtrler");
            //StartCoroutine(WaitForKeyCodesDictionary());
            //populateKeyCode();
            AddKeyCodeToDictionary();
            Debug.Log(GameData.keypadPuzzleCodes[0].Count);

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
