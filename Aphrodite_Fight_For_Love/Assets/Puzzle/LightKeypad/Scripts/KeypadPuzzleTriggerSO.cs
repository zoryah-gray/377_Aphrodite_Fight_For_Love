using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    [CreateAssetMenu(fileName = "New Puzzle", menuName = "KeypadPuzzle")]
    public class KeypadPuzzleTriggerSO : ScriptableObject
    {

        // what door the puzzle unlocks
        public GameObject unloackableObj;
        public Sprite triggerSprite;

        //public int doorID;
        public List<int> doorIDs;


        // the actual code/password
        public List<int> code;

        // the unique id of the code
        public int codeID;

        // the scene the puzzle belongs to
        public int sceneID;

        // if the puzzle has been unlocked
        public bool unlocked = false;

        //the dificulty of the puzzle
        [SerializeField] public puzzleDifficulty difficulty = puzzleDifficulty.easy;
        public enum puzzleDifficulty
        {
            easy,
            medium,
            hard,


        }

        private void Awake()
        {
            //ResetValues();
        }

        private void OnEnable()
        {
            ResetValues();
        }

        public void ResetValues()
        {
            unlocked = false;
            if (doorIDs.Count == 0)
            {
                doorIDs.Add(codeID + 1);
            }
        }


    }
}
