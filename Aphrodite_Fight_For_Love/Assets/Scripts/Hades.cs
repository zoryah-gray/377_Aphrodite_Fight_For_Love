using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

namespace AphroditeFightCode
{
    public class Hades : MonoBehaviour
    {


        public GameObject hadesBulletPrefab;
        public CameraShake CameraScrpt;
        [SerializeField]

        public GameObject sceneTrig;
       
        public float hadesBulletSpeed = 1f;

        public Animator hadesAnim;

        private float loopEnd;
        private float loopInterval = 7f;

        private float holdAnimLoopEnd;
        private float holdAnimLoopInterval = 6f;

        private GameObject player;
        //private SpriteRenderer hestRend;

        private bool HadesIsLeft;

        public GameObject minionPrefab1;
        public GameObject minionPrefab2;
        public GameObject minionPrefab3;
        public GameObject minionPrefab4;

        public float health = 40f;
        public Image healthBar;
        public Image border;
        public Image damageBar;
        public Collider2D healthBarCol;

        public bool isHadesDead = false;
        public GameObject hadesHealthBufferCollider;

        [Header("Audio Files")]
        public AudioClip deathClip;
        public AudioClip shootClip;
        public AudioClip spawnClip;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            loopEnd = -loopInterval;
            holdAnimLoopEnd -= holdAnimLoopInterval;
            hadesAnim = GetComponent<Animator>();
            hadesAnim.SetInteger("Hold", 1);

            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<Collider2D>().enabled = true;
            hadesHealthBufferCollider.GetComponent<Collider2D>().enabled = true;

        }


        void Update()
        {
            //Debug.Log("player.transform.position.x" + player.transform.position.x);
            //Debug.Log("transform.position.x" + transform.position.x);
            changeHadesDir();
            HadesAttackAnim();
            updateHealth();
            //if (isHadesDead) 
            //{
                

            //}
        }
        public void hadesDeathAnim()
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
        public void updateHealth()
        {
            healthBar.fillAmount = health / 40f; // change if health changes 
        }

        public void TakeDamage(int playerDamage)
        {
            health -= playerDamage;
            Debug.Log("Hades has taken " + playerDamage + " damage. " + health + " health remaining");
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
                HadesDeath();
            }
        }
        private void HadesDeath()
        {
            //gameObject.GetComponent<SpriteRenderer>().enabled = false;
            hadesDeathAnim();
            gameObject.GetComponent<Collider2D>().enabled = false;
            hadesHealthBufferCollider.GetComponent<Collider2D>().enabled = false;
            isHadesDead = true;
            Destroy(healthBar);
            Destroy(border);
            Destroy(damageBar);
            Destroy(healthBarCol);
            Destroy(hadesBulletPrefab);
            //Destroy(gameObject);
            sceneTrig.SetActive(true);

            //Destroy(minionPrefab1);
            //Destroy(minionPrefab2);
            //Destroy(minionPrefab3);
            //Destroy(minionPrefab4);



           
        }
        public void SpawnHadesEnemies()
        {
            AudioSource.PlayClipAtPoint(spawnClip, transform.localPosition);
            Instantiate(minionPrefab1, new Vector2(Random.Range(-7f, -1.5f), Random.Range(0f, 3.5f)), Quaternion.identity).GetComponent<AIDestinationSetter>().target = player.transform;
        }
        public void HadesSpawn2()
        {
           Instantiate(minionPrefab2, new Vector2(Random.Range(-7f, -1.5f), Random.Range(-3.5f, 0f)), Quaternion.identity).GetComponent<AIDestinationSetter>().target = player.transform;
        }

        public void HadesSpawn3()
        {
            Instantiate(minionPrefab3, new Vector2(Random.Range(1.5f, 7f), Random.Range(-3.5f, 0f)), Quaternion.identity).GetComponent<AIDestinationSetter>().target = player.transform;
        }

        public void HadesSpawn4()
        {
            Instantiate(minionPrefab4, new Vector2(Random.Range(1.5f, 7f), Random.Range(0f, 3.5f)), Quaternion.identity).GetComponent<AIDestinationSetter>().target = player.transform;
        }



        public void ShootHadesBullet()
        {
            
            var hadesBulletInitPos1 = new Vector3(0f, 0f, 0f);
           
            if (!HadesIsLeft) { hadesBulletInitPos1 = new Vector3(1.9f, -1f, 0f); }
            if (HadesIsLeft) { hadesBulletInitPos1 = new Vector3(-1.9f, -1f, 0f); }
            AudioSource.PlayClipAtPoint(shootClip, transform.localPosition);
            GameObject hadesBulletGO = Instantiate(hadesBulletPrefab, hadesBulletInitPos1, Quaternion.identity);
            
            Vector2 direction = player.transform.position - hadesBulletInitPos1;

            //if (player.transform.position.y > 0f) { direction.y += 0.5f; }
            //if (player.transform.position.y < 0f) { direction.y -= 0.5f; }
            //if (player.transform.position.x > -4f && player.transform.position.x < 0f) { direction.x += 1f; }
            //if (player.transform.position.x < 4f && player.transform.position.x > 0f) { direction.x -= 1f; }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation2 = Quaternion.Euler(0f, 0f, angle);

            hadesBulletGO.transform.rotation = rotation2;
            //hadesBulletGO.transform.LookAt(direction);
            Rigidbody2D hadesBulletRB = hadesBulletGO.GetComponent<Rigidbody2D>();
            hadesBulletRB.velocity = direction * hadesBulletSpeed;
           
        }


        void changeHadesDir()
        {
            if (player.transform.position.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(180f, 0f, 180f);
                HadesIsLeft = false;
            }
            if (player.transform.position.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                HadesIsLeft = true;
            }
        }

        void HadesAttackAnim()
        {
            if (Time.time - loopEnd >= loopInterval)
            {
                int statePicker = 1;
                //int statePicker = Random.Range(1, 3);
                Debug.Log("statePicker" + statePicker);

                if (statePicker == 1 && !isHadesDead)
                {
                    hadesAnim.SetInteger("State", statePicker);
                    SetHoldAndWait(1);
                }
                if (statePicker == 2 && !isHadesDead)
                {
                    hadesAnim.SetInteger("State", statePicker);
                    SetHoldAndWait(1);
                }
              
                loopEnd = Time.time;
            }
        }
        void SetHoldAndWait(int holdValue)
        {
            hadesAnim.SetInteger("Hold", holdValue);
            StartCoroutine(WaitAndResetHold(holdAnimLoopInterval)); 
        }

        IEnumerator WaitAndResetHold(float delay)
        {
            yield return new WaitForSeconds(delay); 
            hadesAnim.SetInteger("Hold", 0);
        }
    }
}