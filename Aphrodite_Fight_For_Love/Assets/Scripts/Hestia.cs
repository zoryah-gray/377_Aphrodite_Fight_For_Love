using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AphroditeFightCode
{
    public class Hestia : MonoBehaviour
    {


        public Animator hestAnim;

        private float loopEnd;
        public float loopInterval = 5f;

        private float holdAnimLoopEnd;
        private float holdAnimLoopInterval = 6f;

        private GameObject player;


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

        public bool isHestiaDead;

        public float health = 40f;
        public Image healthBar;
        public Image border;
        public Image damageBar;
        public GameObject sceneTrig; 


        public GameObject leftWall;
        public GameObject rightWall;
        Rigidbody2D leftWallRB;
        Rigidbody2D rightWallRB;

        public GameObject hestHealthBufferCollider;

        [Header("Audio Files")]
        public AudioClip deathClip;
        public AudioClip fireballClip;
        public AudioClip wallClip;

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
            meteorSpeed = 5f;
            numOfMeteor = 10f;
            theWall = false;

            leftWallRB = leftWall.GetComponent<Rigidbody2D>();
            rightWallRB = rightWall.GetComponent<Rigidbody2D>();

            isHestiaDead = false;

            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<Collider2D>().enabled = true;
            hestHealthBufferCollider.GetComponent<Collider2D>().enabled = true;

        }


        void Update()
        {
            //Debug.Log("player.transform.position.x" + player.transform.position.x);
            //Debug.Log("transform.position.x" + transform.position.x);
            changeHestiaDir();
            //if (!isHestiaDead)
            //{
            HestiaAttackAnim();
            //}
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
            healthBar.fillAmount = health / 40f; 
        }
        public void hestDeathAnim()
        {
            Color originalColor = gameObject.GetComponent<SpriteRenderer>().color;
            float flashDuration = 0.1f;
            int numberOfFlashes = 2; // This determines how many times it will ping-pong

            // Create a transparent version of the original color (fully transparent in this case)
            Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
            AudioSource.PlayClipAtPoint(deathClip, transform.localPosition);
            // Perform the ping-pong animation
            LeanTween.value(gameObject, originalColor.a, transparentColor.a, flashDuration)
                .setEase(LeanTweenType.easeInOutSine)
                .setLoopPingPong(numberOfFlashes)
                .setOnUpdate((float val) => {
                    // Update the object's color with the new alpha value during the tween
                    Color currentColor = gameObject.GetComponent<SpriteRenderer>().color;
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g, currentColor.b, val);
                })
                .setOnComplete(() => {
                    // After the ping-pong effect, transition to fully transparent permanently
                    LeanTween.value(gameObject, transparentColor.a, 0f, flashDuration)
                        .setOnUpdate((float val) => {
                            Color currentColor = gameObject.GetComponent<SpriteRenderer>().color;
                            gameObject.GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g, currentColor.b, val);
                        })
                        .setOnComplete(() => {
                            // Ensure the player object is fully transparent after all animations are complete
                            gameObject.GetComponent<SpriteRenderer>().color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
                        });
                });

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
            //isHestiaDead = true;
            hestDeathAnim();
            //gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            hestHealthBufferCollider.GetComponent<Collider2D>().enabled = false;
            isHestiaDead = true;
            //Destroy(gameObject);
            Destroy(healthBar);
            Destroy(border);
            Destroy(damageBar);
            sceneTrig.SetActive(true);
        }

        void shootMeteor()
        {
            int dirM = Random.Range(1, 5);
            //int dirM = 1;
            
            //from bottom
            if (dirM == 1 && !isHestiaDead)
            {
                arr1T.transform.position = new Vector3(-5.45f, -7.25f, 1);
                arr1T.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                arr2T.transform.position = new Vector3(0f, -7.25f, 1);
                arr2T.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                arr3T.transform.position = new Vector3(5.45f, -7.25f, 1);
                arr3T.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                showArrow();
                
                for (float i = 1; i < 5f + numOfMeteor; i++)
                {
                    GameObject bottomMeteor = Instantiate(meteorPrefab, new Vector3(Random.Range(-9f,9f), -10f - i, 0f), Quaternion.identity);
                    Rigidbody2D bottomMeteorRB = bottomMeteor.GetComponent<Rigidbody2D>();
                    bottomMeteorRB.velocity = new Vector3(0f, meteorSpeed, 0f);
                    AudioSource.PlayClipAtPoint(fireballClip, transform.localPosition);
                }
                Invoke("hideArrow", 3f);
                numOfMeteor += 1;
                meteorSpeed += 0.2f;

            }
            

            //from top
            if (dirM == 2 && !isHestiaDead)
            {
                arr1T.transform.position = new Vector3(-5.45f, 7.25f, 1);
                arr1T.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                arr2T.transform.position = new Vector3(0f, 7.25f, 1);
                arr2T.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                arr3T.transform.position = new Vector3(5.45f, 7.25f, 1);
                arr3T.transform.rotation = Quaternion.Euler(0f, 0f, 180f);

                showArrow();
                for (float i = 1; i < 5f + numOfMeteor; i++)
                {
                    GameObject topMeteor = Instantiate(meteorPrefab, new Vector3(Random.Range(-9f, 9f), 10f + i, 0f), Quaternion.identity);
                    Rigidbody2D topMeteorRB = topMeteor.GetComponent<Rigidbody2D>();
                    topMeteor.transform.Rotate(0f, 0f, 180f);
                    topMeteorRB.velocity = new Vector3(0f, (-1) * meteorSpeed, 0f);
                    AudioSource.PlayClipAtPoint(fireballClip, transform.localPosition);
                }
                Invoke("hideArrow", 3f);
                numOfMeteor += 1;
                meteorSpeed += 0.2f;
            }

            //from left
            if (dirM == 3 && !isHestiaDead)
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
                    GameObject leftMeteor = Instantiate(meteorPrefab, new Vector3(-14f - i, Random.Range(-7f,7f), 0f), Quaternion.identity);
                    Rigidbody2D leftMeteorRB = leftMeteor.GetComponent<Rigidbody2D>();
                    leftMeteor.transform.Rotate(0f, 0f, -90f);
                    leftMeteorRB.velocity = new Vector3(meteorSpeed, 0f, 0f);
                    AudioSource.PlayClipAtPoint(fireballClip, transform.localPosition);
                }
                Invoke("hideArrow", 3f);
                numOfMeteor += 1;
                meteorSpeed += 0.2f;

            }

            //from right
            if (dirM == 4 && !isHestiaDead)
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
                    GameObject rightMeteor = Instantiate(meteorPrefab, new Vector3(14f + i,Random.Range(-7f,7f), 0f), Quaternion.identity);
                    Rigidbody2D rightMeteorRB = rightMeteor.GetComponent<Rigidbody2D>();
                    rightMeteor.transform.Rotate(0f, 0f, 90f);
                    rightMeteorRB.velocity = new Vector3((-1) * meteorSpeed, 0f, 0f);
                    AudioSource.PlayClipAtPoint(fireballClip, transform.localPosition);
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
            if (HestiaIsLeft && !isHestiaDead)
            {
                if (leftWallRB.velocity.x == 0f)
                {
                    AudioSource.PlayClipAtPoint(wallClip, transform.localPosition);
                    leftWallRB.velocity = leftWall.transform.right * 3f;
                }
            }
            if (!HestiaIsLeft && !isHestiaDead)
            {
                if(rightWallRB.velocity.x == 0f)
                {
                    AudioSource.PlayClipAtPoint(wallClip, transform.localPosition);
                    Debug.Log("TRUE");
                    rightWallRB.velocity = rightWall.transform.right * -3f;
                }
            }
        }

        void HestiaAttackAnim()
        {
            if (Time.time - loopEnd >= loopInterval)
            {
                //int statePicker = 1;

                //int statePicker = Random.Range(1, 4);
                int statePicker = Random.Range(1, 3);

                Debug.Log("statePicker" + statePicker);

                //if (statePicker == 1)
                //{
                //    hestAnim.SetInteger("State", 1);
                //    hestAnim.SetInteger("Hold", 1);
                //}
                if (statePicker == 2 && !isHestiaDead) //Meteor
                {
                    hestAnim.SetInteger("State", 2);
                    hestAnim.SetInteger("Hold", 1);
                }
                if (statePicker == 1 && !isHestiaDead)
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
    }
}
