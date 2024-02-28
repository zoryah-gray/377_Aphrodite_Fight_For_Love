using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class CameraFollow : MonoBehaviour
    {
        public float cameraFollowSpeed = 2f;
        public float yOffset = 1.15f;
        public Transform target;

        void Update()
        {
            if (!GameData.moveCamFromPlayer)
            {
                Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, transform.position.z);
                transform.position = Vector3.Slerp(transform.position, newPos, cameraFollowSpeed * Time.fixedDeltaTime);
            }
        }

        public void MoveCameraToTarget(Transform target)
        {
            if (target != null && target.gameObject.name != "Player")
            {
                // Calculate the desired position based on the target's position
                Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

                // Move the camera to the target position smoothly using LeanTween
                LeanTween.move(gameObject, targetPosition, 2f).setEase(LeanTweenType.easeOutQuint);
            
            }
        }

        public void MoveCameraToTargetKey(Transform target, float duration)
        {
            if (target != null && target.gameObject.name != "Player")
            {
                // Calculate the desired position based on the target's position
                Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

                // Move the camera to the target position smoothly using LeanTween
                LeanTween.move(gameObject, targetPosition, duration).setEase(LeanTweenType.easeOutQuint);

            }
        }
        
    }
}
