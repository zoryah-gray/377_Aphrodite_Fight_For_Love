using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class Hestia : MonoBehaviour
    {
        public GameObject fireballPrefab;

        public bool canFF;
        public GameObject FFPrefab;

        public float fireballSpeed = 1f;

        public Animator hestAnim;
        
        private float loopEnd;
        public float loopInterval = 20f;

        private float holdAnimLoopEnd;
        private float holdAnimLoopInterval = 20f;

        private GameObject player;
        //private SpriteRenderer hestRend;

        private bool fireballIsLeft;
        private bool currPullingWall;

        //private GameObject fftile;

        public GameObject leftWall;
        //public GameObject rightWall;





        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            loopEnd = -loopInterval;
            Animator hestAnim = GetComponent<Animator>();
            hestAnim.SetInteger("Hold",1);
            fireballIsLeft = true;
            currPullingWall = false;
        }

       
        void Update()
        {
            //Debug.Log("player.transform.position.x" + player.transform.position.x);
            //Debug.Log("transform.position.x" + transform.position.x);
            changeHestiaDir();
            HestiaAttackAnim();
           
        }
        void ShootFireball()
        {
            
            var fireballInitPos1 = new Vector3(0f, 0f, 0f);

            if (fireballIsLeft) { fireballInitPos1 = new Vector3(1.5f, 0f, 0f); }
            if (!fireballIsLeft) { fireballInitPos1 = new Vector3(-1.5f, 0f, 0f); }
            
            GameObject fireballGO = Instantiate(fireballPrefab, fireballInitPos1, Quaternion.identity);
            Vector2 direction = player.transform.position;
            Rigidbody2D fireballRB = fireballGO.GetComponent<Rigidbody2D>();
            fireballRB.velocity = direction * fireballSpeed;
        }


        void changeHestiaDir()
        {

            if (player.transform.position.x > transform.position.x && currPullingWall == false)
            {
                transform.rotation = Quaternion.Euler(180f, 0f, 180f);
                fireballIsLeft = true;
            }
            if (player.transform.position.x < transform.position.x && currPullingWall == false)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                fireballIsLeft = false;
            }
        }

        void fireFloor()
        {
            for (var i = 0f; i < 5f; i++)
            {
                var ffTilePos = new Vector3(-3.5f + (i * 2), -2.5f, 0f);
                GameObject fftile = Instantiate(FFPrefab, ffTilePos, Quaternion.identity);
                if (hestAnim.GetInteger("State") == 2 && hestAnim.GetInteger("Hold") == 0)
                {
                    Destroy(fftile, 3f);
                }
            }
            
        }

        void fireWall()
        {
            currPullingWall = true;
            Rigidbody2D leftFireWallRB = leftWall.GetComponent<Rigidbody2D>();
            if(leftWall.transform.position.x < 0f && leftFireWallRB.velocity.x >= 0f)
            {
                leftFireWallRB.velocity = transform.right * 0.5f;
            }
            if(leftWall.transform.position.x < -11f && leftFireWallRB.velocity.x < 0f)
            {
                leftFireWallRB.velocity = transform.right * -0.5f;
            }

        }

        void HestiaAttackAnim()
        {
            if (Time.time - loopEnd >= loopInterval)
            {
                //int statePicker = 3;
                //int statePicker = Random.Range(1, 3);

                int statePicker = Random.Range(1, 4);
                Debug.Log("statePicker" + statePicker);

                if (statePicker == 1)
                {
                    hestAnim.SetInteger("State", 1);
                    hestAnim.SetInteger("Hold", 1);
                    if (hestAnim.GetInteger("State") == 1 && hestAnim.GetInteger("Hold") == 1)
                    {
                        Invoke("ShootFireball", 0f);
                    }
                }
                if (statePicker == 2)
                {
                    hestAnim.SetInteger("State", 2);
                    hestAnim.SetInteger("Hold", 1);

                    if (hestAnim.GetInteger("State") == 2 && hestAnim.GetInteger("Hold") == 1)
                    {
                        Invoke("fireFloor",0f);
                    }
                }
                if (statePicker == 3)
                {
                    hestAnim.SetInteger("State", 3);
                    hestAnim.SetInteger("Hold", 1);
                    fireWall();
                }
                loopEnd = Time.time;
                if (Time.time - holdAnimLoopEnd >= holdAnimLoopInterval)
                {
                    holdAnimLoopEnd = Time.time;
                    hestAnim.SetInteger("Hold", 0);
                }
            }
        }
    }
}
