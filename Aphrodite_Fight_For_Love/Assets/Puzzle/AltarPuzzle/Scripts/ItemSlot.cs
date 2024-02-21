using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AphroditeFightCode
{
    public class ItemSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] public DragAndDropManager manager;
        RectTransform dropAreaRect;
        public int holdingItemID;
        public bool inUse = false;

        private void Awake()
        {
            dropAreaRect = GetComponent<RectTransform>();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Vector3[] corners = new Vector3[4];
            dropAreaRect.GetWorldCorners(corners);
            for (int i = 0; i < 4; i++)
            {
                Gizmos.DrawLine(corners[i], corners[(i + 1) % 4]);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            //Debug.Log("OnDrop Slot");
            // gameobject currently being dragged
            if (eventData.pointerDrag != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                if (eventData.pointerDrag.TryGetComponent<DragAndDropUIItem>(out DragAndDropUIItem itemScrpt))
                {
                    //Debug.Log("The item " + itemScrpt.name + " has been placed choiceID => " + itemScrpt.choiceID);
                    holdingItemID = itemScrpt.choiceID;
                    inUse = true;
                    manager.AddToChosen(holdingItemID);
                    manager.CheckComplete();
                    
                }
            }
        }

        private Rect GetScreenRect(RectTransform rt)
        {
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);
            var size = corners[2] - corners[0];
            Rect rect = new Rect(corners[0], size);
            return rect;
        }

        private bool IsInsideRectTransformArea(RectTransform container, RectTransform obj)
        {
            Rect cntRect = GetScreenRect(container);
            Rect objRect = GetScreenRect(obj);
            return cntRect.Overlaps(objRect);
        }

        private List<int> FindUIElemntsInContainer(RectTransform cntr)
        {

            List<int> elementsInBox = new List<int>();

            RectTransform[] allRectTransforms = GameObject.FindObjectsOfType<RectTransform>();


            //GameObject.Find("Spoon").GetComponent<Image>().color = Color.black;

            foreach (RectTransform rectTransform in allRectTransforms)
            {

                if (rectTransform != cntr && IsInsideRectTransformArea(rectTransform, cntr))
                {
                    //Debug.Log("UICHECK Checking: " + rectTransform.gameObject);
                    DragAndDropUIItem itemScrpt = rectTransform.gameObject.GetComponent<DragAndDropUIItem>();
                    //Debug.Log("-------------->" + ingredientControl);
                    if (itemScrpt != null)
                    {
                        elementsInBox.Add(itemScrpt.choiceID);
                    }

                }
            }


            return elementsInBox;
        }

        private void Update()
        {
            //foreach (int i in manager.currentChoices)
            //{
            //    Debug.Log("in current " + i);
            //}

            List<int> inSlots = FindUIElemntsInContainer(dropAreaRect);
            if (inSlots.Count == 0)
            {
                inUse = false;
                manager.RemoveFromList(holdingItemID);
                holdingItemID = -1;
                return;
            }

        }




    }
}
