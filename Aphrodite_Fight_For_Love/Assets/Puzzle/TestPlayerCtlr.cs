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
        
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            
            if (movementInput != Vector2.zero)
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
            Debug.Log("on move");
            movementInput = movementValue.Get<Vector2>();
        }

        public void OnInteract(InputValue inputV)
        {
            Debug.Log("interact pressed");
            if (inPuzzleTrigger)
            {
                Debug.Log("activate puzzle");
                puzzle.SetActive(true);
            }
        }
    }
}
