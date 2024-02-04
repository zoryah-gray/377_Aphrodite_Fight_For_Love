using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class PlayerAttack : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        public Transform meleeBox;
        void Start ()
        {
            meleeBox = transform.Find("MeleeBox");
        }
        // Update is called once per frame
        void Update()
        {
            if (playerMovement.directionInt == 1)
            {
                meleeBox.position = new Vector3(transform.position.x, transform.position.y + 0.8f,transform.position.z);
                meleeBox.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (playerMovement.directionInt == 2)
            {
                meleeBox.position = new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z);
                meleeBox.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
            else if (playerMovement.directionInt == 3)
            {
                meleeBox.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
                meleeBox.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (playerMovement.directionInt == 4)
            {
                meleeBox.position = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
                meleeBox.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
    
        }
    }
}