using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class WallDamage : MonoBehaviour
    {

        [Header("Audio Files")]
        public AudioClip hitClip;
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
                Attack(collidingObject, 0.5f);
            }
        }
        void Attack(GameObject playerObject, float wallDamage)
        {
            //playerObject.GetComponent<QuickPlayerMove>().health -= minionDamage;
            GameData.CheckPlayerHealth(wallDamage);

            AudioSource.PlayClipAtPoint(hitClip, transform.localPosition);
            Debug.Log("Player took " + wallDamage + " damage. Curr Player Health = " + GameData.playerHeath); // + playerObject.GetComponent<QuickPlayerMove>().health + " health remaining.");
        }
    }
}
