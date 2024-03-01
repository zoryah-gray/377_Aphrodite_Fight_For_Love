using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class TrapDoor : Door
    {
        public bool unlockedConditionFulfilled = false;
        public List<Sprite> speakerSprites = new List<Sprite>();
        public List<string> unlockInstructions = new List<string>();

       
    }
}
