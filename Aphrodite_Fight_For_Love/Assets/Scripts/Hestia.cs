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

        public float fbIntvalShort = 1f;
        public float fbIntvalLong = 10f;
        public Animator hestiaAnimator;
        private float animIndex;

        // Start is called before the first frame update
        void Start()
        {
            lastShotFireball = -fireballInterval;
        }

        // Update is called once per frame
        void Update()
        {
            
            //if (Time.time - lastShotFireball >= fireballInterval)
            //{
            //    
            //    SwitchHestiaAnim(animIndex);
            //    AdjustFireballInterval(animIndex);
            //    lastShotFireball = Time.time;
            //    animIndex = (animIndex + 1f) % 4f;
            //}
            if (Time.time - lastShotFireball >= fireballInterval)
            {
                ShootFireball();
                AdjustFireballInterval(animIndex);
                SwitchHestiaAnim(animIndex);
                animIndex = (animIndex + 1f) % 8f;
                lastShotFireball = Time.time;
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

        void AdjustFireballInterval(float index)
        {
            switch (index)
            {
                case 0: //Fireball Prep
                    fireballInterval = fbIntvalShort;
                    break;
                case 1: //Fireball Attack
                    fireballInterval = fbIntvalLong;
                    break;
                case 2: //Fireball Winding Down
                    fireballInterval = fbIntvalShort;
                    break;
                case 3: //Idle
                    fireballInterval = fbIntvalLong;
                    break;
                case 4:
                    fireballInterval = fbIntvalShort;
                    break;
                case 5:
                    fireballInterval = fbIntvalShort;
                    break;
                case 6:
                    fireballInterval = fbIntvalLong;
                    break;
                case 7:
                    fireballInterval = fbIntvalLong;
                    break;
            }
        }
        void SwitchHestiaAnim(float index)
        {
            switch (index)
            {
                case 0: //FireballPrep
                    hestiaAnimator.SetFloat("HestiaNotAttack", -1f);
                    hestiaAnimator.SetFloat("FireballAttack", 0f);
                    Debug.Log("FireballPrep");
                    break;
                case 1: //FireballActive
                    hestiaAnimator.SetFloat("HestiaNotAttack", -1f);
                    hestiaAnimator.SetFloat("FireballAttack", 1f);
                    Debug.Log("FireballActive");
                    break;
                case 2: //FireballWindDown
                    hestiaAnimator.SetFloat("HestiaNotAttack", 0f);
                    hestiaAnimator.SetFloat("FireballAttack", -1f);
                    Debug.Log("Fireball Winding Down");
                    break;
                case 3: //HestiaIdle
                    hestiaAnimator.SetFloat("HestiaNotAttack", 1f);
                    hestiaAnimator.SetFloat("FireballAttack", -1f);
                    Debug.Log("HestiaIdle");
                    break;
                case 4: //HestiaFireFloorPrep
                    hestiaAnimator.SetFloat("HestiaNotAttack", -1f);
                    hestiaAnimator.SetFloat("FireballAttack", -1f);
                    Debug.Log("HestiaFireFloorPrep");
                    break;
                case 5: //HestiaFireFloorWindingDown
                    hestiaAnimator.SetFloat("HestiaNotAttack", 1f);
                    hestiaAnimator.SetFloat("FireballAttack", 0f);
                    Debug.Log("HestiaFireFloorWindingDown");
                    break;
                case 6: //HestiaWallIn
                    hestiaAnimator.SetFloat("HestiaNotAttack", 1f);
                    hestiaAnimator.SetFloat("FireballAttack", 1f);
                    Debug.Log("HestiaWallIn");
                    break;
                case 7: //HestiaWallOut
                    hestiaAnimator.SetFloat("HestiaNotAttack", 0f);
                    hestiaAnimator.SetFloat("FireballAttack", 1f);
                    Debug.Log("HestiaWallOut");
                    break;
            }
        }         
    }
}
