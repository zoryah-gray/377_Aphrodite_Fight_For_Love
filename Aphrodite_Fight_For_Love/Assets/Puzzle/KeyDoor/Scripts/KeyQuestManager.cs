using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    [CreateAssetMenu(fileName = "New KeyQuest", menuName = "KeyQuest")]
    public class KeyQuestManager : ScriptableObject
    {
        public Sprite questSprite;
        public string questName;
        public int questID;
        public int sceneID;
        public int keyAmt;
        public List<ClickableKey> requiredKeys = new List<ClickableKey>();
        
        public List<int> requiredKeysID = new List<int>();
        public List<int> obtainedKeys = new List<int>();
        public bool ongoing = false;
        public bool complete = false;
        public string questInstructions = "I'd open that door, but I've lost my apples";
        public string questEnd = "My apples!! Good job, I'll unlocked it for you now";
        public string questEnded = "Gah I've already opened it for you";
        

        private void Awake()
        {
            if (requiredKeys.Count != 0 && requiredKeys.Count != keyAmt)
            {
                foreach(ClickableKey key in requiredKeys)
                {
                    Debug.Log("|-> Adding to Quest " + questName + ": key " + key.keyName + " (" + key.keyID + ")");
                    requiredKeysID.Add(key.keyID);
                }
            }
        }

        public bool CheckQuestComplete()
        {
            if (obtainedKeys.Count == requiredKeys.Count)
            {
                foreach (int i in obtainedKeys)
                {
                    if (!requiredKeysID.Contains(i))
                    {
                        Debug.Log(questName + " is NOT complete");
                        return false;
                    }
                }
                ongoing = false;
                complete = true;
                Debug.Log(questName + " is complete");
                
                return true;
            }
            Debug.Log(questName + " is NOT complete");
            return false;
        }

        public bool CheckKey(int id)
        {
            if (!requiredKeysID.Contains(id) && obtainedKeys.Contains(id))
            {
                return false;
            }
            return true;
            
        }

        public bool AddKey(ClickableKey key)
        {
            if (key.keyQuestName == questName && CheckKey(key.keyID))
            {
                ResetObtained();
                return true;
            }
            return false;
        }

        private void ResetObtained()
        {
            if (obtainedKeys.Count > keyAmt)
            {
                obtainedKeys.RemoveRange(keyAmt, obtainedKeys.Count - keyAmt);
                
            }
        }

        
    }
}
