using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AphroditeFightCode
{
    public class TestPlayerCtlr : MonoBehaviour
    {
        Vector2 movementInput;
        Rigidbody2D rb;
        public float speed = 4f;
        public float collisionOffset = 0.05f;
        public ContactFilter2D movementFilter;
        List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
        [Header("PuzzleUI")]
        public GameObject puzzle;
        public bool inPuzzleTrigger = false;
        public bool inPuzzle = false;
        public bool freezePlayer = false;

        [Header("Action Map")]
        [SerializeField] InputActionAsset actionMap;
        



        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            //Debug.Log(actionMap.controlSchemes.ToString());
            //Debug.Log(actionMap.enabled);
            //actionMap.FindAction("Player");
            //foreach (var i in actionMap.actionMaps)
            //{
            //    Debug.Log(i);
            //}
        }

        private void FixedUpdate()
        {
            
            if (movementInput != Vector2.zero && !freezePlayer)
            {
                //check for obstacles
                int count = rb.Cast(movementInput, movementFilter, castCollisions, speed * Time.fixedDeltaTime + collisionOffset);
                if (count == 0)
                {
                    rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movementInput);
                }
            }
        }


        public void OnMove(InputValue movementValue)
        {
            movementInput = movementValue.Get<Vector2>();
        }

        public void OnInteract(InputValue inputV)
        {
            Debug.Log("interact pressed");
            if (inPuzzleTrigger)
            {
                Debug.Log("Puzzle Activated");
                inPuzzle = true;
                puzzle.SetActive(true);
                freezePlayer = true;
            }
        }
    }
}
