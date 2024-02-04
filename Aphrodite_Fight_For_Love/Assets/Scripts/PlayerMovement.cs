using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AphroditeFightCode
{
    public class PlayerMovement : MonoBehaviour
    {
        // Start is called before the first frame update
        private PlayerInputs input = null;
        private Vector2 moveVector = Vector2.zero;
        private Rigidbody2D rb = null;
        private float moveSpeed = 3f;
        public Animator animator;
        public GameObject playerGO;
        public GameObject meleeBoxGO;

        public bool isFacingRight = false;
        public bool isFacingLeft = false;
        public bool isFacingFront = false;
        public bool isFacingBack = false;
        public bool isRight = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                GetComponent<Animator>().SetBool("isWalkingFront", true);
            }
            else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) {
                GetComponent<Animator>().SetBool("isWalkingFront", false);
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                GetComponent<Animator>().SetBool("isWalkingHoriz", true);
                if(transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) {
                GetComponent<Animator>().SetBool("isWalkingHoriz", false);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                GetComponent<Animator>().SetBool("isWalkingHoriz", true);
                if (transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3((-1) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            }
            else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) {
                GetComponent<Animator>().SetBool("isWalkingHoriz", false);
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                GetComponent<Animator>().SetBool("isWalkingBack", true);
            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                GetComponent<Animator>().SetBool("isWalkingBack", false);
            }
        }

        private void Awake()
        {
            input = new PlayerInputs();
            rb = GetComponent<Rigidbody2D>();
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
            }
        }
        private void OnMovementPerformed(InputAction.CallbackContext val)
        {
            moveVector = val.ReadValue<Vector2>();
        }
        private void OnMovementCancelled(InputAction.CallbackContext val)
        {
            moveVector = Vector2.zero;
        }
    }
}
