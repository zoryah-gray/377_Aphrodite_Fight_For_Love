using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class MinionAttack : MonoBehaviour
    {
        MinionScript minionBase;
        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            minionBase = GetComponentInParent<MinionScript>();
            animator = GetComponentInParent<Animator>();

        }

        // Update is called once per frame
        void Update()
        {

        }

        //Attack the player on a regular interval
        /*public IEnumerator Attack(GameObject playerObject, bool inRadius)
        {
            // playerObject.health-= strength;

            //invoke player damage function, feed in enemy strength stat
            while (inRadius)
            {
               Debug.Log("Attack with " + minionBase.strength + " strength.");
               yield return new WaitForSeconds(minionBase.attackSpeed);
            }

        }*/
        bool inRadius;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //inRadius = true;
            /*StartCoroutine(Attack(collision.gameObject, inRadius));*/

            GameObject playerObject = collision.gameObject;
            if (playerObject.CompareTag("Player"))
            {
                //animator.SetBool("isAttacking", true);
                animator.SetInteger("currState", 2);
                //animator.SetTrigger("isFollowing");
            }

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //inRadius = false;
            Debug.Log("Player outside of attack radius. inRadius is " + inRadius);
            /// Stop attacking the player
            /*StopCoroutine(Attack(collision.gameObject, inRadius));*/
            GameObject playerObject = collision.gameObject;
            if (playerObject.CompareTag("Player"))
            {
                //animator.SetBool("isAttacking", false);
                animator.SetInteger("currState", 1);
                LeanTween.cancel(playerObject);
                playerObject.GetComponent<SpriteRenderer>().color = Color.white;
                //animator.SetTrigger("isFollowing");
            }
        }
    }
}
