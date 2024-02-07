using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class PlayerAttack : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        public Transform meleeBox;
        public Transform attackBox;
        public LayerMask enemyLayers;
        public Vector3 curPosition;

        Vector2 rectangleSize = new Vector2(0.9f, 0.45f);

        void Start()
        {
            meleeBox = transform.Find("MeleeBox");
        }
        // Update is called once per frame
        void Update()
        {
            if (playerMovement.directionInt == 1) //Up
            {
                meleeBox.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        //        Debug.Log(playerMovement.animAfterLeft);
                //if (playerMovement.animAfterLeft == true)
                //{
                meleeBox.rotation = Quaternion.Euler(0f, 0f, 270f);
                //}
                //else
                //{
                //    meleeBox.rotation = Quaternion.Euler(0f, 0f, 90f);
                //}
            }
            else if (playerMovement.directionInt == 2) //Right
            {
                meleeBox.position = new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z);
                meleeBox.rotation = Quaternion.Euler(0f, 180f, 180f);

            }
            else if (playerMovement.directionInt == 3) //Down
            {
        //        Debug.Log(playerMovement.animAfterLeft);

                meleeBox.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
                //if (playerMovement.animAfterLeft == true)
                //{
                meleeBox.rotation = Quaternion.Euler(0f, 0f, 90f);
                //}
                //else
                //{
                //    meleeBox.rotation = Quaternion.Euler(0f, 0f, 270f);
                //}

            }
            else if (playerMovement.directionInt == 4) //Left
            {
                meleeBox.position = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
                meleeBox.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                curPosition = meleeBox.position;
                Attack();
                Debug.Log("Attack!");
            }

        }
        void Attack()
        {
            // do correct animation
            //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeBox.position, 4.0f, enemyLayers);

            Debug.Log(meleeBox.position + "cur pos" + enemyLayers.ToString());

            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackBox.position, rectangleSize, 0f, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit an enemy!" + enemy.name);
            }
        }

        private void OnDrawGizmosSelected()
        {
            //    Gizmos.DrawWireCube(attackBox.position, new Vector2(1.1f, 0.6f));
            Gizmos.DrawWireSphere(meleeBox.position, 0.5f);
            //    Gizmos.DrawCube(curPosition, new Vector2(2f, 2f));

        }
    }
}