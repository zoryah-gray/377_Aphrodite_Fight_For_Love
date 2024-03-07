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
        [SerializeField] public CameraFollow camScrpt;
        //[SerializeField] private GameObject uncoversArea;
        public List<GameObject> objectsToFade;
        public float fadeDuration = 1.0f;
        public Vector3 originalScale;
        public Vector3 originalPos;

        [Header("Audio Files")]
        public AudioClip doorClip;
        

        private void Start()
        {
            GameEvents.current.onOpenDoorTrigger += OnOpenDoor;
            GameEvents.current.onMoveToDoorTrigger += OnMoveToDoor;
            //GameEvents.current.
        }

        private void OnDestroy()
        {
            GameEvents.current.onOpenDoorTrigger -= OnOpenDoor;
            GameEvents.current.onMoveToDoorTrigger -= OnMoveToDoor;
        }

        public void OnMoveToDoor(int id)
        {
            if (id == this.id)
            {
                camScrpt.MoveCameraToTarget(gameObject.transform);
                StartCoroutine(CamWait());
                
            }
        }

        IEnumerator CamWait()
        {
            yield return new WaitForSeconds(2f);
            ResetCam();
        }

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
                

                if (!isOpen)
                {
                    originalScale = transform.localScale;
                    originalPos = transform.position;
                    //GetComponent<SpriteRenderer>().color = Color.green;
                    Vector3 size = GetComponent<SpriteRenderer>().bounds.size;
                    float topCoordinate = transform.position.y + (transform.localScale.y * 0.5f);
                    //Debug.Log("size of boz = " + size.y * 2f);
                    //Debug.Log("original coordy =" + transform.position + "top coord = " +  topCoordinate);
                    AudioSource.PlayClipAtPoint(doorClip, transform.localPosition);
                    if (openHorz)
                    {
                        //Debug.Log("open Horz scale x = " + originalScale.x);
                        //LeanTween.scaleX(gameObject, originalScale.x, 5f).setEase(LeanTweenType.easeOutQuint);
                        LeanTween.moveX(gameObject, originalPos.x + (originalScale.x * 2f * direction), 5f).setEase(LeanTweenType.easeOutQuint).setOnComplete(ResetCam);
                    }
                    else
                    {
                        //Debug.Log("open Horz scale y = " + originalScale.y);
                        //LeanTween.scaleY(gameObject, originalScale.y, 5f).setEase(LeanTweenType.easeOutQuint);
                        //LeanTween.moveY(gameObject, originalPos.y + (originalScale.y * 2f * direction), 5f).setEase(LeanTweenType.easeOutQuint).setOnComplete(ResetCam);
                        LeanTween.moveY(gameObject, topCoordinate, 5f).setEase(LeanTweenType.easeOutQuint).setOnComplete(ResetCam);

                    }


                    //LeanTween.alpha(uncoversArea, 0f, 5f).setOnComplete(uncoverArea);
                    if (objectsToFade.Count != 0)
                    {
                        foreach (GameObject obj in objectsToFade)
                        {
                            // Use LeanTween to fade out the alpha of the object
                            LeanTween.alpha(obj, 0f, 4f + fadeDuration)
                                .setEase(LeanTweenType.easeOutQuad)
                                .setOnComplete(() => obj.SetActive(false)); // Optional: Destroy the object after fading out
                                                                            //.setOnComplete(() => Destroy(obj)); // Optional: Destroy the object after fading out
                        }
                    }
                    isOpen = true;
                }
                else
                {
                    Invoke("ResetCam", 5f);
                }
            }
        }

        IEnumerator Pause(int id)
        {
            yield return new WaitForSeconds(0.5f);
            OpenDoor(id);
        }

        public void ResetCam()
        {
            GameData.moveCamFromPlayer = false;
        }
    }
}
