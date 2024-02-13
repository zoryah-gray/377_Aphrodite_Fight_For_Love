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
        public GameObject unlocks;
        public delegate void UnlockHandler();
        public event UnlockHandler Unlocked;


        private void OnEnable()
        {
            GameData.freezePlayer = true;
            riddle.text = riddleText;
        }

        public void ExitPuzzle()
        {
            currentChoices.Clear();
            GameData.freezePlayer = false;
            Unlock();
            gameObject.SetActive(false);
        }

        public void Unlock()
        {
            // door has been unlocked
            Unlocked?.Invoke();
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
