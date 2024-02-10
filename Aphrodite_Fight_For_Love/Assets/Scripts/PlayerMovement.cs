using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace AphroditeFightCode
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Player RB and Move Controls")]
        private PlayerInputs input = null;
        private Vector2 moveVector = Vector2.zero;
        private Rigidbody2D rb = null;
        private float moveSpeed = 4f;

        [Header("Attached Objects and References")]
        public Animator animator;
        public GameObject playerGO;
        public GameObject meleeBoxGO;

        [Header("Movement Animation Paramters")]
        public int directionInt = 0;
        public bool animAfterLeft = true;

        [Header("Collision Control")]
        public int collisionCount;
        public float collisionOffset = 0.5f;
        
        //public ContactFilter2D movementFilter;
        public ContactFilter2D movementFilter;
        List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

        [Header("Collision Control Attempt 2")]
        public float circleCastRadius = 5f;
        public List<RaycastHit2D> collisionRslts = new List<RaycastHit2D>();
        public ContactFilter2D castCollisions2;
        public Vector2 boxCastSize;
        public Vector3 boxOffset;


        private void Awake()
        {
            input = new PlayerInputs();
            rb = GetComponent<Rigidbody2D>();
            input.Player.Enable();

            // lock the cursor and turn it invisible
            // => in playMode if you want to click away press 'Esc'
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }


        private void OnEnable()
        {
            input.Player.Enable();
            input.Player.Movement.performed += OnMovementPerformed;
            input.Player.Movement.canceled += OnMovementCancelled;
        }
        private void OnDisable()
        {
            input.Player.Disable();
            input.Player.Movement.performed -= OnMovementPerformed;
            input.Player.Movement.canceled -= OnMovementCancelled;
        }
        private void FixedUpdate()
        {
            if (!GameData.freezePlayer)
            {
                rb.velocity = moveVector * moveSpeed;
                HandleMovementAnimBlendTree();
            }



        }




        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Vector3 startPosition = transform.position;
            Vector3 endPosition = transform.position + new Vector3(moveVector.x, moveVector.y, 0);
            Gizmos.DrawLine(startPosition, endPosition);

            Vector3 cubeCenter = endPosition + new Vector3(0, 0, collisionOffset * 0.5f);
            Vector3 cubeSize = new Vector3(collisionOffset, collisionOffset, collisionOffset);
            Gizmos.DrawWireCube(cubeCenter, cubeSize);
        }

        private void OnMovementPerformed(InputAction.CallbackContext val)
        {
            moveVector = val.ReadValue<Vector2>();
        }

        private void OnMovementCancelled(InputAction.CallbackContext val)
        {
            moveVector = Vector2.zero;
        }



        private void HandleMovementAnimBlendTree()
        {
            if (moveVector != Vector2.zero)
            {
                animator.SetFloat("Horizontal", moveVector.x);
                animator.SetFloat("Vertical", moveVector.y);

                if (moveVector.x < 0.01f)
                {
                    // moving left
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    // moving right
                    transform.localScale = new Vector3((-1) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }


                if (animator.GetFloat("Vertical") == 1)
                {
                    // moving up
                    directionInt = 1;
                }
                else if (animator.GetFloat("Vertical") == -1)
                {
                    // moving down
                    directionInt = 3;
                }


                if (animator.GetFloat("Horizontal") == 1)
                {
                    // move right
                    animAfterLeft = false;
                    directionInt = 2;
                }
                else if (animator.GetFloat("Horizontal") == -1)
                {
                    // move left
                    animAfterLeft = true;
                    directionInt = 4;
                }


            }
            animator.SetFloat("Speed", moveVector.sqrMagnitude);
        }
    }
}







    