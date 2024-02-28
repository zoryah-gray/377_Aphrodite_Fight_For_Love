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
        [SerializeField] int health = 1;
        [SerializeField] public float strength = .5f;
        [SerializeField] public float attackSpeed = 1.5f; //how many seconds it takes
        [SerializeField] public float knockbackForce = 5f;
        [SerializeField] private Rigidbody2D rb = null; 
        [SerializeField] public float moveSpeed = 1;
        [SerializeField] private Animator anim;
        [SerializeField] public SpecialMinionTypes minionType = SpecialMinionTypes.standard;
        [SerializeField] public bool yesPatrol;
        [SerializeField] public int level = 1;
        AIPath minionPathing;
        GameObject healthBar;
        float healthBarSize;
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
            switch (minionType)
            {
                case SpecialMinionTypes.userDefine:

                    break;
                case SpecialMinionTypes.standard:
                    health = 3;
                    moveSpeed = 2;
                    attackSpeed = 1;
                    strength = .25f;
                    break;
                case SpecialMinionTypes.light:
                    health = 2;
                    moveSpeed = 4;
                    attackSpeed = 1.5f;
                    strength = .125f;
                    break;
                case SpecialMinionTypes.heavy:
                    health = 4;
                    moveSpeed = 1f;
                    attackSpeed = 1;
                    strength = .5f;
                    break;
                case SpecialMinionTypes.boss:
                    health = 5;
                    moveSpeed = 1;
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
            Color flashColor = Color.red;
            float flashDuration = 0.1f;
            int numberOfFlashes = 1;

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
            Color originalColor = GetComponent<SpriteRenderer>().color;
            Color flashColor = Color.red;
            float flashDuration = 0.1f;
            int numberOfFlashes = 2;


            if (health <= 0)
            {
                //we'll actually do a kill function which will do a death animation,
                //then delete the game object
                MinionDeath();
            }
        }

        private void PushBackMinion()
        {
            if (rb != null)
            {
                Vector2 pushDir = -transform.right;
                rb.AddForce(pushDir * knockbackForce, ForceMode2D.Impulse);
            }
        }


        private void MinionDeath()
        {
            //Do the death animation
            //
            //TODO
            //
            anim.SetTrigger("death");
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
            GameData.CheckPlayerHealth(minionDamage);
            Debug.Log("Player took " + minionDamage + " damage. Curr Player Health = " + GameData.playerHeath); // + playerObject.GetComponent<QuickPlayerMove>().health + " health remaining.");
        }

    }
}
