using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    
    public class KeyManager : MonoBehaviour
    {
        public List<int> keysCollected = new List<int>();
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
            if (collision.gameObject.GetComponent<Drawers>()!=null)
            {
                Drawers drawer = collision.gameObject.GetComponent<Drawers>();
                keysCollected.Add(drawer.containsKey);
                Debug.Log("You just collected key " + drawer.containsKey);
                //Update GUI
            }
            
        }
    }
}
