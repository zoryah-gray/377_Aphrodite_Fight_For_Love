using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace AphroditeFightCode
{
    public class PlayerKeypadPuzzleController : MonoBehaviour
    {
        private PlayerInputs input = null;
        private Rigidbody2D rb;
        public bool freezePlayer = false;
        [Header("PuzzleUI")]
        public bool inPuzzleTrigger = false;
        public bool inKeypadPuzzle = false;
        public GameObject keypadUI;
        public bool inQuestTrigger = false;


        private void Awake()
        {
            input = new PlayerInputs();
            rb = GetComponent<Rigidbody2D>();
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }
        private void OnEnable()
        {
            input.Player.Enable();
            input.Player.Interact.performed += OnInteractPerformed;
            //input.Player.Interact.canceled += OnInteractCanceled;
        }
        private void OnDisable()
        {
            input.Player.Disable();
            input.Player.Interact.performed -= OnInteractPerformed;
        }

            //private void FixedUpdate()
            //{
            //    if (freezePlayer)
            //    {
            //        rb.velocity = Vector2.zero;
            //    }

            //}

        private void OnInteractPerformed(InputAction.CallbackContext val)
        {
            // open puzzle and freeze player movement
            Debug.Log("Plyr interact pressed");
            if (inPuzzleTrigger)
            {
                
                GameData.inKeypadPuzzle = true;
                GameData.freezePlayer = true;
                inPuzzleTrigger = false;

                keypadUI.SetActive(true);
                Debug.Log("Puzzle Activated| in puzzle? " + inKeypadPuzzle + "freeze player>?: " + freezePlayer + " Game data saved: " + GameData.freezePlayer);

            }
            if (inQuestTrigger)
            {
                GameData.startingQuestActions = true;
            }
        }
    }
}
