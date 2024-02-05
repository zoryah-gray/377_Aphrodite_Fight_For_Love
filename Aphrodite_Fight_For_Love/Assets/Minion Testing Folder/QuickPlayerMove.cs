using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class QuickPlayerMove : MonoBehaviour
    {

        public float health = 4;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            //Collider2D collider = GetComponent<Collider2D>();
            //collider.
            transform.position = new Vector2(Input.GetAxis("Horizontal") * 3, Input.GetAxis("Vertical") * 3);

            if (health <= 0)
            {
                Debug.Log("Player Killed");
                Destroy(gameObject);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject minionObject = GameObject.Find("Minion Object");
                minionObject.GetComponent<MinionScript>().TakeDamage(1);
            }
        }
    }
}
