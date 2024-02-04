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
            if (playerMovement.directionInt == 1) //Up
            {
                meleeBox.position = new Vector3(transform.position.x, transform.position.y + 0.8f,transform.position.z);
                Debug.Log(playerMovement.animAfterLeft);
                if (playerMovement.animAfterLeft == true)
                {
                    meleeBox.rotation = Quaternion.Euler(0f, 0f, 270f);
                }
                else
                {
                    meleeBox.rotation = Quaternion.Euler(0f, 0f, 90f);
                }
            }
            else if (playerMovement.directionInt == 2) //Right
            {
                meleeBox.position = new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z);
                meleeBox.rotation = Quaternion.Euler(0f, 180f, 180f);

            }
            else if (playerMovement.directionInt == 3) //Down
            {
                Debug.Log(playerMovement.animAfterLeft);

                meleeBox.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
                if (playerMovement.animAfterLeft == true)
                {
                    meleeBox.rotation = Quaternion.Euler(0f, 0f, 90f);
                }
                else
                {
                    meleeBox.rotation = Quaternion.Euler(0f, 0f, 270f);
                }

            }
            else if (playerMovement.directionInt == 4) //Left
            {
                meleeBox.position = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
                meleeBox.rotation = Quaternion.Euler(0f, 0f, 0f);

            }

        }
    }
}