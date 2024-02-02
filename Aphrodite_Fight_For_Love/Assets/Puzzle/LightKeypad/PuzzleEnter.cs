using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class PuzzleEnter : MonoBehaviour
    {
        

        [Header("PuzzleIcon")]
        public GameObject icon;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            icon.SetActive(true);
            if (collision.gameObject.name == "Player")
            {
                collision.gameObject.GetComponent<PlayerKeypadPuzzleController>().inPuzzleTrigger = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            icon.SetActive(false);
            if (collision.gameObject.name == "Player")
            {
                collision.gameObject.GetComponent<PlayerKeypadPuzzleController>().inPuzzleTrigger = false;
            }
        }
    }
}
