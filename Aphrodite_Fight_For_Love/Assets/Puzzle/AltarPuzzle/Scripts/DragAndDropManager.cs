using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AphroditeFightCode
{
    public class DragAndDropManager : MonoBehaviour
    {
        public TMP_Text riddle;
        public string riddleText;
        public bool answered = false;
        public List<int> correctChoices = new List<int>();
        public List<int> currentChoices = new List<int>();

        [Header("Quest Unlocks")]
        public int doorID;


        [Header("Dialouge")]
        [SerializeField] private GameObject dialogueManager;
        public List<string> dialogueList = new List<string>();
        public List<string> speakerList = new List<string>();
        public List<Sprite> speakerSprites = new List<Sprite>();



        private void OnEnable()
        {
            GameData.freezePlayer = true;
            riddle.text = riddleText;
            StartDialouge();
            //Prove yourself worthy to meet the goddess: The goddess Hestia is known for which two items?
        }

        public void StartDialouge()
        {
            dialogueManager.SetActive(true);
            dialogueManager.GetComponent<DialogueManager>().ReceiveStartReadyDialogue(dialogueList.ToArray(), speakerList.ToArray(), speakerSprites);
        }


        public void ExitPuzzle()
        {
            currentChoices.Clear();
            GameData.freezePlayer = false;
            GameEvents.current.OpenDoorTrigger(doorID);
            //Unlock();
            gameObject.SetActive(false);
        }


        public void AddToChosen(int id)
        {
            if (!ChoiceContains(id))
            {
                currentChoices.Add(id);
            }
        }

        public void RemoveFromList(int id)
        {
            if (ChoiceContains(id))
            {
                currentChoices.Remove(id);
            }
        }

        public bool ChoiceContains(int id)
        {
            if (currentChoices.Contains(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CheckComplete()
        {
            if (currentChoices.Count == correctChoices.Count)
            {
                foreach (int i in currentChoices)
                {
                    if (!correctChoices.Contains(i))
                    {

                        return;
                    }
                }
                Debug.Log("RIDDLE ANSWERED - all correct choices have been made");
                riddle.color = Color.green;
                answered = true;
                Invoke("ExitPuzzle", 1.5f);
            }
            else
            {
                return;
            }
            
        }
    }
}
