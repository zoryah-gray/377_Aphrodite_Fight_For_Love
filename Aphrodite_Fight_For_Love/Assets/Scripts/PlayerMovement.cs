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
        private float moveSpeed = 10f;
        private void Awake()
        {
            input = new PlayerInputs();
            rb = GetComponent<Rigidbody2D>();
        }
        private void OnEnable()
        {
            input.Enable();
            input.Player.Movement.performed += OnMovementPerformed;
            input.Player.Movement.canceled += OnMovementCancelled;
        }
        private void OnDisable()
        {
            input.Disable();
            input.Player.Movement.performed -= OnMovementPerformed;
            input.Player.Movement.canceled -= OnMovementCancelled;
        }
        private void FixedUpdate()
        {
            rb.velocity = moveVector * moveSpeed;
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
