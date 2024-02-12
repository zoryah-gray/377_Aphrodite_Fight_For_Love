using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class HestiaProjectile : MonoBehaviour
    {
        public bool isHestiaFireball;
        private void OnBecameInvisible()
        {
            Debug.Log("gameObject OOB");
            Destroy(gameObject);
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("bullet col" + collision);
            if(collision.gameObject.tag == "Player" && isHestiaFireball)
            {
                Debug.Log("Fireball Hit The Player");
                Destroy(gameObject);
            }

          //  if(collision.gameObject.layer == 7) // 7 is enemy layer
            if(collision.gameObject.tag == "Enemy")
            {
                Debug.Log("Bullet has hit enemy");
                collision.gameObject.GetComponent<MinionScript>().TakeDamage(1);
                Destroy(gameObject);
            }
        }
    }
}
