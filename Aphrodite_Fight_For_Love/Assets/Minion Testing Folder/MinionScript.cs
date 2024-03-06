using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

namespace AphroditeFightCode
{
    public class MinionScript : MonoBehaviour
    {
        [Header("Minion Type Variables")]

        [SerializeField] public SpecialMinionTypes minionType = SpecialMinionTypes.standard;
        [SerializeField] int health = 1;
        [SerializeField] public float strength = .5f;
        [SerializeField] public float attackSpeed = 1.5f; //how many seconds it takes
        [SerializeField] public float moveSpeed = 1;

        [Header("Other variables")]
        [SerializeField] private Rigidbody2D rb = null;
        [SerializeField] private Animator anim;
        [SerializeField] public int level = 1;

        public AudioClip[] audioClips;
        AIPath minionPathing;
        GameObject healthBar;
        float healthBarSize;
        AudioListener listener;
        // Start is called before the first frame update

        //handles damage to the minion, holds values, and destroys the minion upon death.


        public enum SpecialMinionTypes
        {
            userDefine,
            standard,
            light,
            heavy,
            boss,


        }
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            listener = GameObject.Find("Main Camera").GetComponent<AudioListener>();
            switch (minionType)
            {
                case SpecialMinionTypes.userDefine:

                    break;
                case SpecialMinionTypes.standard:
                    health = 3;
                    moveSpeed = 3;
                    attackSpeed = 1;
                    strength = .25f;
                    break;
                case SpecialMinionTypes.light:
                    health = 2;
                    moveSpeed = 6;
                    attackSpeed = 1.5f;
                    strength = .125f;
                    break;
                case SpecialMinionTypes.heavy:
                    health = 4;
                    moveSpeed = 2f;
                    attackSpeed = 1;
                    strength = .5f;
                    break;
                case SpecialMinionTypes.boss:
                    health = 5;
                    moveSpeed = 3;
                    attackSpeed = 1;
                    strength = .75f;
                    break;


            }

            minionPathing = GetComponentInParent<AIPath>();
            minionPathing.maxSpeed = moveSpeed;
            healthBar = transform.GetChild(2).gameObject;
            healthBarSize = healthBar.transform.localScale.x / health;

        }

        // Update is called once per frame
        void Update()
        {
            //shut up about this being empty I know
            
        }
        
        public void TakeDamage(int playerDamage)
        {
            health -= playerDamage;
            Color originalColor = GetComponent<SpriteRenderer>().color;
            Color flashColor = Color.red;
            float flashDuration = 0.1f;
            int numberOfFlashes = 2;
            AudioSource.PlayClipAtPoint(audioClips[1], transform.localPosition);
            
            LeanTween.value(gameObject, Color.white, flashColor, flashDuration)
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
                   gameObject.GetComponent<SpriteRenderer>().color = Color.white;
               });

            ///Affect healthbar or thingy thing
            Vector3 newHealth = new Vector3(healthBar.transform.localScale.x - healthBarSize, healthBar.transform.localScale.y, healthBar.transform.localScale.z);


            healthBar.LeanScale(newHealth, .01f);


            Debug.Log("Minion has taken " + playerDamage + " damage. " + health + " health remaining");
            


            if (health <= 0)
            {
                //we'll actually do a kill function which will do a death animation,
                //then delete the game object
                //Animator animator = GetComponentInParent<Animator>();
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                anim.SetTrigger("death");
                healthBar.SetActive(false);
            }
        }


        private void MinionDeath()
        {
            //Do the death animation
            //
            //TODO
            
            Destroy(gameObject);
            //Destroy game object
            //if (minionType != SpecialMinionTypes.special)
            //{
            //Destroy(gameObject);
            //}

        }

        public void DestroyMinion()
        {

            Destroy(gameObject);
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject collidingObject = collision.gameObject;
            if (collidingObject.CompareTag("Player"))
            {
                Attack(collidingObject, strength);
            }
        }
        void Attack(GameObject playerObject, float minionDamage)
        {
            //playerObject.GetComponent<QuickPlayerMove>().health -= minionDamage;
            // already handled in attackPlayer Script

            //Debug.Log("minion scrpt" +GameData.freezePlayer);
            //if (!GameData.freezePlayer)
            //{
            //    GameData.CheckPlayerHealth(minionDamage);
            //    Debug.Log("Player took " + minionDamage + " damage. Curr Player Health = " + GameData.playerHeath); // + playerObject.GetComponent<QuickPlayerMove>().health + " health remaining.");
            //}
        }

    }
}
