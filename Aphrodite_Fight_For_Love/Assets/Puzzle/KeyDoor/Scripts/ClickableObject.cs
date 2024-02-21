using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    [CreateAssetMenu(fileName = "New ClickableKey", menuName = "KeyClickableObject")]
    public class ClickableKey : ScriptableObject
    {
        public KeyQuestManager quest;
        [Header("Key Sprite and Anim")]
        public Sprite keySprite;
        public bool hasAnimation;
        [Header("Key Info")]
        public string keyQuestName;
        public string keyName;
        public int keyID;
        public string info;
        public string instructions;


        public void AddToQuest(KeyQuestManager quest)
        {
            if (quest.AddKey(this))
            {
                quest.obtainedKeys.Add(keyID);
                GUITextManager.instance.UpdateQuestBar();
                if (quest.CheckQuestComplete())
                {
                    quest.complete = true;
                }
            }
        }
    }
}
