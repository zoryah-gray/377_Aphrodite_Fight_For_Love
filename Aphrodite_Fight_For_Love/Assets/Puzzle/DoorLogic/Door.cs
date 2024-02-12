using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class Door : MonoBehaviour
    {
        public bool isOpen = false;
        [SerializeField] private CameraFollow camScrpt;

        public void OpenDoor()
        {
            camScrpt.MoveCameraToTarget(gameObject.transform);
            Vector3 originalScale = transform.localScale;
            Vector3 originalPos = transform.position;
            GetComponent<SpriteRenderer>().color = Color.green;

            LeanTween.scaleX(gameObject, originalScale.x, 5f).setEase(LeanTweenType.easeOutQuint);
            LeanTween.moveX(gameObject, originalPos.x + originalScale.x, 5f).setEase(LeanTweenType.easeOutQuint).setOnComplete(ResetCam);
            isOpen = true;



        }

        private void ResetCam()
        {
            GameData.moveCamFromPlayer = false;
        }
    }
}
