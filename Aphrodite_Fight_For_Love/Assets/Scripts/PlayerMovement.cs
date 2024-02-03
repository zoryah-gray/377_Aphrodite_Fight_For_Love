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
        public bool isWalkingFront = false;
        public bool isWalkingHoriz = false;
        public bool isWalkingBack = false;
        public bool isRight = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S)) {
                GetComponent<Animator>().SetBool("isWalkingFront", true);
                isWalkingFront = true;
            }
            else if (Input.GetKeyUp(KeyCode.S)) {
                GetComponent<Animator>().SetBool("isWalkingFront", false);
                isWalkingFront = false;
            }
            if (Input.GetKeyDown(KeyCode.A)) {
                if (isRight)
                {
                    transform.localScale = new Vector3((-1) * transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    GetComponent<Animator>().SetBool("isWalkingHoriz", true);
                }
                isRight = false;
                isWalkingHoriz = true;
            }
            else if (Input.GetKeyUp(KeyCode.A)) {
                GetComponent<Animator>().SetBool("isWalkingHoriz", false);
                isWalkingHoriz = false;
            }
            if (Input.GetKeyDown(KeyCode.D)) {
                if (!isRight)
                {
                    transform.localScale = new Vector3((-1) * transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    GetComponent<Animator>().SetBool("isWalkingHoriz", true);
                }
                isRight = true;
                isWalkingHoriz = true;
            }
            else if (Input.GetKeyUp(KeyCode.D)) {
                GetComponent<Animator>().SetBool("isWalkingHoriz", false);
                isWalkingHoriz = false;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                GetComponent<Animator>().SetBool("isWalkingBack", true);
                isWalkingBack = true;
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                GetComponent<Animator>().SetBool("isWalkingBack", false);
                isWalkingBack = false;
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
