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
        public float transitionDuration = 2.5f;
        public int currIdx = 0;
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
            //List<Sprite> speakers = new List<Sprite>();
            //speakers.Add(quest.questSprite);
            dialogueManager.SetActive(true);
            dialogueManager.GetComponent<DialogueManager>().ReceiveStartReadyDialogue(dialogueList.ToArray(), speakerList.ToArray(), quest.speakers);
        }

        public void StartQuest()
        {
            
            ResetDialougeLists();
            GameData.inQuest = true;
            //foreach (var line in quest.questInstructions)
            //{
            //    speakerList.Add(quest.questGiverName);
            //    dialogueList.Add(line);
            //}
            Debug.Log(quest.questInstructions.Count);
            Debug.Log(quest.questInstructionsSpeakers.Count);
            speakerList = new List<string>(quest.questInstructionsSpeakers);
            dialogueList = new List<string>(quest.questInstructions);
            StartDialouge(dialogueList, speakerList);
            
            //GUITextManager.instance.PrintToGUI(quest.questInstructions, quest.GUIinstructions, quest.questSprite);
            
            quest.ongoing = true;

            StartCoroutine(QuestStartCamera());

            //activate all the quest keys throughout the scene
            foreach (GameObject key in requiredKeysGO)
            {
                key.SetActive(true);
            }
            GUITextManager.instance.InitalizeQuestBar(requiredKeysGO.Count, quest.keySprite, quest.questGoalText);
            // start a co-routine to turn off the GUI after a few seconds
            //StartCoroutine(DeactivateGUI());
        }

        IEnumerator QuestStartCamera()
        {
            while (dialogueManager != null && dialogueManager.activeSelf)
            {
                yield return null;
            }
            //GameData.freezePlayer = true;

            foreach (GameObject key in requiredKeysGO)
            {
                key.SetActive(true);

            }
            //GUITextManager.instance.InitalizeQuestBar(requiredKeysGO.Count, quest.keySprite);

            foreach (GameObject key in requiredKeysGO)
            {
                GameEvents.current.MoveCameraToKey(key, transitionDuration);
                yield return new WaitForSeconds(transitionDuration);

            }
            GameData.freezePlayer = false;
        }

        


        IEnumerator DeactivateGUI()
        {
            yield return new WaitForSeconds(4f);
            GUITextManager.instance.SetActive(false);
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
            GameData.inQuest = false;
            GameData.freezePlayer = false;
            //Unlock();
            //StartCoroutine(DeactivateGUI());

        }

        private void RepeatPrompt(bool completed)
        {
            ResetDialougeLists();
            if (!completed)
            {

                //speakerList = quest.questInstructionsSpeakers;
                //dialogueList = quest.questInstructions;
                speakerList = new List<string>(quest.questInstructionsSpeakers);
                dialogueList = new List<string>(quest.questInstructions);
                //foreach (var line in quest.questInstructions)
                //{
                //    speakerList.Add(quest.questGiverName);
                //    dialogueList.Add(line);
                //}

                //GUITextManager.instance.PrintToGUI(quest.questInstructions, quest.GUIinstructions, quest.questSprite);
            }
            else
            {
                speakerList.Add(quest.questGiverName);
                dialogueList.Add(quest.questEnded);
                GameEvents.current.MoveToDoorTrigger(quest.doorID);

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
