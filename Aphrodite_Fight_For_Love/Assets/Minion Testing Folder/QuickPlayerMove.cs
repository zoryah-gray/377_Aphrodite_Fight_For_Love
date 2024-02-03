using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class QuickPlayerMove : MonoBehaviour
    {

        public int health = 2;
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
            }
        }
    }
}
