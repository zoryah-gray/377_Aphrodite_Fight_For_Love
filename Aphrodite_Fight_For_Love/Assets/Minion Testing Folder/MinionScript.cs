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
        [SerializeField] public float moveSpeed = 1;
        [SerializeField] public SpecialMinionTypes minionType = SpecialMinionTypes.standard;
        [SerializeField] public bool yesPatrol;
        [SerializeField] public int level = 1;
        AIPath minionPathing;
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
            switch (minionType)
            {
                case SpecialMinionTypes.userDefine:

                    break;
                case SpecialMinionTypes.standard:
                    health = 2 * level;
                    moveSpeed = 1;
                    attackSpeed = 1;
                    strength = .25f;
                    break;
                case SpecialMinionTypes.light:
                    health = 1 * level;
                    moveSpeed = 2;
                    attackSpeed = 1.5f;
                    strength = .125f;
                    break;
                case SpecialMinionTypes.heavy:
                    health = 4 * level;
                    moveSpeed = .75f;
                    attackSpeed = 1;
                    strength = .5f;
                    break;
                case SpecialMinionTypes.boss:
                    health = 5 * level;
                    moveSpeed = 1;
                    attackSpeed = 1;
                    strength = .25f;
                    break;
                

            }

            minionPathing = GetComponentInParent<AIPath>();
            minionPathing.maxSpeed = moveSpeed;

        }

        // Update is called once per frame
        void Update()
        {
            //shut up about this being empty I know
        }

        public void TakeDamage(int playerDamage)
        {
            health -= playerDamage;
            Debug.Log("Minion has taken " +  playerDamage +" damage. " + health + " health remaining");
            if (health <= 0)
            {
                //we'll actually do a kill function which will do a death animation,
                //then delete the game object
                
                MinionDeath();
            }
        }
        private void MinionDeath()
        {
            //Do the death animation
            //
            //TODO
            //
            //Destroy game object
            //if (minionType != SpecialMinionTypes.special)
            //{
                Destroy(gameObject);
            //}

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

            playerObject.GetComponent<PlayerAttack>().health -= minionDamage;
            Debug.Log("Player took " + minionDamage + " damage. "); // + playerObject.GetComponent<QuickPlayerMove>().health + " health remaining.");
        }

    }
}
