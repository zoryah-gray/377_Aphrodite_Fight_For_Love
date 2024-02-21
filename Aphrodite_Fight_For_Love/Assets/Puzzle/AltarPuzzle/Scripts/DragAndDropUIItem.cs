using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AphroditeFightCode
{
    public class DragAndDropUIItem : MonoBehaviour, IPointerDownHandler, IDragHandler ,IBeginDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField]private Canvas canvas;
        private CanvasGroup canvasGroup;
        private RectTransform rt;
        [Header("Item Info")]
        public string itemName;
        public string info;
        public int choiceID;
        public Sprite itemSprite;

        private void Awake()
        {
            rt = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log("OnPointerDown");
        }

        public void OnDrag(PointerEventData eventData)
        {
            //Debug.Log("OnDrag");
            rt.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            //Debug.Log("OnBeginDrag");
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            //Debug.Log("OnEndDrag");
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }

        public void OnDrop(PointerEventData eventData)
        {
            //Debug.Log("OnDrop");
        }
    }
}
