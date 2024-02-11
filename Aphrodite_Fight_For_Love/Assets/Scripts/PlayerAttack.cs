using System.Collections;
using System.Collections.Generic;
using AphroditeFightCode;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace AphroditeFightCode
{
    public class PlayerAttack : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        //public UnityEngine.Transform meleeBox;
        public GameObject meleeBoxGO;
        public UnityEngine.Transform attackBox;
        public LayerMask enemyLayers;
        public Vector3 curPosition;
        //private bool canAttack = true;        
        //private bool isPlaying;

        Vector2 rectangleSize = new Vector2(0.9f, 0.45f);
        //private bool isPlayingSwingAnim = true;

        void Start()
        {
        }
        // Update is called once per frame
        void Update()
        {
            if (playerMovement.directionInt == 1) //Up
            {
                meleeBoxGO.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
                meleeBoxGO.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
            }
            else if (playerMovement.directionInt == 2) //Right
            {
                meleeBoxGO.transform.position = new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z);
                meleeBoxGO.transform.rotation = Quaternion.Euler(0f, 180f, 180f);
            }
            else if (playerMovement.directionInt == 3) //Down
            {
                meleeBoxGO.transform.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
                meleeBoxGO.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
            else if (playerMovement.directionInt == 4) //Left
            {
                meleeBoxGO.transform.position = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
                meleeBoxGO.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                meleeBoxGO.GetComponent<Animator>().SetBool("animCanAttack",true);
                curPosition = meleeBoxGO.transform.position;
                Attack();
                Debug.Log("Attack!");
 
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                meleeBoxGO.GetComponent<Animator>().SetBool("animCanAttack", false);
            }
            
        }
        

        void Attack()
        {
            // do correct animation
            //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeBox.position, 4.0f, enemyLayers);

            Debug.Log(meleeBoxGO.transform.position + "cur pos" + enemyLayers.ToString());

            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackBox.position, rectangleSize, 0f, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit an enemy!" + enemy.name);
                enemy.GetComponentInParent<MinionScript>().TakeDamage(1);
            }
        }

        private void OnDrawGizmosSelected()
        {
            //    Gizmos.DrawWireCube(attackBox.position, new Vector2(1.1f, 0.6f));
            Gizmos.DrawWireSphere(meleeBoxGO.transform.position, 0.5f);
            //    Gizmos.DrawCube(curPosition, new Vector2(2f, 2f));

        }
    }
}






//Transform:
//void Start()
//{
//    meleeBox = transform.Find("MeleeBox");
//}
//// Update is called once per frame
//void Update()
//{
//    if (playerMovement.directionInt == 1) //Up
//    {
//        meleeBox.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
//        meleeBox.rotation = Quaternion.Euler(0f, 0f, 270f);
//    }
//    else if (playerMovement.directionInt == 2) //Right
//    {
//        meleeBox.position = new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z);
//        meleeBox.rotation = Quaternion.Euler(0f, 180f, 180f);
//    }
//    else if (playerMovement.directionInt == 3) //Down
//    {
//        meleeBox.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
//        meleeBox.rotation = Quaternion.Euler(0f, 0f, 90f);
//    }
//    else if (playerMovement.directionInt == 4) //Left
//    {
//        meleeBox.position = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
//        meleeBox.rotation = Quaternion.Euler(0f, 0f, 0f);
//    }
