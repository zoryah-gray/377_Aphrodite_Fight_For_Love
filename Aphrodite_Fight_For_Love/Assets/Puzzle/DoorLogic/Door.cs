using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class Door : MonoBehaviour
    {
        public bool isOpen = false;

        public void OpenDoor()
        {

            Vector3 originalScale = transform.localScale;
            Vector3 originalPos = transform.position;
            GetComponent<SpriteRenderer>().color = Color.green;

            LeanTween.scaleX(gameObject, originalScale.x, 1f).setEase(LeanTweenType.easeOutQuint);
            LeanTween.moveX(gameObject, originalPos.x + originalScale.x, 1f).setEase(LeanTweenType.easeOutQuint);
            isOpen = true;


        }
    }
}
