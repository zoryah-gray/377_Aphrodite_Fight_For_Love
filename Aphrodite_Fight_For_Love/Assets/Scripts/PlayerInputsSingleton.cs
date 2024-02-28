using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace AphroditeFightCode
{
    public class PlayerInputsSingleton : MonoBehaviour
    {
        private static PlayerInputsSingleton instance;
        public static PlayerInputs PlayerInputsInstance { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                //DontDestroyOnLoad(gameObject);

                // Initialize the PlayerInputs instance
                PlayerInputsInstance = new PlayerInputs();
                PlayerInputsInstance.Enable();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
