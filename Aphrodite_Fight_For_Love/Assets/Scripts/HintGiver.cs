using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AphroditeFightCode
{
    public class HintGiver : MonoBehaviour
    {
        [Header("Dialouge")]
        [SerializeField] private GameObject dialogueManager;
        public List<string> dialogueList = new List<string>();
        public List<string> speakerList = new List<string>();
        public List<Sprite> speakerSprites = new List<Sprite>();
        public GameObject talkIcon;
        public bool inTrigger = false;

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
            Debug.Log("puzzle interact Click");
            if (inTrigger)
            {
                StartDialouge();
                inTrigger = false;

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.name == "Player")
            {
                talkIcon.SetActive(true);
                inTrigger = true;

            }
        }

        

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                talkIcon.SetActive(false);
                inTrigger = false;

            }
        }

        public void StartDialouge()
        {
            //List<Sprite> speakers = new List<Sprite>();
            //speakers.Add(quest.questSprite);
            dialogueManager.SetActive(true);
            dialogueManager.GetComponent<DialogueManager>().ReceiveStartReadyDialogue(dialogueList.ToArray(), speakerList.ToArray(), speakerSprites);
        }
    }
}
