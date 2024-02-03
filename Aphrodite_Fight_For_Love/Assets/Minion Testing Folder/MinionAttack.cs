using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class MinionAttack : MonoBehaviour
    {
        MinionScript minionBase;

        // Start is called before the first frame update
        void Start()
        {
            minionBase = GetComponentInParent<MinionScript>();

        }

        // Update is called once per frame
        void Update()
        {
        
        }

        //Attack the player on a regular interval
        public IEnumerator Attack(GameObject playerObject)
        {
            // playerObject.health-= strength;

            //invoke player damage function, feed in enemy strength stat
            Debug.Log("Attack with " + minionBase.strength + " strength.");
            yield return new WaitForSeconds(minionBase.attackSpeed);

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            StartCoroutine(Attack(collision.gameObject));

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            /// Stop attacking the player
            StopCoroutine(Attack(collision.gameObject));
        }
    }
}
