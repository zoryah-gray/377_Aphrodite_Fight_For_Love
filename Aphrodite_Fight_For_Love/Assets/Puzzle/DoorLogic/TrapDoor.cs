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

        public GameObject minionAreaRaycast;
        public int minionsLeft;
        public Collider2D[] minions;
        public float areaHeight = 10f;
        public float areaWidth = 10f;
        public LayerMask minionLayer;
        public bool inArea;

        private void Start()
        {
            //GameEvents.current.onTrapDoorTriggerEnter += base.OnOpenDoor;

            GameEvents.current.onTrapDoorTriggerEnter += OnTrapDoorOpen;
            GameEvents.current.onTrapDoorTriggerExit += OnTrapDoorClose;
        }

        private void OnDestroy()
        {
            GameEvents.current.onTrapDoorTriggerEnter -= OnOpenDoor;
            GameEvents.current.onTrapDoorTriggerExit -= OnMoveToDoor;
        }

        private void Update()
        {
            if (inArea && !unlockedConditionFulfilled)
            {

                CheckMinions();
            }
        }

        public void CheckMinions()
        {
            int found;
            //minions = Physics2D.OverlapCircleAll(minionAreaRaycast.transform.position, areaRadius, minionLayer);
            minions = Physics2D.OverlapBoxAll(minionAreaRaycast.transform.position, new Vector2(areaWidth, areaHeight), 0f, minionLayer);
            found = minions.Length;
            minionsLeft = found;
            //Debug.Log("found _" + found + "_ minions");
            if (found == 0)
            {
                unlockedConditionFulfilled = true;
                GameEvents.current.TrapDoorTriggerEnter(id);
            }
        }

        private void OnTrapDoorOpen(int id)
        {
            //OpenDoor(id);
            if (id == base.id)
            {
                Debug.Log("RUNNING: Open door");

                if (!isOpen)
                {
                    //LeanTween.cancel(gameObject);
                    originalScale = transform.localScale;
                    originalPos = transform.position;
                    //GetComponent<SpriteRenderer>().color = Color.green;
                    Vector3 size = GetComponent<SpriteRenderer>().bounds.size;
                    float topCoordinate = transform.position.y + (transform.localScale.y * 0.5f);
                    Debug.Log("|->size of boz = " + size.y * 2f);
                    Debug.Log("|->original coordy =" + transform.position + "top coord = " + topCoordinate);

                    if (openHorz)
                    {
                        LeanTween.moveX(gameObject, originalPos.x + (originalScale.x * 2f * direction), 5f).setEase(LeanTweenType.easeOutQuint);
                    }
                    else
                    {
                        LeanTween.moveY(gameObject, originalPos.y + topCoordinate, 5f).setEase(LeanTweenType.easeOutQuint);

                    }
                    isOpen = true;
                }
            }
        }

        private void OnTrapDoorClose(int id)
        {

            if (id == base.id)
            {


                Debug.Log("CLOSE to pos = " + originalPos);
                if (isOpen)
                {
                    //Invoke("MoveCam", 1f);
                    //LeanTween.cancel(gameObject);
                    if (openHorz)
                    {
                        //LeanTween.scaleX(gameObject, originalScale.x, 5f).setEase(LeanTweenType.easeOutQuint);
                        LeanTween.moveX(gameObject, originalPos.x, 3f).setEase(LeanTweenType.easeOutQuint);
                    }
                    else
                    {
                        //LeanTween.scaleY(gameObject, originalScale.y, 5f).setEase(LeanTweenType.easeOutQuint);
                        LeanTween.moveY(gameObject, originalPos.y, 3f).setEase(LeanTweenType.easeOutQuint);
                    }
                    
                    
                    isOpen = false;
                }
            }

        }

        public void MoveCam()
        {
            camScrpt.MoveCameraToTarget(gameObject.transform);
        }



        void OnDrawGizmos()
        {
            float halfWidth = areaWidth / 2f;
            float halfHeight = areaHeight / 2f;

            // Draw a wireframe square in the Scene view to visualize the detection area
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(minionAreaRaycast.transform.position, new Vector3(areaWidth, areaHeight, 0f));
        }


    }
}
