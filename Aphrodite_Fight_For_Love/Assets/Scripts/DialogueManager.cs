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
        [SerializeField] float typeSpeed = 0.2f;
        TextMeshProUGUI[] dialogueBoxArray;
        TextMeshProUGUI dialogueText;
        TextMeshProUGUI speakerName;
        string[] dialogueArray;
        string[] speakerArray;
        int dialogueIndex;
        void Start()
        {
            //Gather the text mesh pros used to display the text to the player
            dialogueBoxArray = GetComponentsInChildren<TextMeshProUGUI>();
            
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetAxis("ProgressDialogue") != 0)
            {

            }
        }

        void ReceiveAndStartDialogue(string[] dialogue)
        {
            dialogueArray = dialogue;
            speakerName = dialogueBoxArray[0];
            dialogueText = dialogueBoxArray[1];
            speakerName.text = "Illia";
            dialogueText.text = "Testing, Testing, 1 2 3. Please save me from the utter hell i'm about to go through.";
        }

        private void NextSentence(string sentence)
        {

        }

        IEnumerator Type()
        {
            foreach (char letter in dialogueArray[dialogueIndex].ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typeSpeed);
            }

        }

        void EndOfDialogue()
        {
            gameObject.SetActive(false);
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
    }
}
