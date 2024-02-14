using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AphroditeFightCode
{
    public class GameEvents : MonoBehaviour
    {
        public static GameEvents current;

        private void Awake()
        {
            current = this;
        }

        public event Action<int> onOpenDoor;
        public void OpenDoor(int id)
        {
            if (onOpenDoor != null)
            {
                onOpenDoor(id);
            }
        }
    }
}
