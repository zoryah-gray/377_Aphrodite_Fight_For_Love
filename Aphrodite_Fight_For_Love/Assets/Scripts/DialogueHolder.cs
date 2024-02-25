using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class DialogueHolder : MonoBehaviour
    {

        [SerializeField] public string[] speakerList;
        [SerializeField] public string[] dialogueList;
        [SerializeField] public List<Sprite> sprites;
        public bool sceneStart = false;
        public bool sceneFinish  = false;
        public bool singleSpeaker = false;
        [SerializeField]GameObject dialogueManager;
        // Start is called before the first frame update
        void Start()
        {
          //GameObject.Find("DialogueManager");
          Debug.Assert(speakerList.Length == dialogueList.Length, "Speaker length not equal to dialogue list");
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        //On Click
        private void OnTriggerEnter2D(Collider2D collision)
        {
            /*f (Input.GetKeyDown(KeyCode.F))
            {*/

                //If only one person is speaking, hide the other speaker image. Otherwise,
                //make sure it is visible
                
                //Set the dialogue manager active (that script handles making the canvas visible)
                //and load the dialogue into the manager.
                dialogueManager.SetActive(true);

            /*Debug.Log(dialogueList[0]);
            Debug.Log(speakerList[0]);*/
                
                dialogueManager.GetComponent<DialogueManager>().ReceiveStartReadyDialogue(dialogueList, speakerList, sprites);

                /*if (singleSpeaker)
                {
                    GameObject.Find("SpeakerImage2").SetActive(false);
                }
                else if (GameObject.Find("SpeakerImage2").activeInHierarchy == false)
                {
                    GameObject.Find("SpeakerImage2").SetActive(true);
                }*/
            /*}*/
        }

        
    }
}
