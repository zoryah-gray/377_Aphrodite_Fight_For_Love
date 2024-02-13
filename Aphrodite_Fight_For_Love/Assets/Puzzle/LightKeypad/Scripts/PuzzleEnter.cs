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
            puzzleCtrlScript.unlocksObj = door;
            //puzzle.unloackableObj = door;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            puzzleCtrlScript.currPuzzle = puzzle;
            if (collision.gameObject.name == "Player")
            {
                icon.SetActive(true);
                collision.gameObject.GetComponent<PlayerKeypadPuzzleController>().inPuzzleTrigger = true;
                puzzleCtrlScript.currPuzzle = puzzle;
                Debug.Log(puzzleCtrlScript.currPuzzle.name);

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                icon.SetActive(false);
                collision.gameObject.GetComponent<PlayerKeypadPuzzleController>().inPuzzleTrigger = false;
                //puzzleCtrlScript.currPuzzle = null;
                Debug.Log("exiting trigger area");
            }
        }
    }
}
