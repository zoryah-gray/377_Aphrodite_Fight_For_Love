using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class HestiaProjectile : MonoBehaviour
    {
        public bool isHestiaFireball = true;
        private void OnBecameInvisible()
        {
            Debug.Log("gameObject OOB");
            Destroy(gameObject);
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Player" && isHestiaFireball)
            {
                Debug.Log("Fireball Hit The Player");
                Destroy(gameObject);
            }
        }
    }
}
