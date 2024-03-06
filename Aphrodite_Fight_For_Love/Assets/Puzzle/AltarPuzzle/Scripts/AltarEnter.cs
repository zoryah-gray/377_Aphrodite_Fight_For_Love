using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace AphroditeFightCode
{
    public class AltarEnter : MonoBehaviour
    {
        [Header("PuzzleIcon")]
        public GameObject icon;

        [Header("Altar GO")]
        public GameObject altarGO;

        [Header("Altar Script")]
        public DragAndDropManager altarManagerScript;


        [Header("Unlocks Obj/Door")]
        public GameObject door;


        [Header("Inputs")]
        //private PlayerInputs input = null;
        public bool inTrigger = false;

        
        private void Awake()
        {
            //input = new PlayerInputs();
            Intialize();
        }


        private void Intialize()
        {
            if (altarManagerScript != null)
            {
                return;
            }
            else
            {
                Debug.LogWarning("An Altar DragAndDropmanager object has not been assigned");
            }
        }

        private void OnEnable()
        {
            PlayerInputsSingleton.PlayerInputsInstance.Player.Interact.performed += OnInteractPerformed;

        }
        private void OnDisable()
        {
            PlayerInputsSingleton.PlayerInputsInstance.Player.Interact.performed -= OnInteractPerformed;


            StopAllCoroutines();
        }

        private void OnInteractPerformed(InputAction.CallbackContext val)
        {
            if (inTrigger)
            {
                GameData.freezePlayer = true;
                altarGO.SetActive(true);

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if (collision.gameObject.name == "Player")
            {
                icon.SetActive(true);
                inTrigger = true;

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
           
            if (collision.gameObject.name == "Player")
            {
                icon.SetActive(false);
                inTrigger = false;

            }
        }
    }
}
