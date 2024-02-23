using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace AphroditeFightCode
{
    public class PuzzleEnter : MonoBehaviour
    {
        

        [Header("PuzzleIcon")]
        public GameObject icon;

        [Header("Puzzle SO")]
        public KeypadPuzzleTriggerSO puzzle;

        [Header("Puzzle UI Controller")]
        public KeyPadContoller puzzleCtrlScript;
        public GameObject keypadUI;

        [Header("Puzzle Unlocks Obj/Door")]
        public GameObject door;

        [Header("Inputs")]
        //private PlayerInputs input = null;
        public bool inTrigger = false;

        private void Awake()
        {
            //input = new PlayerInputs();
        }

        private void OnEnable()
        {
            PlayerInputsSingleton.PlayerInputsInstance.Player.Interact.performed += OnInteractPerformed;

            //if (!input.Player.enabled)
            //{
            //    input.Player.Enable();

            //}
            //input.Player.Interact.performed += OnInteractPerformed;
            //input.Player.Enable();
            //input.Player.Interact.performed += OnInteractPerformed;
        }
        private void OnDisable()
        {
            PlayerInputsSingleton.PlayerInputsInstance.Player.Interact.performed -= OnInteractPerformed;

            //if (input.Player.enabled)
            //{
            //    input.Player.Disable();
            //}
            //input.Player.Interact.performed -= OnInteractPerformed;

            //input.Player.Disable();
            //input.Player.Interact.performed -= OnInteractPerformed;
            StopAllCoroutines();
        }

        private void OnInteractPerformed(InputAction.CallbackContext val)
        {
            Debug.Log("puzzle interact Click");
            if (inTrigger)
            {
                GameData.currKeypadPuzzle = puzzle;
                GameData.inKeypadPuzzle = true;
                GameData.freezePlayer = true;
                Debug.Log(puzzleCtrlScript.currPuzzle.name + " | gameData = " + GameData.currKeypadPuzzle.name);

                keypadUI.SetActive(true);
                

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            puzzleCtrlScript.currPuzzle = puzzle;
            GameData.currKeypadPuzzle = puzzle;
            if (collision.gameObject.name == "Player")
            {
                icon.SetActive(true);
                inTrigger = true;
                //collision.gameObject.GetComponent<PlayerKeypadPuzzleController>().inPuzzleTrigger = true;
                //puzzleCtrlScript.currPuzzle = puzzle;
                Debug.Log(puzzleCtrlScript.currPuzzle.name + " | gameData = " + GameData.currKeypadPuzzle.name);

            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            GameData.currKeypadPuzzle = puzzle;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                icon.SetActive(false);
                inTrigger = false;
                //collision.gameObject.GetComponent<PlayerKeypadPuzzleController>().inPuzzleTrigger = false;
                //puzzleCtrlScript.currPuzzle = ;
                Debug.Log("exiting trigger area | " + puzzleCtrlScript.currPuzzle.name + " | gameData = " + GameData.currKeypadPuzzle.name);

            }
        }
    }
}
