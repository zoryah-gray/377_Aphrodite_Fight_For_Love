using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class doorlevel2 : MonoBehaviour
    {
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("collided with door");
            // Check if the collided GameObject has the required component
            if (collision.gameObject.GetComponent<KeyManager>() != null)
            {
                Debug.Log("collided with gameobject having key manager");
                KeyManager keyManager = collision.gameObject.GetComponent<KeyManager>();
                if (keyManager.keysCollected.Contains(3))
                {
                    Destroy(gameObject);
                }
                else
                    Debug.Log("key list does not include required key.");
            }
        }

    }
}
