using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class Hestia : MonoBehaviour
    {
        public GameObject fireballPrefab;
        public float fireballSpeed = 5f;
        private float lastShotFireball;
        public float fireballInterval = 3f;

        // Start is called before the first frame update
        void Start()
        {
            lastShotFireball = -fireballInterval;
        }

        // Update is called once per frame
        void Update()
        {
            
            if(Time.time - lastShotFireball >= fireballInterval)
            {
                ShootFireball();
                lastShotFireball = Time.time;
            } 
        }
        void ShootFireball()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            var fireballInitPos = new Vector2(transform.position.x, player.transform.position.y);
            GameObject fireballGO = Instantiate(fireballPrefab, fireballInitPos, Quaternion.identity);

            //I want a better way to have the fireball track the player's position. Will do more research later
            Vector2 direction = (player.transform.position - transform.position).normalized;

            Rigidbody2D fireballRB = fireballGO.GetComponent<Rigidbody2D>();
            fireballRB.velocity = direction * fireballSpeed;
        }
    }
}
