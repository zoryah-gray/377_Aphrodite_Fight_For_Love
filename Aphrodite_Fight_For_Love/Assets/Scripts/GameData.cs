using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace AphroditeFightCode
{
    public static class GameData
    {
        
        //public static Dictionary<int, int[]> keypadPuzzleCodes = new Dictionary<int, int[]>();
        //public static Dictionary<int, List<int>> keypadPuzzleCodesL = new Dictionary<int, List<int>>();
        
        [Header("Puzzle Data")]
        public static Dictionary<int, List<int>> keypadPuzzleCodes = new Dictionary<int, List<int>>();
        



        // will run when the class is accessed for the first time
        static GameData()
        {
            
            populatePuzzleCodes();

            
        }

        private static void populatePuzzleCodes()
        {
            keypadPuzzleCodes = new Dictionary<int, List<int>>
            {
                { 0, new List<int> { 1, 2, 3, 4, 5 } },
                { 1, new List<int> { 1, 2, 3 } },
                { 2, new List<int> { 2,4,1, 5 } },
                { 3, new List<int> { 1, 9} }
            };
            
            
            //printDictionary(keypadPuzzleCodes);
            
        }

        public static void AddKeyCodeToDictionary(int codeID, List<int> code)
        {
            if (!keypadPuzzleCodes.ContainsKey(codeID))
            {
                keypadPuzzleCodes.Add(codeID, code);
            }
            else
            {
                Debug.LogWarning("key already exists in keycodesdictionary");
            }
        }


        public static List<int> GetPuzzleCodeList(int codeID)
        {
            // check if the codeID exists in the dictionary, if it does return that
            if (keypadPuzzleCodes.ContainsKey(codeID))
            {
                return keypadPuzzleCodes[codeID];
            }
            // if not add it to dictionary and return it
            else
            {
                Debug.LogWarning(codeID + " does not exist in available keyPuzzleCodes");
                return new List<int>();
            }

            
            
        }

        public static void printDictionary<T>(Dictionary<T, List<T>> dict)
        {
            foreach (var pair in dict)
            {
                Debug.Log("Dict Key: " + pair.Key + ", Value: " + pair.Value);
                foreach (var val in pair.Value)
                {
                    Debug.Log("Dict Value: " + val);
                }
            }
        }



    }
}