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
            Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, transform.position.z);
            transform.position = Vector3.Slerp(transform.position, newPos, cameraFollowSpeed * Time.fixedDeltaTime);
        }
    }
}
