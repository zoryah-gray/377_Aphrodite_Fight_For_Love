using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class MinionChase : MonoBehaviour
    {
        MinionScript minionBase;
        Animator animator;


        public GameObject playerBody;
        public GameObject healthBar;

        // Start is called before the first frame update
        void Start()
        {
            /*
             Set values for any references in the parent that will need to be accessed.
             */
            minionBase = GetComponentInParent<MinionScript>();
            animator = GetComponentInParent<Animator>();
            

            //Update minion speed to fit the minion type

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            GameObject collidingObject = collision.gameObject;
            /* If the player, 
             * stop patrolling and 
             * begin following the player*/
            if (collidingObject.CompareTag("Player"))
            {
                //GetComponentInParent<AIPath>().enabled = true;
                animator.SetInteger("currState", 1);
                healthBar.SetActive(true);

            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            //If player, stop following the player
            GameObject collidingObject = collision.gameObject;
            if (collidingObject.CompareTag("Player"))
            {

                //GetComponentInParent<AIPath>().enabled = false;
                animator.SetInteger("currState", 0);
            }
        }


    }
}
