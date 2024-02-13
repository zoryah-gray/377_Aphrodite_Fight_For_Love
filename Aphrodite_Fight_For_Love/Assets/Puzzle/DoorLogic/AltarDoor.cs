using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class AltarDoor : Door
    {
        // a subclass of the door for quest puzzles

        [SerializeField] private DragAndDropManager altar;

        public void Start()
        {
            altar.Unlocked += OpenDoor;
        }

        private void OnDestroy()
        {
            altar.Unlocked -= OpenDoor;
        }

    }
}
