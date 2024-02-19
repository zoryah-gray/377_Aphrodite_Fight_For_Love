using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class RotateObject : MonoBehaviour
    {
        public float degreesPerSec = 20f;
        public bool rotating = false;
        public void StartRotation()
        {
            rotating = true;
            degreesPerSec = 20f;
            Debug.Log("Rotating Start");
        }

        public void StopRotation()
        {
            rotating = false;
        }

        void Update()
        {
            if (rotating)
            {
                float rotAmount = degreesPerSec * Time.deltaTime;
                float curRot = transform.localRotation.eulerAngles.z;
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot + rotAmount));
            }
        }    
    }
}
