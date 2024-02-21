using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace AphroditeFightCode
{
    public class DialogueManager : MonoBehaviour
    {
        // Start is called before the first frame update
        //[SerializeField] float typeSpeed = 0.2f;
        TextMeshProUGUI[] dialogueBoxArray;
        [SerializeField] private GameObject player;
        TextMeshProUGUI dialogueText;
        TextMeshProUGUI speakerName;
        public string[] dialogueArray;
        public string[] speakerArray;
        int dIndex;

        bool isSetup = false;
        void Setup()
        {
            //Gather the text mesh pros used to display the text to the player
            dIndex = 0;
            dialogueBoxArray = GetComponentsInChildren<TextMeshProUGUI>();
            speakerName = dialogueBoxArray[0];
            dialogueText = dialogueBoxArray[1];
            
            //ReceiveStartReadyDialogue(dialogueArray, speakerArray);
            player.SetActive(false);
            
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) == true)
            {
                Debug.Log(dIndex);
                if (dIndex == dialogueArray.Length)
                {
                    EndOfDialogue();
                }
                else { NextSentence(); }
            }
        }

        public void ReceiveStartReadyDialogue(string[] dialogue, string[] speaker)
        {
            Setup();
            /* Debug.Log(speakerName.name);
             Debug.Log(dialogueText.name);*/
            dialogueArray = dialogue;
            speakerArray = speaker;
            speakerName.text = speakerArray[dIndex];
            dialogueText.text = dialogueArray[dIndex];
            dIndex++;
        }

        private void NextSentence()
        {
            Debug.Log(speakerName.text + ": " + dialogueText.text);
            speakerName.text = speakerArray[dIndex];
            dialogueText.text = dialogueArray[dIndex];
            dIndex++;

        }

        

        void EndOfDialogue()
        {

            player.SetActive(true);
            /*player.GetComponent<PlayerKeypadPuzzleController>().enabled = true;
            player.GetComponent<PlayerAttack>().enabled = true;
            player.GetComponent<PlayerMovement>().enabled = true;*/
            gameObject.SetActive(false);
            Debug.Log("End Of Dialogue. Change trigger thing");

        }



        /*public void nextSentence()
            {

                if (index < speech.Length - 1)
                {
                    index++;
                    textComponent.text = "";
                    nameComponent.text = parentName[index];
                    StartCoroutine(Type());
                }
                else
                {
                    textComponent.text = "";
                    nameComponent.text = string.Empty;
                    canvasWrite.SetActive(false);
                }

            }*/

        /*IEnumerator Type()
        {
            foreach (char letter in dialogueArray[dialogueIndex].ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typeSpeed);
            }

        }*/
    }
}
