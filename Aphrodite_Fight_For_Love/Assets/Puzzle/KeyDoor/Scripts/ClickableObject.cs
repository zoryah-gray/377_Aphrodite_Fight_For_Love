using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    [CreateAssetMenu(fileName = "New ClickableKey", menuName = "KeyClickableObject")]
    public class ClickableKey : ScriptableObject
    {
        public KeyQuestManager quest;
        public Sprite keySprite;
        public string keyQuestName;
        public string keyName;
        public int keyID;
        public string info;


        public void AddToQuest(KeyQuestManager quest)
        {
            if (quest.AddKey(this))
            {
                quest.obtainedKeys.Add(keyID);
                if (quest.CheckQuestComplete())
                {
                    quest.complete = true;
                }
            }
        }
    }
}
