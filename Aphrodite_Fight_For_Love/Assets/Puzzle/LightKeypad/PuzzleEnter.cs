using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class PuzzleEnter : MonoBehaviour
    {
        

        [Header("PuzzleIcon")]
        public GameObject icon;

        [Header("Puzzle SO")]
        public KeypadPuzzleTriggerSO puzzle;

        [Header("Puzzle UI Controller")]
        public KeyPadContoller puzzleCtrlScript;

        [Header("Puzzle Unlocks Obj/Door")]
        public GameObject door;

        private void Awake()
        {
            puzzle.unloackableObj = door;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            icon.SetActive(true);
            Debug.Log(collision.gameObject.name);
            puzzleCtrlScript.currPuzzle = puzzle;
            if (collision.gameObject.name == "Player")
            {
                collision.gameObject.GetComponent<PlayerKeypadPuzzleController>().inPuzzleTrigger = true;
                puzzleCtrlScript.currPuzzle = puzzle;
                Debug.Log(puzzleCtrlScript.currPuzzle.name);

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            icon.SetActive(false);
            if (collision.gameObject.name == "Player")
            {
                collision.gameObject.GetComponent<PlayerKeypadPuzzleController>().inPuzzleTrigger = false;
                //puzzleCtrlScript.currPuzzle = null;
            }
        }
    }
}
