using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AphroditeFightCode
{
    public class Hestia : MonoBehaviour
    {
        //public GameObject fireballPrefab;

        //public bool canFF;
        //public GameObject FFPrefab;

        //public FFState currentState = FFState.ff1;
        //public Sprite ff_empty;
        //public Sprite ff1_Sprite;
        //public Sprite ff2_Sprite;
        //public Sprite ffdamage_Sprite;
        //private SpriteRenderer fftileSpriteRenderer;
        //private float ffloopEnd;
        //private float ffloopInterval = 10f;

        //public GameObject firetile;

        //public float fireballSpeed = 1f;

        public Animator hestAnim;

        private float loopEnd;
        public float loopInterval = 5f;

        private float holdAnimLoopEnd;
        private float holdAnimLoopInterval = 6f;

        private GameObject player;
        //private SpriteRenderer hestRend;

        private bool HestiaIsLeft;

        public GameObject meteorPrefab;

        public GameObject arr1Prefab;
        public GameObject arr2Prefab;
        public GameObject arr3Prefab;

        private SpriteRenderer arr1SR;
        private SpriteRenderer arr2SR;
        private SpriteRenderer arr3SR;

        private Transform arr1T;
        private Transform arr2T;
        private Transform arr3T;

        public float meteorSpeed;
        public float numOfMeteor;

        public bool theWall;



        public float health = 20f;
        public Image healthBar;
        public Image border;
        public Image damageBar;
        //private bool currPullingWall;

        //public float hasFF = 0f;

        //private GameObject fftile;

        public GameObject leftWall;
        public GameObject rightWall;
        Rigidbody2D leftWallRB;
        Rigidbody2D rightWallRB;

        //public enum FFState
        //{
        //    empty,
        //    ff1,
        //    ff2,
        //    ffdamage
        //}

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            loopEnd = -loopInterval;
            Animator hestAnim = GetComponent<Animator>();
            hestAnim.SetInteger("Hold", 1);

            arr1SR = arr1Prefab.GetComponent<SpriteRenderer>();
            arr2SR = arr2Prefab.GetComponent<SpriteRenderer>();
            arr3SR = arr3Prefab.GetComponent<SpriteRenderer>();

            arr1T = arr1Prefab.GetComponent<Transform>();
            arr2T = arr2Prefab.GetComponent<Transform>();
            arr3T = arr3Prefab.GetComponent<Transform>();
            meteorSpeed = 3f;
            numOfMeteor = 0f;
            theWall = false;

            leftWallRB = leftWall.GetComponent<Rigidbody2D>();
            rightWallRB = rightWall.GetComponent<Rigidbody2D>();

            //HestiaIsLeft = true;
            //currPullingWall = false;
            //ffloopEnd = -ffloopInterval;
            //canFF = false;
            //fftileSpriteRenderer = firetile.GetComponent<SpriteRenderer>();
        }


        void Update()
        {
            //Debug.Log("player.transform.position.x" + player.transform.position.x);
            //Debug.Log("transform.position.x" + transform.position.x);
            changeHestiaDir();
            HestiaAttackAnim();
            if (theWall)
            {
                fireWall();
            }
            changeWallDir();
            updateHealth();
            //FFForUpdate();
        }

        public void updateHealth()
        {
            healthBar.fillAmount = health / 20f; 
        }
        public void TakeDamage(int playerDamage)
        {
            health -= playerDamage;
            Debug.Log("Hestia has taken " + playerDamage + " damage. " + health + " health remaining");
            if (health <= 0)
            {
                //we'll actually do a kill function which will do a death animation,
                //then delete the game object
                Color originalColor = gameObject.GetComponent<SpriteRenderer>().color;
                Color flashColor = Color.red;
                float flashDuration = 0.1f;
                int numberOfFlashes = 2;

                LeanTween.value(gameObject, originalColor, flashColor, flashDuration)
                   .setEase(LeanTweenType.easeInOutSine)
                   .setLoopPingPong(numberOfFlashes)
                   .setOnUpdate((Color val) =>
                   {
                       // Update the object's color during the tween
                       gameObject.GetComponent<SpriteRenderer>().color = val;
                   })
                   .setOnComplete(() =>
                   {
                       // Reset the color to the original after the flash is complete
                       gameObject.GetComponent<SpriteRenderer>().color = originalColor;
                   });
                HestiaDeath();
            }
        }

        private void HestiaDeath()
        {
            Destroy(gameObject);
            Destroy(healthBar);
            Destroy(border);
            Destroy(damageBar);
        }

        void shootMeteor()
        {
            int dirM = Random.Range(1, 5);
            //int dirM = 4;
            
            //from bottom
            if (dirM == 1)
            {
                arr1T.transform.position = new Vector3(-5.45f, -4.5f, 1);
                arr1T.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                arr2T.transform.position = new Vector3(0f, -4.5f, 1);
                arr2T.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                arr3T.transform.position = new Vector3(5.45f, -4.5f, 1);
                arr3T.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                showArrow();
                
                for (float i = 1; i < 5f + numOfMeteor; i++)
                {
                    GameObject bottomMeteor = Instantiate(meteorPrefab, new Vector3(Random.Range(-8f,8f), -10f - i, 0f), Quaternion.identity);
                    Rigidbody2D bottomMeteorRB = bottomMeteor.GetComponent<Rigidbody2D>();
                    bottomMeteorRB.velocity = new Vector3(0f, meteorSpeed, 0f);
                }
                Invoke("hideArrow", 3f);
                numOfMeteor += 1;
                meteorSpeed += 0.2f;

            }
            

            //from top
            if (dirM == 2)
            {
                arr1T.transform.position = new Vector3(-5.45f, 4.5f, 1);
                arr1T.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                arr2T.transform.position = new Vector3(0f, 4.5f, 1);
                arr2T.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                arr3T.transform.position = new Vector3(5.45f, 4.5f, 1);
                arr3T.transform.rotation = Quaternion.Euler(0f, 0f, 180f);

                showArrow();
                for (float i = 1; i < 5f + numOfMeteor; i++)
                {
                    GameObject topMeteor = Instantiate(meteorPrefab, new Vector3(Random.Range(-8f, 8f), 10f + i, 0f), Quaternion.identity);
                    Rigidbody2D topMeteorRB = topMeteor.GetComponent<Rigidbody2D>();
                    topMeteor.transform.Rotate(0f, 0f, 180f);
                    topMeteorRB.velocity = new Vector3(0f, (-1) * meteorSpeed, 0f);
                }
                Invoke("hideArrow", 3f);
                numOfMeteor += 1;
                meteorSpeed += 0.2f;
            }

            //from left
            if (dirM == 3)
            {
                arr1T.transform.position = new Vector3(-8.25f, 2.5f, 1);
                arr1T.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                arr2T.transform.position = new Vector3(-8.25f, 0f, 1);
                arr2T.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                arr3T.transform.position = new Vector3(-8.25f, -2.5f, 1);
                arr3T.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                showArrow();
                for (float i = 1; i < 5f + numOfMeteor; i++)
                {
                    GameObject leftMeteor = Instantiate(meteorPrefab, new Vector3(-14f - i, Random.Range(-4f,4f), 0f), Quaternion.identity);
                    Rigidbody2D leftMeteorRB = leftMeteor.GetComponent<Rigidbody2D>();
                    leftMeteor.transform.Rotate(0f, 0f, -90f);
                    leftMeteorRB.velocity = new Vector3(meteorSpeed, 0f, 0f);
                }
                Invoke("hideArrow", 3f);
                numOfMeteor += 1;
                meteorSpeed += 0.2f;

            }

            //from right
            if (dirM == 4)
            {
                arr1T.transform.position = new Vector3(8.25f, 2.5f, 1);
                arr1T.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                arr2T.transform.position = new Vector3(8.25f, 0f, 1);
                arr2T.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                arr3T.transform.position = new Vector3(8.25f, -2.5f, 1);
                arr3T.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                showArrow();
                
                for (float i = 1; i < 5f + numOfMeteor; i++)
                {
                    GameObject rightMeteor = Instantiate(meteorPrefab, new Vector3(14f + i,Random.Range(-4f,4f), 0f), Quaternion.identity);
                    Rigidbody2D rightMeteorRB = rightMeteor.GetComponent<Rigidbody2D>();
                    rightMeteor.transform.Rotate(0f, 0f, 90f);
                    rightMeteorRB.velocity = new Vector3((-1) * meteorSpeed, 0f, 0f);
                }
                Invoke("hideArrow", 3f);
                numOfMeteor++;
                meteorSpeed += 0.2f;

            }
        }

        void hideArrow()
        {
            arr1SR.enabled = false;
            arr2SR.enabled = false;
            arr3SR.enabled = false;
        }

        void showArrow()
        {
            arr1SR.enabled = true;
            arr2SR.enabled = true;
            arr3SR.enabled = true;
        }

        //void decideArrow()
        //{
        //    int dirM = 1;
        //    if(dirM == 1)
        //    {

        //    }
        //    if (dirM == 2)
        //    {
        //        Debug.Log("showFromUpArrow");
        //    }
        //    if (dirM == 3)
        //    {
        //        Debug.Log("showFromLeftArrow");
        //    }
        //    if (dirM == 4)
        //    {
        //        Debug.Log("showFromRightArrow");
        //    }
        //}

        //void ShootFireball()
        //{
        //    var fireballInitPos1 = new Vector3(0f, 0f, 0f);

        //    if (HestiaIsLeft) { fireballInitPos1 = new Vector3(1.5f, 0f, 0f); }
        //    if (!HestiaIsLeft) { fireballInitPos1 = new Vector3(-1.5f, 0f, 0f); }

        //    GameObject fireballGO = Instantiate(fireballPrefab, fireballInitPos1, Quaternion.identity);
        //    Vector2 direction = player.transform.position;
        //    Rigidbody2D fireballRB = fireballGO.GetComponent<Rigidbody2D>();
        //    fireballRB.velocity = direction * fireballSpeed;
        //}


        void changeHestiaDir()
        {
            if (player.transform.position.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(180f, 0f, 180f);
                HestiaIsLeft = false;
            }
            if (player.transform.position.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                HestiaIsLeft = true;
            }
        }

        //void fireFloor()
        //{
        //    if (hasFF % 20 == 0)
        //    {
        //        var ffTilePos = new Vector3(-3.5f, -2.5f, 0f);
        //        GameObject fftile = Instantiate(FFPrefab, ffTilePos, Quaternion.identity);
        //        //FFForUpdate();
        //        Destroy(fftile, 10f);
        //        hasFF = 1;
        //    }
        //    hasFF += 1;
        //}

        void fWT()
        {
            theWall = true;
        }
        void fWF()
        {
            theWall = false;
        }
        void changeWallDir()
        {
            if (leftWallRB.velocity.x > 0f && leftWall.transform.position.x > -4f)
            {
                leftWallRB.velocity = leftWall.transform.right * -3f;
            }
            if(leftWallRB.velocity.x < 0f && leftWall.transform.position.x < -15f)
            {
                leftWall.transform.position = new Vector3(-15f, 0.06f, 0f);
                leftWallRB.velocity = leftWall.transform.right * 0f;
            }
            
            if (rightWallRB.velocity.x < 0f && rightWall.transform.position.x < 4.5f)
            {
                rightWallRB.velocity = rightWall.transform.right * 3f;
            }
            if (rightWallRB.velocity.x > 0.1f && rightWall.transform.position.x > 15f)
            {
                rightWall.transform.position = new Vector3(15f, 1.06f, 0f);
                rightWallRB.velocity = rightWall.transform.right * 0f;
            }

        }
        void fireWall()
        {
            if (HestiaIsLeft)
            {
                if (leftWallRB.velocity.x == 0f)
                {
                    leftWallRB.velocity = leftWall.transform.right * 3f;
                }
            }
            if (!HestiaIsLeft)
            {
                if(rightWallRB.velocity.x == 0f)
                {
                    Debug.Log("TRUE");
                    rightWallRB.velocity = rightWall.transform.right * -3f;
                }
            }




            //if (!HestiaIsLeft)
            //{
            //    //If wall not moving, set it to left
            //    if (leftFireWallRB.velocity.x == 0f)
            //    {
            //        leftWall.transform.position = new Vector3(-17f, 0f, 0f);
            //    }

            //    //If it's on the left side, move it from the left
            //    if (leftWall.transform.position.x < -6f && leftFireWallRB.velocity.x > 0f)
            //    {
            //        leftFireWallRB.velocity = leftWall.transform.right * 1.5f;
            //    }


            //    //If the left wall is moving back and it's to the left of it's initial position, freeze the velocity
            //    //if (leftFireWallRB.velocity.x < 0f && leftWall.transform.position.x <= -17f) { leftFireWallRB.velocity = new Vector2(0f, 0f); }
            //}
            //if(leftWall.transform.position.x >= -6f)
            //{
            //    leftFireWallRB.velocity = leftWall.transform.right * -1.5f;
            //}


            //if (HestiaIsLeft)
            //{
            //    //If wall not moving, set it to right
            //    if (rightFireWallRB.velocity.x == 0f) { rightWall.transform.position = new Vector3(17f, 0f, 0f); }

            //    //If it's on the left side, move it from the left
            //    if (rightWall.transform.position.x >= 6f) { rightFireWallRB.velocity = rightWall.transform.right * -1.5f; }
            //}




            ////If it has passed the cave-in time, then move backwards
            //if (rightWall.transform.position.x < 6f) { rightFireWallRB.velocity = rightWall.transform.right * 1.5f; }

            ////If the left wall is moving back and it's to the right of it's initial position, freeze the velocity
            //if (rightFireWallRB.velocity.x > 0f && rightWall.transform.position.x >= 17f)
            //{
            //    rightWall.transform.position = new Vector3(17f, 0f, 0f);
            //    rightFireWallRB.velocity = new Vector2(0f, 0f);
            //}

            //if (leftFireWallRB.velocity.x < 0f && leftWall.transform.position.x <= -17f)
            //{
            //    leftWall.transform.position = new Vector3(-17f, 0f, 0f);
            //    leftFireWallRB.velocity = new Vector2(0f, 0f);
            //}

            ////If it has passed the cave-in time, then move backwards
            ////if (leftWall.transform.position.x > -8f) { leftFireWallRB.velocity = leftWall.transform.right * -1.5f; }


        }

        void HestiaAttackAnim()
        {
            if (Time.time - loopEnd >= loopInterval)
            {
                int statePicker = 1;

                //int statePicker = Random.Range(1, 4);
                //int statePicker = Random.Range(1, 3);
                Debug.Log("statePicker" + statePicker);

                //if (statePicker == 1)
                //{
                //    hestAnim.SetInteger("State", 1);
                //    hestAnim.SetInteger("Hold", 1);
                //}
                if (statePicker == 2)
                {
                    hestAnim.SetInteger("State", 2);
                    hestAnim.SetInteger("Hold", 1);
                }
                if (statePicker == 1)
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
