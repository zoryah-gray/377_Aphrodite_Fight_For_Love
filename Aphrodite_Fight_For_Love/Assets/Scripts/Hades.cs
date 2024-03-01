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
        [SerializeField] 

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

        public float hadesBulletSpeed = 1f;

        public Animator hadesAnim;

        private float loopEnd;
        public float loopInterval = 5f;

        private float holdAnimLoopEnd;
        private float holdAnimLoopInterval = 6f;

        private GameObject player;
        //private SpriteRenderer hestRend;

        private bool HadesIsLeft;

        public GameObject minionPrefab1;
        public GameObject minionPrefab2;


        public float health = 40f;
        public Image healthBar;
        public Image border;
        public Image damageBar;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            loopEnd = -loopInterval;
            Animator hadesAnim = GetComponent<Animator>();
            hadesAnim.SetInteger("Hold", 1);

            
            //HadesIsLeft = true;
            //currPullingWall = false;
            //ffloopEnd = -ffloopInterval;
            //canFF = false;
            //fftileSpriteRenderer = firetile.GetComponent<SpriteRenderer>();
        }


        void Update()
        {
            //Debug.Log("player.transform.position.x" + player.transform.position.x);
            //Debug.Log("transform.position.x" + transform.position.x);
            changeHadesDir();
            HadesAttackAnim();
            updateHealth();
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
            Destroy(gameObject);
            Destroy(healthBar);
            Destroy(border);
            Destroy(damageBar);
        }
        public void SpawnHadesEnemies()
        {

            minionPrefab1 = Instantiate(minionPrefab1, new Vector2(Random.Range(-7f,-1.5f), Random.Range(-3.5f,3.5f)), Quaternion.identity);
            minionPrefab2 = Instantiate(minionPrefab2, new Vector2(Random.Range(1.5f, 7f), Random.Range(-3.5f, 3.5f)), Quaternion.identity);

            AIDestinationSetter minPref1targ = minionPrefab1.GetComponent<AIDestinationSetter>();
            minPref1targ.target = player.transform;
            AIDestinationSetter minPref2targ = minionPrefab2.GetComponent<AIDestinationSetter>();
            minPref2targ.target = player.transform;
            //Vector2 direction = player.transform.position;
            //Rigidbody2D hadesBulletRB = hadesBulletGO.GetComponent<Rigidbody2D>();
            //hadesBulletRB.velocity = direction * hadesBulletSpeed;
        }

        public void ShootHadesBullet()
        {
            
            var hadesBulletInitPos1 = new Vector3(0f, 0f, 0f);
           
            if (!HadesIsLeft) { hadesBulletInitPos1 = new Vector3(1.75f, 0f, 0f); }
            if (HadesIsLeft) { hadesBulletInitPos1 = new Vector3(-1.75f, 0f, 0f); }
            
            GameObject hadesBulletGO = Instantiate(hadesBulletPrefab, hadesBulletInitPos1, Quaternion.identity);
            Vector2 direction = player.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation2 = Quaternion.Euler(0f, 0f, angle);

            hadesBulletGO.transform.rotation = rotation2;
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
                //int statePicker = 2;

                //int statePicker = Random.Range(1, 4);
                int statePicker = Random.Range(1, 3);
                Debug.Log("statePicker" + statePicker);

                //if (statePicker == 1)
                //{
                //    hadesAnim.SetInteger("State", 1);
                //    hadesAnim.SetInteger("Hold", 1);
                //}
                if (statePicker == 1)
                {
                    hadesAnim.SetInteger("State", 1);
                    hadesAnim.SetInteger("Hold", 1);
                }
                if (statePicker == 2)
                {
                    hadesAnim.SetInteger("State", 2);
                    hadesAnim.SetInteger("Hold", 1);
                }
                
                loopEnd = Time.time;
                if (Time.time - holdAnimLoopEnd >= holdAnimLoopInterval)
                {
                    holdAnimLoopEnd = Time.time;
                    hadesAnim.SetInteger("Hold", 0);
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