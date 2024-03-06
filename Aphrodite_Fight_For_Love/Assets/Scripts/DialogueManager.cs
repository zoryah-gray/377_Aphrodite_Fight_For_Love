using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace AphroditeFightCode
{
    public class DialogueManager : MonoBehaviour
    {
        // Start is called before the first frame update
        //[SerializeField] float typeSpeed = 0.2f;
        //[Header("Action Map Controls")]
        //private PlayerInputs input = null;
        [Header("Dialouge Inputs")]
        TextMeshProUGUI[] dialogueBoxArray;
        [SerializeField] private GameObject player;
        TextMeshProUGUI dialogueText;
        TextMeshProUGUI speakerName;
        public string[] dialogueArray;
        public string[] speakerArray;
        public List<Sprite> SpeakerSprite;
        int dIndex;
        public Image speaker1;
        public Image speaker2;

        [Header("Audio Files")]
        public AudioClip progressClip;

        Color inactiveAlpha;
        Color activeAlpha;

        void Setup()
        {
            //Gather the text mesh pros used to display the text to the player
            dIndex = 0;
            dialogueBoxArray = GetComponentsInChildren<TextMeshProUGUI>();
            speakerName = dialogueBoxArray[0];
            dialogueText = dialogueBoxArray[1];
            //Image[] spriteArray = GetComponentsInChildren<Image>();
            inactiveAlpha = speaker1.color;
            inactiveAlpha.a = 0.25f;
            activeAlpha = speaker1.color;
            speaker1.color = activeAlpha;
            GameData.freezePlayer = true;

            //speaker1 = spriteArray[0];
            //speaker2 = spriteArray[1];

            //ReceiveStartReadyDialogue(dialogueArray, speakerArray);


            //player.SetActive(false);

        }

        private void Awake()
        {
            //input = new PlayerInputs();
            //input.Dialouge.Enable();
        }

        private void OnEnable()
        {
            PlayerInputsSingleton.PlayerInputsInstance.Dialouge.Enable();
            PlayerInputsSingleton.PlayerInputsInstance.Player.Disable();
            PlayerInputsSingleton.PlayerInputsInstance.Dialouge.Next.performed += OnNextPerformed;
            GameData.freezePlayer = true;
            //input.Dialouge.Enable();
            //input.Player.Disable();
            //input.Dialouge.Next.performed += OnNextPerformed;
        }
        private void OnDisable()
        {
            
            PlayerInputsSingleton.PlayerInputsInstance.Dialouge.Disable();
            PlayerInputsSingleton.PlayerInputsInstance.Player.Enable();
            PlayerInputsSingleton.PlayerInputsInstance.Dialouge.Next.performed -= OnNextPerformed;

            //input.Dialouge.Disable();
            //input.Player.Enable();
            //input.Dialouge.Next.performed -= OnNextPerformed;
        }

        //private void OnDestroy()
        //{
        //    if (!PlayerInputsSingleton.PlayerInputsInstance.Player.enabled)
        //    {
        //        PlayerInputsSingleton.PlayerInputsInstance.Player.Enable();
        //    }
        //    //if (!input.Player.enabled)
        //    //{
        //    //    input.Player.Enable();
        //    //}
        //}
        void Update()
        {
            //Debug.Log("DM: is current action map player? = " + input.Player.enabled);
            //if (Input.GetKeyDown(KeyCode.Space) == true)
            //{
            //    Debug.Log(dIndex);
            //    if (dIndex == dialogueArray.Length)
            //    {
            //        EndOfDialogue();
            //    }
            //    else { NextSentence(); }
            //}
        }


        private void OnNextPerformed(InputAction.CallbackContext val)
        {
            Debug.Log(dIndex);
            if (dIndex == dialogueArray.Length)
            {
                EndOfDialogue();
            }
            else { NextSentence(); }
        }

        public void ReceiveStartReadyDialogue(string[] dialogue, string[] speaker, List<Sprite> speakerSprites)
        {
            // list of sprites is in the order of speaking; if the length of the list of Sprites
            // is 1 then its a single speaker, if the length is 2 then the first is the first speaker
            // and the second is the 2nd speaker [length max 2]
            // ex speaker = ["Farmer", "Illia", "Farmer", "Farmer"]
            // sprites[0] == Farmer's Sprite
            // sprites[1] == Illi's Sprite

            // switch the action map so space now controls the dialouge
            //SwitchMap();
            

            Setup();
            AudioSource.PlayClipAtPoint(progressClip, transform.localPosition);
            dialogueArray = dialogue;
            speakerArray = speaker;
            speakerName.text = speakerArray[dIndex];
            dialogueText.text = dialogueArray[dIndex];
            dIndex++;
            speaker1.sprite = speakerSprites[0];

            if (speakerSprites.Count == 1)
            {
                speaker2.color = new Color (255,255, 255, 0);
            }
            else
            {
                speaker2.sprite = speakerSprites[1];
                speaker2.color = inactiveAlpha;
            }
        }

        private void NextSentence()
        {
            //Debug.Log(speakerName.text + ": " + dialogueText.text);
            AudioSource.PlayClipAtPoint(progressClip, transform.localPosition);
            speakerName.text = speakerArray[dIndex];
            dialogueText.text = dialogueArray[dIndex];
            if (speakerArray[(dIndex - 1)] != speakerArray[dIndex])
            {
                if (speakerArray[dIndex] != speakerArray[0])
                {
                    speaker1.transform.localScale = new Vector3(0.787880003f, 0.787880003f, 0.787880003f);
                    speaker2.transform.localScale = new Vector3(1, 1, 1);
                    speaker2.color = activeAlpha;
                    speaker1.color = inactiveAlpha;

                    //speaker2.color = new Color(255, 255, 255, 255);
                    //speaker1.color = new Color(255, 255, 255, 50);
                }
                else
                {
                    speaker2.transform.localScale = new Vector3(0.787880003f, 0.787880003f, 0.787880003f);
                    speaker1.transform.localScale = new Vector3(1, 1, 1);
                    speaker1.color = activeAlpha;
                    speaker2.color = inactiveAlpha;

                    //speaker1.color = new Color(255, 255, 255, 255);
                    //speaker2.color = new Color(255, 255, 255, 50);

                }
            }

            dIndex++;

            


        }

        

        void EndOfDialogue()
        {
            //GameData.freezePlayer = false;
            gameObject.SetActive(false);
            
            //Debug.Log("End Of Dialogue.");

        }

        //void SwitchMap()
        //{
        //    if (input != null)
        //    {
        //        input.Enable();
        //    }
        //    Debug.Log("is current action map player? = " + input.Player.enabled);
        //    //if (InputSystem.IsCurrentActionMap("Player"))
        //    //{
                
        //    //    input.Player.Disable();
        //    //    input.Dialouge.Enable();

        //    //    input.Player.Movement.performed -= OnMovementPerformed;
        //    //    input.Player.Movement.canceled -= OnMovementCancelled;

        //    //    input.Dialouge.Next.performed += OnNextPerformed;
        //    //}
        //    //else
        //    //{
                
        //    //    input.Player.Enable();
        //    //    input.Dialouge.Disable();
        //    //    input.Dialouge.Next.performed -= OnNextPerformed;
        //    //}

        //    if (input.Player.enabled)
        //    {
        //        // switch map to dialouge
        //        input.Player.Disable();
        //        input.Dialouge.Enable();
                
        //        input.Dialouge.Next.performed += OnNextPerformed;

        //    }
        //    else
        //    {
        //        input.Player.Enable();
        //        input.Dialouge.Disable();
        //        input.Dialouge.Next.performed -= OnNextPerformed;
        //    }
        //    Debug.Log("After: is current action map player? = " + input.Player.enabled);
        //}

    }
}
