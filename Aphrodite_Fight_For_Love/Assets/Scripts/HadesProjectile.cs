using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class HadesProjectile : MonoBehaviour
    {
        // Start is called before the first frame update
        

        // Update is called once per frame
        //void Update()
        //{
        
        //}

        public bool isHestiaFireball = true;
        //
        public UnityEngine.Transform bulletBox;

        //
        public LayerMask enemyLayers;
        public LayerMask wallLayers;
        Vector2 rectangleSize = new Vector2(0.5f, 0.5f);
        public GameObject playerObject;

        [Header("Audio Files")]
        public AudioClip hitClip;
        //
        void Start()
        {

        }
        private void Update()
        {

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
            GameObject collidingObject = collision.gameObject;
            if (isHestiaFireball && collidingObject.CompareTag("Player"))
            {

                Destroy(gameObject);
                Attack(collidingObject, 0.3f);

            }
        }
        void Attack(GameObject playerObject, float wallDamage)
        {
            //playerObject.GetComponent<QuickPlayerMove>().health -= minionDamage;
            GameData.CheckPlayerHealth(wallDamage);
            Debug.Log("Player took " + wallDamage + " damage. Curr Player Health = " + GameData.playerHeath); // + playerObject.GetComponent<QuickPlayerMove>().health + " health remaining.");
       



            Color originalColor = playerObject.GetComponent<SpriteRenderer>().color;
            Color flashColor = Color.red;
            float flashDuration = 0.1f;
            int numberOfFlashes = 2;

            AudioSource.PlayClipAtPoint(hitClip, transform.localPosition);
            LeanTween.value(playerObject, originalColor, flashColor, flashDuration)
               .setEase(LeanTweenType.easeInOutSine)
               .setLoopPingPong(numberOfFlashes)
               .setOnUpdate((Color val) =>
               {
                   // Update the object's color during the tween
                   playerObject.GetComponent<SpriteRenderer>().color = val;
               })
               .setOnComplete(() =>
               {
                   // Reset the color to the original after the flash is complete
                   playerObject.GetComponent<SpriteRenderer>().color = Color.white;
               });
            //GameData.CheckPlayerHealth(minionDamage);
            //Debug.Log("Player taking " + minionDamage + " damage. Curr Player Health = " + GameData.playerHeath);
            //playerObject.GetComponent<QuickPlayerMove>().health -= minionDamage;
            //Debug.Log("Player took " + minionDamage + " damage. "); // + playerObject.GetComponent<QuickPlayerMove>().health + " health remaining.");
        }

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.CompareTag("Wall"))
        //    {
        //        Destroy(gameObject);
        //    }
        //}



    }
}
