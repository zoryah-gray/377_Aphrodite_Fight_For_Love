using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class HestiaProjectile : MonoBehaviour
    {
        public bool isHestiaFireball = true;
        //
        public UnityEngine.Transform bulletBox;

        //
        public LayerMask enemyLayers;
        Vector2 rectangleSize = new Vector2(0.5f, 0.5f);
        //

        private void Update()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(bulletBox.position, rectangleSize, 0f, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.gameObject.GetComponent<MinionScript>().TakeDamage(1);
                Debug.Log("We hit an enemy!" + enemy.name);
                Destroy(gameObject);
            }
        }
        private void OnBecameInvisible()
        {
            Debug.Log("gameObject OOB");
            Destroy(gameObject);
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player" && isHestiaFireball)
            {
                Debug.Log("Fireball Hit The Player");
                Destroy(gameObject);
            }
        }
    }
}
