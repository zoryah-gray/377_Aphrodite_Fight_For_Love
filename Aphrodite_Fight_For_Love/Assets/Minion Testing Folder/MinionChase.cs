using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class MinionChase : MonoBehaviour
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            StopCoroutine(PatrolForPlayer());
        }


        private void OnTriggerStay2D(Collider2D collision)
        {
            Debug.Log("Collider stay in trigger area.");
            GameObject playerObject = collision.gameObject;
            if (playerObject.GetComponent("QuickPlayerMove") != null)
            {
                Debug.Log("Player Registered. Entered Trigger Area");
                //chase the player
                FollowPlayer(playerObject);

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //MinionScript minionscript = GetComponentInParent<MinionScript>();
            
            if (minionBase.yesPatrol)
                StartCoroutine(PatrolForPlayer());
        }

        private void FollowPlayer(GameObject playerObject)
        {
            //Vector2 enemyDirection = playerCollider.GameObject().;

            transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, minionBase.moveSpeed * Time.deltaTime);
        }

        IEnumerator PatrolForPlayer()
        {

            yield return new WaitForSeconds(3 / minionBase.moveSpeed);
        }
    }
}
