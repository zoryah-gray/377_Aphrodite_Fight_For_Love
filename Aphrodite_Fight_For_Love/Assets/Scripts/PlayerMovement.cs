using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace AphroditeFightCode
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Player RB and Move Controls")]
        private PlayerInputs input = null;
        private Vector2 moveVector = Vector2.zero;
        private Rigidbody2D rb = null;
        private float moveSpeed = 3f;

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
        public ContactFilter2D movementFilter;
        List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

        private void Awake()
        {
            input = new PlayerInputs();
            rb = GetComponent<Rigidbody2D>();
            input.Player.Enable();

            // lock the cursor and turn it invisible
            // => in playMode if you want to click away press 'Esc'
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {

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
                if (CanMove(moveVector))
                {
                    rb.velocity = moveVector * moveSpeed;
                    //HandleMovementAnim();

                    // I got bored and wanted to procrasinate so I tried condensing/simplifying
                    // your movement statements into blend trees; if you want to see how it works
                    // uncomment the function on the line below and then
                    // go into animator and set IdleTree to be the default state
                    // n if you have any questions (even if it is Why?) let me know :D - Z

                    // Z, ur incredible, thank you! -Rch

                    HandleMovementAnimBlendTree();
                }
                else
                {
                    // try moving left/right
                    if (CanMove(new Vector2(moveVector.x, 0)))
                    {
                        // can move l/r
                        rb.velocity = new Vector2(moveVector.x, 0) * moveSpeed;
                    }
                    else if (CanMove(new Vector2(0, moveVector.y)))
                    {
                        //can move up/down
                        rb.velocity = new Vector2(0, moveVector.y) * moveSpeed;
                    }

                }

            }
        }



        public bool CanMove(Vector2 dir)
        {
            //check for obstacles
            collisionCount = rb.Cast(dir, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (collisionCount == 0)
            {
                // no collisions detected
                return true;
            }
            else
            {
                foreach (RaycastHit2D hit2D in castCollisions)
                {
                    Debug.Log("colliding with: " + hit2D.ToString());
                }
                //return false;
                return true;
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







    