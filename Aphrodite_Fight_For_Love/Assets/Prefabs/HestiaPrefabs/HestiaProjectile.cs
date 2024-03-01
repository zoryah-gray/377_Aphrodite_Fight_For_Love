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
        public LayerMask wallLayers;
        Vector2 rectangleSize = new Vector2(0.5f, 0.5f);

        //

        private void Update()
        {
            if (!isHestiaFireball && gameObject.tag != "BulletBox")
            {
                Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(bulletBox.position, rectangleSize, 0f, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.gameObject.tag == "Hestia")
                    {
                        enemy.gameObject.GetComponent<Hestia>().TakeDamage(1);
                        Debug.Log("We hit Hestia!" + enemy.name);
                        Destroy(gameObject);
                    }
                    else if (enemy.gameObject.tag == "Hades")
                    {
                        enemy.gameObject.GetComponent<Hades>().TakeDamage(1);
                        Debug.Log("We hit Hades!" + enemy.name);
                        Destroy(gameObject);
                    }

                    else
                    {
                        enemy.gameObject.GetComponent<MinionScript>().TakeDamage(1);
                        Debug.Log("We hit an enemy!" + enemy.name);
                        Destroy(gameObject);
                    }
                }
            }
            Collider2D[] hitWalls = Physics2D.OverlapBoxAll(bulletBox.position, rectangleSize, 0f, wallLayers);
            if (!isHestiaFireball && gameObject.tag != "BulletBox")
            {
                foreach (Collider2D wall in hitWalls)
                {

                    Debug.Log("We hit a wall");
                    Destroy(gameObject);
                }
            }
        }
        private void OnBecameInvisible()
        {
            Debug.Log("gameObject OOB");
            Destroy(gameObject);
            //if (!isHestiaFireball && gameObject.tag != "BulletBox")
            //{
            //    Destroy(gameObject);
            //}
            

        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            //if (collision.gameObject.tag == "Player" && isHestiaFireball && gameObject.tag != "BulletBox")
            //{
            //    Debug.Log("Fireball Hit The Player");
            //    Destroy(gameObject);
            //}
            
            if (collision.gameObject.tag == "Wall" && gameObject.tag != "BulletBox")
            {
                Destroy(gameObject);
            }
            
            //GameObject collidingObject = collision.gameObject;
            //if (isHestiaFireball && collidingObject.CompareTag("Player"))
            //{
            //    Destroy(gameObject);
            //    Attack(collidingObject, 0.3f);

            //}
        }
        //void Attack(GameObject playerObject, float wallDamage)
        //{
        //    //playerObject.GetComponent<QuickPlayerMove>().health -= minionDamage;
        //    GameData.CheckPlayerHealth(wallDamage);
        //    Debug.Log("Player took " + wallDamage + " damage. Curr Player Health = " + GameData.playerHeath); // + playerObject.GetComponent<QuickPlayerMove>().health + " health remaining.");
        //}

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.CompareTag("Wall"))
        //    {
        //        Destroy(gameObject);
        //    }
        //}

    }
}
