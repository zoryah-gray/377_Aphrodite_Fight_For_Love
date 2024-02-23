using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace AphroditeFightCode
{
    public class QuestController : MonoBehaviour
    {
        // quest controller for populating a questObject
        [Header("Quest SO")]
        public KeyQuestManager quest;
        public List<GameObject> requiredKeysGO = new List<GameObject>();
        
        [Header("Quest Icon")]
        public GameObject icon;
        [Header("Quest Unlocks")]
        public GameObject unlocks;
        // event to unlock the door
        //public delegate void UnlockHandler();
        //public event UnlockHandler Unlocked;
        [Header("Dialouge")]
        [SerializeField] private GameObject dialogueManager;
        private List<string> dialogueList = new List<string>();
        private List<string> speakerList = new List<string>();
        private bool completedDialouge = false;
        //[SerializeField] private DialogueManager dialogueManager;
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
            if (quest != null)
            {
                quest.ResetValues();
            }
            else
            {
                Debug.LogWarning("A KeyQuestManager object has not been assigned");
            }

        }

        private void OnEnable()
        {
            if (!PlayerInputsSingleton.PlayerInputsInstance.Player.enabled)
            {
                PlayerInputsSingleton.PlayerInputsInstance.Player.Enable();
            }
            PlayerInputsSingleton.PlayerInputsInstance.Player.Interact.performed += OnInteractPerformed;

            //if (!input.Player.enabled)
            //{
            //    input.Player.Enable();
                
            //}
            //input.Player.Interact.performed += OnInteractPerformed;
        }
        private void OnDisable()
        {
            PlayerInputsSingleton.PlayerInputsInstance.Player.Interact.performed -= OnInteractPerformed;

            //if (input.Player.enabled)
            //{
            //    input.Player.Disable();
            //}
            //input.Player.Interact.performed -= OnInteractPerformed;

            StopAllCoroutines();
        }

        private void OnInteractPerformed(InputAction.CallbackContext val)
        {
            Debug.Log("Quest interact Click");
            if (inTrigger)
            {
                CheckQuestStatus();
            }
        }



        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            Debug.Log(collision.gameObject.name);


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
                inTrigger = false;
                icon.SetActive(false);
            }
        }

        public void CheckQuestStatus()
        {
            Debug.Log("status => " + GameData.CheckQuestCompleted(quest.questID));
            //check if quest is not ongoing and not completed
            if (!GameData.CheckQuestOngoing(quest.questID) && !GameData.CheckQuestCompleted(quest.questID) && !quest.complete)
            {
                Debug.Log("quest not in ongoing => start it");
                //not in ongoing => start it
                GameData.startingQuestActions = true;
                GameData.AddQuestToOngoing(quest.questID, quest);
                StartQuest();
            }
            else if (quest.CheckQuestComplete() & !completedDialouge)
            {
                Debug.Log("quest complete");
                // quest complete
                EndQuest();

            }
            else if (completedDialouge)
            {
                Debug.Log("repeat end prompt");
                RepeatPrompt(true);
            }
            else
            {
                Debug.Log("repeat prompt");
                //repeat prompt
                RepeatPrompt(false);
            }
            //GameData.startingQuestActions = false;
        }

        public void StartDialouge(List<string> dialogueList, List<string> speakerList)
        {
            List<Sprite> speakers = new List<Sprite>();
            speakers.Add(quest.questSprite);
            dialogueManager.SetActive(true);
            dialogueManager.GetComponent<DialogueManager>().ReceiveStartReadyDialogue(dialogueList.ToArray(), speakerList.ToArray(), speakers);
        }

        public void StartQuest()
        {
            
            ResetDialougeLists();
            foreach (var line in quest.questInstructions)
            {
                speakerList.Add(quest.questGiverName);
                dialogueList.Add(line);
            }
            StartDialouge(dialogueList, speakerList);
            
            //GUITextManager.instance.PrintToGUI(quest.questInstructions, quest.GUIinstructions, quest.questSprite);
            
            quest.ongoing = true;
            //activate all the quest keys throughout the scene
            foreach (GameObject key in requiredKeysGO)
            {
                key.SetActive(true);
            }
            GUITextManager.instance.InitalizeQuestBar(requiredKeysGO.Count, quest.keySprite);
            // start a co-routine to turn off the GUI after a few seconds
            //StartCoroutine(DeactivateGUI());
        }

        IEnumerator DeactivateGUI()
        {
            yield return new WaitForSeconds(4f);
            GUITextManager.instance.SetActive(false);
            //if (quest.complete)
            //{
            //    //move camera over to position of door
            //}
        }

        public void EndQuest()
        {
            
            ResetDialougeLists();
            foreach (var line in quest.questEnd)
            {
                speakerList.Add(quest.questGiverName);
                dialogueList.Add(line);
            }
            StartDialouge(dialogueList, speakerList);

            //GUITextManager.instance.PrintToGUI(quest.questEnd, quest.GUIinstructions ,quest.questSprite);
            quest.ongoing = false;
            quest.complete = true;
            completedDialouge = true;
            GameData.RemoveQuestFromOngoing(quest.questID);
            GameData.AddQuestToCompleted(quest.questID, quest);
            GUITextManager.instance.DeactivateQuestBar();
            GameData.moveCamFromPlayer = true;
            GameEvents.current.OpenDoorTrigger(quest.doorID);
            //Unlock();
            //StartCoroutine(DeactivateGUI());

        }

        private void RepeatPrompt(bool completed)
        {
            ResetDialougeLists();
            if (!completed)
            {
                foreach (var line in quest.questInstructions)
                {
                    speakerList.Add(quest.questGiverName);
                    dialogueList.Add(line);
                }

                //GUITextManager.instance.PrintToGUI(quest.questInstructions, quest.GUIinstructions, quest.questSprite);
            }
            else
            {
                speakerList.Add(quest.questGiverName);
                dialogueList.Add(quest.questEnded);

                //GUITextManager.instance.PrintToGUI(quest.questEnded, quest.GUIinstructions, quest.questSprite);

            }

            StartDialouge(dialogueList, speakerList);

            // start a co-routine to turn off the GUI after a few seconds
            //StartCoroutine(DeactivateGUI());
        }

        void ResetDialougeLists()
        {
            speakerList.Clear();
            dialogueList.Clear();
        }




        private void OnDestroy()
        {
            StopAllCoroutines();
        }

    }
}
