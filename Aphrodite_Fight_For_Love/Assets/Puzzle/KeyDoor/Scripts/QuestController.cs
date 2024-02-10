using System.Collections;
using System.Collections.Generic;
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
        public delegate void UnlockHandler();
        public event UnlockHandler Unlocked;

        private void Awake()
        {
            
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.name == "Player")
            {
                icon.SetActive(true);
                collision.gameObject.GetComponent<PlayerKeypadPuzzleController>().inQuestTrigger = true;
                if (GameData.startingQuestActions)
                {
                    CheckQuestStatus();
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            // the player has clicked the interact button and are starting the quest
            if (collision.gameObject.name == "Player" && GameData.startingQuestActions) {
                CheckQuestStatus();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            
            if (collision.gameObject.name == "Player")
            {
                icon.SetActive(false);
                collision.gameObject.GetComponent<PlayerKeypadPuzzleController>().inQuestTrigger = false;
                StopAllCoroutines();
                GUITextManager.instance.SetActive(false);
            }
        }

        public void CheckQuestStatus()
        {
            //check if quest is not ongoing and not completed
            if (!GameData.CheckQuestOngoing(quest.questID) && !GameData.CheckQuestCompleted(quest.questID))
            {
                Debug.Log("quest not in ongoing => start it");
                //not in ongoing => start it
                GameData.AddQuestToOngoing(quest.questID, quest);
                StartQuest();
            }
            else if (quest.CheckQuestComplete())
            {
                Debug.Log("quest complete");
                // quest complete
                EndQuest();

            }
            else if (GameData.CheckQuestCompleted(quest.questID))
            {
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
            GUITextManager.instance.PrintToGUI(quest.questInstructions, quest.questSprite);
            
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
            GUITextManager.instance.PrintToGUI(quest.questEnd, quest.questSprite);
            quest.ongoing = false;
            quest.complete = true;
            GameData.RemoveQuestFromOngoing(quest.questID);
            GameData.AddQuestToCompleted(quest.questID, quest);
            Unlock();
            StartCoroutine(DeactivateGUI());

        }

        private void RepeatPrompt(bool completed)
        {
            if (!completed)
            {
                GUITextManager.instance.PrintToGUI(quest.questInstructions, quest.questSprite);
            }
            else
            {
                GUITextManager.instance.PrintToGUI(quest.questEnded, quest.questSprite);

            }
            // start a co-routine to turn off the GUI after a few seconds
            StartCoroutine(DeactivateGUI());
        }

        public void Unlock()
        {
            // door has been unlocked
            Unlocked?.Invoke();
        }


        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

    }
}
