using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class WallDamage : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject collidingObject = collision.gameObject;
            if (collidingObject.CompareTag("Player"))
            {
                Attack(collidingObject, 1f);
            }
        }
        void Attack(GameObject playerObject, float wallDamage)
        {
            //playerObject.GetComponent<QuickPlayerMove>().health -= minionDamage;
            GameData.CheckPlayerHealth(wallDamage);
            Debug.Log("Player took " + wallDamage + " damage. Curr Player Health = " + GameData.playerHeath); // + playerObject.GetComponent<QuickPlayerMove>().health + " health remaining.");
        }
    }
}
