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
        private bool completedDialouge = false;
        [Header("Quest Icon")]
        public GameObject icon;
        [Header("Quest Unlocks")]
        public GameObject unlocks;
        // event to unlock the door
        public delegate void UnlockHandler();
        public event UnlockHandler Unlocked;
        [Header("Inputs")]
        private PlayerInputs input = null;
        [SerializeField] private bool interacting = false;
        public bool inTrigger = false;

        private void Awake()
        {
            input = new PlayerInputs();
            Intialize();
        }


        private void Intialize()
        {
            if (quest != null)
            {
                return;
            }
            else
            {
                Debug.LogWarning("A KeyQuestManager object has not been assigned");
            }
        }

        private void OnEnable()
        {
            input.Player.Enable();
            input.Player.Interact.performed += OnInteractPerformed;
            //input.Player.Interact.canceled += OnInteractCanceled;
        }
        private void OnDisable()
        {
            input.Player.Disable();
            input.Player.Interact.performed -= OnInteractPerformed;
            //input.Player.Interact.canceled -= OnInteractCanceled;
            StopAllCoroutines();
        }

        private void OnInteractPerformed(InputAction.CallbackContext val)
        {
            Debug.Log("Quest interact Click");
            interacting = true;
            if (inTrigger)
            {
                CheckQuestStatus();
            }
        }

        private void Update()
        {
            
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            Debug.Log(collision.gameObject.name);

            if (collision.gameObject.name == "Player")
            {
                icon.SetActive(true);
                inTrigger = true;
                collision.gameObject.GetComponent<PlayerKeypadPuzzleController>().inQuestTrigger = true;
                if (interacting)
                {
                    GameData.startingQuestActions = true;
                    interacting = false;
                }
                if (GameData.startingQuestActions)
                {
                    CheckQuestStatus();
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            // the player has clicked the interact button and are starting the quest
            if (collision.gameObject.name == "Player" && (GameData.startingQuestActions || interacting)) {
                CheckQuestStatus();
                if (interacting)
                {
                    interacting = false;
                }
            }
            //else if (collision.gameObject.name == "Player" && interacting)
            //{
            //    CheckQuestStatus();
            //}
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            
            if (collision.gameObject.name == "Player")
            {
                inTrigger = false;
                icon.SetActive(false);
                collision.gameObject.GetComponent<PlayerKeypadPuzzleController>().inQuestTrigger = false;
                StopAllCoroutines();
                GUITextManager.instance.SetActive(false);
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

        public void StartQuest()
        {
            Debug.Log("starting quest");
            GUITextManager.instance.PrintToGUI(quest.questInstructions, quest.GUIinstructions, quest.questSprite);
            
            quest.ongoing = true;
            
            //activate all the quest keys throughout the scene
            foreach (GameObject key in requiredKeysGO)
            {
                key.SetActive(true);
            }
            // start a co-routine to turn off the GUI after a few seconds
            StartCoroutine(DeactivateGUI());
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
            Debug.Log("Ending Quest");
            GUITextManager.instance.PrintToGUI(quest.questEnd, quest.GUIinstructions ,quest.questSprite);
            quest.ongoing = false;
            quest.complete = true;
            completedDialouge = true;
            GameData.RemoveQuestFromOngoing(quest.questID);
            GameData.AddQuestToCompleted(quest.questID, quest);
            GameData.moveCamFromPlayer = true;
            Unlock();
            StartCoroutine(DeactivateGUI());

        }

        private void RepeatPrompt(bool completed)
        {
            if (!completed)
            {
                GUITextManager.instance.PrintToGUI(quest.questInstructions, quest.GUIinstructions, quest.questSprite);
            }
            else
            {
                GUITextManager.instance.PrintToGUI(quest.questEnded, quest.GUIinstructions, quest.questSprite);

            }
            // start a co-routine to turn off the GUI after a few seconds
            StartCoroutine(DeactivateGUI());
        }

        public void Unlock()
        {
            // door has been unlocked
            Unlocked?.Invoke();
        }



        private void OnDestroy()
        {
            StopAllCoroutines();
        }

    }
}
