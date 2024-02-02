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
        public GameObject puzzle;
        public bool inPuzzleTrigger = false;
        public bool inKeypadPuzzle = false;
        private void Awake()
        {
            input = new PlayerInputs();
            rb = GetComponent<Rigidbody2D>();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        private void OnEnable()
        {
            input.Enable();
            input.Player.Interact.performed += OnInteractPerformed;
            //input.Player.Interact.canceled += OnInteractCanceled;
        }

        private void FixedUpdate()
        {
            if (freezePlayer)
            {
                rb.velocity = Vector2.zero;
            }
            
        }

        private void OnInteractPerformed(InputAction.CallbackContext val)
        {
            // open puzzle and freeze player movement
            Debug.Log("interact pressed");
            if (inPuzzleTrigger)
            {
                inKeypadPuzzle = true;
                GameData.freezePlayer = true;
                //TODO
                //currently single ref game object => later have a function to
                // pass in the puzzle idx
                puzzle.SetActive(true);
                freezePlayer = true;
                Debug.Log("Puzzle Activated| in puuzzle? " + inKeypadPuzzle + "freeze player>?: " + freezePlayer + " Game data saved: " + GameData.freezePlayer);

            }
        }
    }
}
