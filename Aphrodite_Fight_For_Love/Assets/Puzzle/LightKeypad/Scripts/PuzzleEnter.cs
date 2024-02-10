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
<<<<<<< Updated upstream:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/PuzzleEnter.cs
            icon.SetActive(true);
            if (collision.gameObject.name == "Player")
            {
                collision.GetComponent<TestPlayerCtlr>().inPuzzleTrigger = true;
=======
           
            Debug.Log(collision.gameObject.name);
            puzzleCtrlScript.currPuzzle = puzzle;
            if (collision.gameObject.name == "Player")
            {
                icon.SetActive(true);
                collision.gameObject.GetComponent<PlayerKeypadPuzzleController>().inPuzzleTrigger = true;
                puzzleCtrlScript.currPuzzle = puzzle;
                Debug.Log(puzzleCtrlScript.currPuzzle.name);

>>>>>>> Stashed changes:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/Scripts/PuzzleEnter.cs
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
<<<<<<< Updated upstream:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/PuzzleEnter.cs
                collision.GetComponent<TestPlayerCtlr>().inPuzzleTrigger = false;
=======
                icon.SetActive(false);
                collision.gameObject.GetComponent<PlayerKeypadPuzzleController>().inPuzzleTrigger = false;
                //puzzleCtrlScript.currPuzzle = null;
                Debug.Log("exiting trigger area");
>>>>>>> Stashed changes:Aphrodite_Fight_For_Love/Assets/Puzzle/LightKeypad/Scripts/PuzzleEnter.cs
            }
        }
    }
}
