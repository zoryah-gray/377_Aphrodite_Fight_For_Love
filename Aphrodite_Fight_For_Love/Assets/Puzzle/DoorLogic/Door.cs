using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class Door : MonoBehaviour
    {
        public bool isOpen = false;
        public bool openHorz = true;
        public int direction = 1;
        public int id;
        [SerializeField] private CameraFollow camScrpt;
        //[SerializeField] private GameObject uncoversArea;
        public List<GameObject> objectsToFade;
        public float fadeDuration = 1.0f;

        private void Start()
        {
            GameEvents.current.onOpenDoorTrigger += OnOpenDoor;
        }

        private void OnDestroy()
        {
            GameEvents.current.onOpenDoorTrigger -= OnOpenDoor;
        }

        //public void OnOpenDoor(int id)
        //{
        //    //Debug.Log("Door compring id vs this | " + id + "  ,   " + this.id);
        //    if (id == this.id)
        //    {
        //        // wait two seconds before running
        //        StartCoroutine(Pause());
        //        Debug.Log("Door comparing id vs this | " + id + "  ,   " + this.id);
        //        camScrpt.MoveCameraToTarget(gameObject.transform);
        //        Vector3 originalScale = transform.localScale;
        //        Vector3 originalPos = transform.position;
        //        GetComponent<SpriteRenderer>().color = Color.green;

        //        if (openHorz)
        //        {
        //            LeanTween.scaleX(gameObject, originalScale.x, 5f).setEase(LeanTweenType.easeOutQuint);
        //            LeanTween.moveX(gameObject, originalPos.x + originalScale.x, 5f).setEase(LeanTweenType.easeOutQuint).setOnComplete(ResetCam);
        //        }
        //        else
        //        {
        //            LeanTween.scaleY(gameObject, originalScale.y, 5f).setEase(LeanTweenType.easeOutQuint);
        //            LeanTween.moveY(gameObject, originalPos.y + originalScale.y, 5f).setEase(LeanTweenType.easeOutQuint).setOnComplete(ResetCam);
        //        }
        //        isOpen = true;
        //        //LeanTween.alpha(uncoversArea, 0f, 5f).setOnComplete(uncoverArea);
        //        foreach (GameObject obj in objectsToFade)
        //        {
        //            // Use LeanTween to fade out the alpha of the object
        //            LeanTween.alpha(obj, 0f, 4f + fadeDuration)
        //                .setEase(LeanTweenType.easeOutQuad)
        //                .setOnComplete(() => obj.SetActive(false)); // Optional: Destroy the object after fading out
        //                //.setOnComplete(() => Destroy(obj)); // Optional: Destroy the object after fading out
        //        }
        //    }
        //}

        public void OnOpenDoor(int id)
        {
            //Debug.Log("Door compring id vs this | " + id + "  ,   " + this.id);
            if (id == this.id)
            {
                // wait two seconds before running
                StartCoroutine(Pause(id));
            }
        }

        public void OpenDoor(int id)
        {
            if (id == this.id)
            {
               
                Debug.Log("Door comparing id vs this | " + id + "  ,   " + this.id);
                camScrpt.MoveCameraToTarget(gameObject.transform);
                Vector3 originalScale = transform.localScale;
                Vector3 originalPos = transform.position;
                //GetComponent<SpriteRenderer>().color = Color.green;

                if (openHorz)
                {
                    LeanTween.scaleX(gameObject, originalScale.x, 5f).setEase(LeanTweenType.easeOutQuint);
                    LeanTween.moveX(gameObject, originalPos.x + (originalScale.x * direction), 5f).setEase(LeanTweenType.easeOutQuint).setOnComplete(ResetCam);
                }
                else
                {
                    LeanTween.scaleY(gameObject, originalScale.y, 5f).setEase(LeanTweenType.easeOutQuint);
                    LeanTween.moveY(gameObject, originalPos.y + (originalScale.y * direction), 5f).setEase(LeanTweenType.easeOutQuint).setOnComplete(ResetCam);
                }
                isOpen = true;
                //LeanTween.alpha(uncoversArea, 0f, 5f).setOnComplete(uncoverArea);
                foreach (GameObject obj in objectsToFade)
                {
                    // Use LeanTween to fade out the alpha of the object
                    LeanTween.alpha(obj, 0f, 4f + fadeDuration)
                        .setEase(LeanTweenType.easeOutQuad)
                        .setOnComplete(() => obj.SetActive(false)); // Optional: Destroy the object after fading out
                                                                    //.setOnComplete(() => Destroy(obj)); // Optional: Destroy the object after fading out
                }
            }
        }

        IEnumerator Pause(int id)
        {
            yield return new WaitForSeconds(0.5f);
            OpenDoor(id);
        }

        private void ResetCam()
        {
            GameData.moveCamFromPlayer = false;
        }
    }
}
