using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AphroditeFightCode
{
    public class GameEvents : MonoBehaviour
    {
        public static GameEvents current;
        [SerializeField] private CameraFollow camScrpt;

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

        public event Action<int> onMoveToDoorTrigger;
        public void MoveToDoorTrigger(int id)
        {
            if (onMoveToDoorTrigger != null)
            {
                onMoveToDoorTrigger(id);
            }
        }

        private void ResetCam()
        {
            GameData.moveCamFromPlayer = false;
        }

        public void MoveCameraToKey(GameObject obj, float duration)
        {
            camScrpt.MoveCameraToTargetKey(obj.transform, duration);
        }
    }
}
