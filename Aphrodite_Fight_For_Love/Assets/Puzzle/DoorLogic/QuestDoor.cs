using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class QuestDoor : Door
    {
        // a subclass of the door for quest puzzles

        [SerializeField] private QuestController quest;

        //public void Start()
        //{
        //    quest.Unlocked += OpenDoor;
        //}

        //private void OnDestroy()
        //{
        //    quest.Unlocked -= OpenDoor;
        //}

    }
}
