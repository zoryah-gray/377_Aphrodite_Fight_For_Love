using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class unlockDoor : MonoBehaviour
    {
        bool collected = false;
        //GameObject door; 
        // Start is called before the first frame update
        void Start()
        {
        }

    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent("PlayerMovement") != null)
            {
                Debug.Log("Collision detected with player");
                // door.state = "open";
            }
        }
        // Update is called once per frame
        void Update()
        {
           
        }
    }
}
