using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class PlayerMovement : MonoBehaviour
    {
        // Start is called before the first frame update
        private PlayerInputs input = null;
        private void Awake()
        {
            input = new PlayerInputs();
        }
        private void OnEnable()
        {
            input.Enable();
        }
        private void OnDisable()
        {
            input.Disable();
        }
        private void OnAnimatorMove()
        {
            
        }
    }
}
