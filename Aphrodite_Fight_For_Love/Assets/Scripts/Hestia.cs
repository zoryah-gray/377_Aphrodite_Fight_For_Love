using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class Hestia : MonoBehaviour
    {
        public GameObject fireballPrefab;
        public float fireballSpeed = 5f;
        private float loopEnd;
        public float loopInterval = 15f;
        private float holdAnimLoopEnd;
        private float holdAnimLoopInterval = 100f;
        public Animator hestAnim;
        // Start is called before the first frame update
        void Start()
        {
            loopEnd = -loopInterval;
            holdAnimLoopEnd = -holdAnimLoopInterval;
            Animator hestAnim = GetComponent<Animator>();

            //InvokeRepeating("MyFunction", 0f, fireballInterval);
        }

        // Update is called once per frame
        void Update()
        {

            if (Time.time - loopEnd >= loopInterval)
            {
                int statePicker = Random.Range(1, 4);
                Debug.Log("statePicker" + statePicker);
                if (statePicker == 1)
                {
                    hestAnim.SetInteger("State", 1);
                    hestAnim.SetInteger("Hold", 1);
                }
                if (statePicker == 2)
                {
                    hestAnim.SetInteger("State", 2);
                    hestAnim.SetInteger("Hold", 1);
                }
                if (statePicker == 3)
                {
                    hestAnim.SetInteger("State", 3);
                    hestAnim.SetInteger("Hold", 1);
                }
                loopEnd = Time.time;
                if (Time.time - holdAnimLoopEnd >= holdAnimLoopInterval)
                {
                    holdAnimLoopEnd = Time.time;
                }
            }
            

        }
        void ShootFireball()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            var fireballInitPos = new Vector2(transform.position.x, player.transform.position.y);
            GameObject fireballGO = Instantiate(fireballPrefab, fireballInitPos, Quaternion.identity);

            //I want a better way to have the fireball track the player's position. Will do more research later
            Vector2 direction = player.transform.position;

            Rigidbody2D fireballRB = fireballGO.GetComponent<Rigidbody2D>();
            fireballRB.velocity = direction * fireballSpeed;
        }
    }
}
