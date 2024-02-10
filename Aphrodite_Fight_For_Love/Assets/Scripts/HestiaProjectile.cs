using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class HestiaProjectile : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            Debug.Log("Fireball OOB");
            Destroy(gameObject);
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                Debug.Log("Fireball Hit The Player");
                Destroy(gameObject);
            }
        }
    }
}
