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

        public event Action<int> onOpenDoorTrigger;
        public void OpenDoorTrigger(int id)
        {
            if (onOpenDoorTrigger != null)
            {
                onOpenDoorTrigger(id);
            }
        }
    }
}
