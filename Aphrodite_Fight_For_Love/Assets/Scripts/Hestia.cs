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

        public FFState currentState = FFState.ff1;
        public Sprite ff_empty;
        public Sprite ff1_Sprite;
        public Sprite ff2_Sprite;
        public Sprite ffdamage_Sprite;
        private SpriteRenderer fftileSpriteRenderer;
        private float ffloopEnd;
        private float ffloopInterval = 10f;

        public GameObject firetile;

        public float fireballSpeed = 1f;

        public Animator hestAnim;

        private float loopEnd;
        public float loopInterval = 20f;

        private float holdAnimLoopEnd;
        private float holdAnimLoopInterval = 20f;

        private GameObject player;
        //private SpriteRenderer hestRend;

        private bool HestiaIsLeft;
        private bool currPullingWall;

        public float hasFF = 0f;

        //private GameObject fftile;

        public GameObject leftWall;
        public GameObject rightWall;

        public enum FFState
        {
            empty,
            ff1,
            ff2,
            ffdamage
        }

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            loopEnd = -loopInterval;
            Animator hestAnim = GetComponent<Animator>();
            hestAnim.SetInteger("Hold", 1);
            HestiaIsLeft = true;
            currPullingWall = false;
            ffloopEnd = -ffloopInterval;
            canFF = false;
            fftileSpriteRenderer = firetile.GetComponent<SpriteRenderer>();
        }


        void Update()
        {
            //Debug.Log("player.transform.position.x" + player.transform.position.x);
            //Debug.Log("transform.position.x" + transform.position.x);
            changeHestiaDir();
            HestiaAttackAnim();
            //FFForUpdate();



        }
        void ShootFireball()
        {
            var fireballInitPos1 = new Vector3(0f, 0f, 0f);

            if (HestiaIsLeft) { fireballInitPos1 = new Vector3(1.5f, 0f, 0f); }
            if (!HestiaIsLeft) { fireballInitPos1 = new Vector3(-1.5f, 0f, 0f); }

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
                HestiaIsLeft = true;
            }
            if (player.transform.position.x < transform.position.x && currPullingWall == false)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                HestiaIsLeft = false;
            }
        }

        void fireFloor()
        {
            if (hasFF % 20 == 0)
            {
                var ffTilePos = new Vector3(-3.5f, -2.5f, 0f);
                GameObject fftile = Instantiate(FFPrefab, ffTilePos, Quaternion.identity);
                //FFForUpdate();
                Destroy(fftile, 10f);
                hasFF = 1;
            }
            hasFF += 1;
        }

        void fireWall()
        {
            Rigidbody2D leftFireWallRB = leftWall.GetComponent<Rigidbody2D>();
            Rigidbody2D rightFireWallRB = rightWall.GetComponent<Rigidbody2D>();

            if (!HestiaIsLeft)
            {
                //If wall not moving, set it to left
                if (leftFireWallRB.velocity.x == 0f) { leftWall.transform.position = new Vector3(-17f, 0f, 0f); }

                //If it's on the left side, move it from the left
                if (leftWall.transform.position.x <= -17f) { leftFireWallRB.velocity = leftWall.transform.right * 1.5f; }


                //If the left wall is moving back and it's to the left of it's initial position, freeze the velocity
                //if (leftFireWallRB.velocity.x < 0f && leftWall.transform.position.x <= -17f) { leftFireWallRB.velocity = new Vector2(0f, 0f); }
            }


            if (HestiaIsLeft)
            {
                //If wall not moving, set it to right
                if (rightFireWallRB.velocity.x == 0f) { rightWall.transform.position = new Vector3(17f, 0f, 0f); }

                //If it's on the left side, move it from the left
                if (rightWall.transform.position.x >= 17f) { rightFireWallRB.velocity = rightWall.transform.right * -1.5f; }
            }




            //If it has passed the cave-in time, then move backwards
            if (rightWall.transform.position.x < 8f) { rightFireWallRB.velocity = rightWall.transform.right * 1.5f; }

            //If the left wall is moving back and it's to the right of it's initial position, freeze the velocity
            if (rightFireWallRB.velocity.x > 0f && rightWall.transform.position.x >= 17f)
            {
                rightWall.transform.position = new Vector3(17f, 0f, 0f);
                rightFireWallRB.velocity = new Vector2(0f, 0f);
            }

            if (leftFireWallRB.velocity.x < 0f && leftWall.transform.position.x <= -17f)
            {
                leftWall.transform.position = new Vector3(-17f, 0f, 0f);
                leftFireWallRB.velocity = new Vector2(0f, 0f);
            }

            //If it has passed the cave-in time, then move backwards
            if (leftWall.transform.position.x > -8f) { leftFireWallRB.velocity = leftWall.transform.right * -1.5f; }


        }

        void HestiaAttackAnim()
        {
            if (Time.time - loopEnd >= loopInterval)
            {
                int statePicker = 1;
                //int statePicker = Random.Range(1, 4);
                //int statePicker = Random.Range(1, 3);
                Debug.Log("statePicker" + statePicker);

                if (statePicker == 1)
                {
                    hestAnim.SetInteger("State", 1);
                    hestAnim.SetInteger("Hold", 1);
                }
                if (statePicker == 2)
                {
                    hestAnim.SetInteger("State", 2);
                    hestAnim.SetInteger("Hold", 1);
                }
                if (statePicker == 3)
                {
                    hestAnim.SetInteger("State", 3);
                    hestAnim.SetInteger("Hold", 1);
                    //fireWall();
                }
                loopEnd = Time.time;
                if (Time.time - holdAnimLoopEnd >= holdAnimLoopInterval)
                {
                    holdAnimLoopEnd = Time.time;
                    hestAnim.SetInteger("Hold", 0);
                }
            }
        }

        //void FFForUpdate()
        //{
        //    if (Time.time - ffloopEnd > 2f)
        //    {
        //        ChangeState(FFState.ff1);
        //        UpdateSprite();
        //        Debug.Log("FFState.ff1");
        //    }
        //    if (Time.time - ffloopEnd >= 4f)
        //    {
        //        ChangeState(FFState.ff2);
        //        UpdateSprite();
        //        Debug.Log("FFState.ff2");
        //    }
        //    if (Time.time - ffloopEnd >= 6f)
        //    {
        //        ChangeState(FFState.ffdamage);
        //        UpdateSprite();
        //        Debug.Log("ffdamage");
        //    }
        //    if (Time.time - ffloopEnd > ffloopInterval)
        //    {
        //        ChangeState(FFState.empty);
        //        UpdateSprite();
        //        Debug.Log("FFState.empty");
        //        ffloopEnd = loopEnd;
        //    }
        //}


        //void ChangeState(FFState newTileState)
        //{
        //    if (currentState != newTileState) { currentState = newTileState; }
        //}

        //void UpdateSprite()
        //{
        //    switch (currentState)
        //    {
        //        case FFState.empty:
        //            fftileSpriteRenderer.sprite = ff_empty;
        //            Debug.Log("ff_empty");
        //            break;
        //        case FFState.ff1:
        //            fftileSpriteRenderer.sprite = ff1_Sprite;
        //            Debug.Log("ff1_Sprite");
        //            break;
        //        case FFState.ff2:
        //            fftileSpriteRenderer.sprite = ff2_Sprite;
        //            Debug.Log("ff2_Sprite");
        //            break;
        //        case FFState.ffdamage:
        //            fftileSpriteRenderer.sprite = ffdamage_Sprite;
        //            Debug.Log("ffdamage_Sprite");
        //            break;
        //    }
        //}
    }
}
