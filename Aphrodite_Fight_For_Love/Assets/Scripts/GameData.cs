using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace AphroditeFightCode
{
    public static class GameData
    {
        [Header("Timelines")]
        public static bool gameStarted = false;

        [Header("Player Game Settings")]
        public static float playerHeath = 60f;
        public static int onHeart = 1;
        public static int currWeapon = 0;
        public static bool playerDead = false;

        [Header("Level Manager")]
        public static int onLevel = 1;

        [Header("Puzzle Data")]
        public static Dictionary<int, List<int>> keypadPuzzleCodes = new Dictionary<int, List<int>>();
        public static bool freezePlayer = false;
        public static bool inKeypadPuzzle = false;
        public static bool inQuest = false;
        public static KeypadPuzzleTriggerSO currKeypadPuzzle;


        [Header("Quest Data")]
        public static bool startingQuestActions = false;
        public static Dictionary<int, KeyQuestManager> ongoingQuests = new Dictionary<int, KeyQuestManager>();
        public static Dictionary<int, KeyQuestManager> completedQuests = new Dictionary<int, KeyQuestManager>();

        [Header("Camera Que Controls")]
        public static bool moveCamFromPlayer = false;



        // will run when the class is accessed for the first time
        static GameData()
        {
            currWeapon = 0;
            populatePuzzleCodes();

            
        }

        public static void CheckPlayerHealth(float damage)
        {
            //playerHealth is max 60f => separated over 3 hearts [60, 40, 20]
            // each heart holds 20f health
            /// regular damage: 0.25f (*10 = 2.5f)
            /// light damage: 0.125f (*10 = 1.25f)
            /// heavy damage: 0.5f (*10 = 5f)
            /// boss damage: 0.75f (*10 = 7.5f)

            
            playerHeath -= (damage * 10f);

            if (playerHeath > 40f)
            {
                onHeart = 1;
            }
            else if (playerHeath > 20f)
            {
                onHeart = 2;
            }
            else if (playerHeath <= 0f)
            {
                onHeart = 3;
            }

            
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
            //else
            //{
            //    Debug.LogWarning("key already exists in keycodesdictionary");
            //}
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

        public static void AddQuestToOngoing(int questID, KeyQuestManager quest)
        {
            if (!ongoingQuests.ContainsKey(questID))
            {
                ongoingQuests.Add(questID, quest);
            }
        }

        public static void RemoveQuestFromOngoing(int questID)
        {
            if (ongoingQuests.ContainsKey(questID))
            {
                ongoingQuests.Remove(questID);
            }
        }

        public static void AddQuestToCompleted(int questID, KeyQuestManager quest)
        {
            if (CheckQuestCompleted(questID))
            {
                completedQuests.Add(questID, quest);
            }
        }

        public static bool CheckQuestOngoing(int questID)
        {
            // function returns true if quest is currently ongoing
            if (ongoingQuests.ContainsKey(questID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckQuestCompleted(int questID)
        {
            // function returns true if quest has been completed already
            if (completedQuests.ContainsKey(questID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
