using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class Door : MonoBehaviour
    {
        public bool isOpen = false;
        [SerializeField] private CameraFollow cameraScript;

        public void OpenDoor()
        {
            GameData.moveCamFromPlayer = true;
            cameraScript.MoveCameraToTarget(gameObject.transform);
            Vector3 originalScale = transform.localScale;
            Vector3 originalPos = transform.position;
            GetComponent<SpriteRenderer>().color = Color.green;

            LeanTween.scaleX(gameObject, originalScale.x, 3f).setEase(LeanTweenType.easeOutQuint);
            LeanTween.moveX(gameObject, originalPos.x + originalScale.x, 3f).setEase(LeanTweenType.easeOutQuint).
                setOnComplete(DoorOpened);
            isOpen = true;


        }

        public void DoorOpened()
        {
            //moveCamFromPlayer
            //cameraScript.MoveCameraToTarget(gameObject.transform);
            GameData.moveCamFromPlayer = false;
        }
    }
}
