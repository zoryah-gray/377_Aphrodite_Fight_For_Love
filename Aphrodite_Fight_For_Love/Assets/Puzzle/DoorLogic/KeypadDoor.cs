using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class KeypadDoor : Door
    {
        // a subclass of the door

        [SerializeField] private KeyPadContoller keypad;

        //public void Start()
        //{
        //    keypad.Unlocked += OpenDoor(keypad.doorID);
        //}

        //private void OnDestroy()
        //{
        //    keypad.Unlocked -= OpenDoor;
        //}
    }
}
