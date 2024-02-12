using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
        //public GameObject bullet;
        //public float bulletSpeed = 5f;
        public UnityEngine.Transform attackBox;
        public LayerMask enemyLayers;
        public Vector3 curPosition;
        public bool isAttacking;
        //private float lastShotGun;
        //public float gunInterval = 1f;
      
        //private bool canAttack = true;        
        //private bool isPlaying;

        Vector2 rectangleSize = new Vector2(0.9f, 0.45f);
        //private bool isPlayingSwingAnim = true;

        void Start()
        {
            Animator meleeBoxAnimator = meleeBoxGO.GetComponent<Animator>();
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                meleeBoxGO.GetComponent<Animator>().SetBool("canAnimAttack",true);
                curPosition = meleeBoxGO.transform.position;
                Attack();
                Debug.Log("Attack!");
            }
            //if (meleeBoxAnimator.GetCurrentAnimatorStateInfo(0).length >
            //meleeBoxAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime &&



            //    AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName)

            //    GetCurrentAnimatorStateInfo(0).IsName("canAttackAnim"))

            if (Input.GetKeyUp(KeyCode.Space))
            {
                meleeBoxGO.GetComponent<Animator>().SetBool("animAttack", false);
                //meleeBoxGO.GetComponent<Animator>().SetBool("animCanAttack", false);
                //UnAttack();
            }
            //if (Input.GetKeyDown(KeyCode.C) && Time.time - lastShotGun >= gunInterval)
            //{
            //    ShootGun();
            //    lastShotGun = Time.time;
            //}
        }


        //void ShootGun()
        //{
        //    var bullInitPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //    GameObject bulletGO = Instantiate(bullet, bullInitPos, Quaternion.identity);
        //    Rigidbody2D bulletRB = bulletGO.GetComponent<Rigidbody2D>();

        //    if (playerMovement.directionInt == 1) //Up
        //    {
        //        bulletRB.velocity = transform.up * bulletSpeed;
        //    }
        //    else if (playerMovement.directionInt == 2) //Right
        //    {
        //        bulletGO.transform.position = new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z);
        //        bulletGO.transform.rotation = Quaternion.Euler(0f, 180f, 180f);
        //    }
        //    else if (playerMovement.directionInt == 3) //Down
        //    {
        //        bulletGO.transform.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
        //        bulletGO.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        //    }
        //    else if (playerMovement.directionInt == 4) //Left
        //    {
        //        bulletGO.transform.position = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
        //        bulletGO.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        //    }
        //}

        void Attack()
        {
            // do correct animation
            //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeBox.position, 4.0f, enemyLayers);
            if (playerMovement.directionInt == 1) //Up
            {
                meleeBoxGO.transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
                meleeBoxGO.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
            }
            else if (playerMovement.directionInt == 2) //Right
            {
                meleeBoxGO.transform.position = new Vector3(transform.position.x + 0.4f, transform.position.y, transform.position.z);
                meleeBoxGO.transform.rotation = Quaternion.Euler(0f, 180f, 180f);
            }
            else if (playerMovement.directionInt == 3) //Down
            {
                meleeBoxGO.transform.position = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
                meleeBoxGO.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
            else if (playerMovement.directionInt == 4) //Left
            {
                meleeBoxGO.transform.position = new Vector3(transform.position.x - 0.4f, transform.position.y, transform.position.z);
                meleeBoxGO.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            Debug.Log(meleeBoxGO.transform.position + "cur pos" + enemyLayers.ToString());

            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackBox.position, rectangleSize, 0f, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit an enemy!" + enemy.name);
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
