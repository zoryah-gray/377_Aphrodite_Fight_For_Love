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
        [SerializeField] public int level;
        // Start is called before the first frame update

        //handles damage to the minion, holds values, and destroys the minion upon death.


        public enum SpecialMinionTypes
        {
            standard,
            light,
            heavy,
            boss,
            userDefine

        }
        void Start()
        {
            switch (minionType)
            {
                case SpecialMinionTypes.standard:
                    health = 2 * level;
                    strength = .25f;
                    break;
                case SpecialMinionTypes.light:
                    health = 1 * level;
                    break;
                case SpecialMinionTypes.heavy:
                    health = 4 * level;
                    break;
                case SpecialMinionTypes.boss: 
                    health = 5 * level;
                    break;
                case SpecialMinionTypes.userDefine: 

                    break;

            }

        }

        // Update is called once per frame
        void Update()
        {
            //shut up about this being empty I know
        }

        public void TakeDamage(int playerDamage)
        {
            health -= playerDamage;
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
       
    }
}
