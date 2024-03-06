using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine;
//using LeanTween;

namespace AphroditeFightCode
{
    public class KeyPadContoller : MonoBehaviour
    {

        [Header("Current Puzzle")]
        public KeypadPuzzleTriggerSO currPuzzle;

        [Header("Keys Objects")]
        [SerializeField] private GameObject currentButton;
  
        public Button firstButton;
     
        private Color originalBtnColor = Color.white;
        private Color flashColor = new Color(1f, 0.97f, 0.6f, 1f); //#FFF899
        
        public List<Button> keypadButtons;
        
        [Header("Code Tracker")]
        public int codeID = 0;
        bool btnInteractable = false;
        //[SerializeField] private int[] code;
        [SerializeField] private List<int> code;

        public List<int> currentInput;

        [Header("Puzzle Settings")]
        public float puzzleStartDelay = 5f;
        public float flashDuration = 0.25f;
        public float delayBtFlashes = 0.42f;

        [Header("Puzzle Unlocks")]
        public GameObject unlocksObj;
        [SerializeField] private List<int> unlocksDoorIDs = new List<int>();

        [Header("Dialouge")]
        [SerializeField] private GameObject dialogueManager;
        public List<string> dialogueList = new List<string>();
        public List<string> speakerList = new List<string>();
        public List<Sprite> speakerSprites = new List<Sprite>();
        public string speaker;
        public Sprite speakerSprite;

        [Header("Audio Files")]
        public AudioClip correctClip;
        public AudioClip incorrectClip;
        public AudioClip clickClip;


        private void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(null);
            unlocksObj = null;
            GameData.freezePlayer = true;
            StartCoroutine(LoadPuzzle());
            //Initalize();
        }

        private void OnDisable()
        {

            StopAllCoroutines();
        
        }

        public void OnReturn()
        {


            currentInput.Clear();
            
            GameData.inKeypadPuzzle = false;
            StopAllCoroutines();
            GUITextManager.instance.SetActive(false);
            if (currPuzzle.unlocked)
            {
                if (unlocksDoorIDs.Count == 0)
                {
                    GameEvents.current.OpenDoorTrigger(1);
                }
                else
                {
                    foreach (int id in unlocksDoorIDs)
                    {
                        Debug.Log("opening " + id);
                        GameEvents.current.OpenDoorTrigger(id);
                    }
                }
            }
            GameData.freezePlayer = false;
            gameObject.SetActive(false);
        }



        public void AddKeyPress(int keyID)
        {
            //Debug.Log("Adding btn " + keyID);
            if (btnInteractable)
            {
                currentInput.Add(keyID);
            }
        }

  
     
        public void SubmitPattern()
        {
            int flashCount = 3;
            Color flashColorCorrect = new Color(0f, 1f, 0.0157f, 1f); // #00FF04 => green
            Color flashColorIncorrect = new Color(1f, 0.3f, 0.23f, 1f); // #FF4C3B => red
            
            // evaluate if the pattern and code are the same
            if (CheckCorrect())
            {
                //correct answer
                //Debug.Log("Code is the same/correct!");
                currPuzzle.unlocked = true;
                FlashAllButtons(flashCount, flashColorCorrect);
                Invoke("OnReturn", 2f);
            }
            else { 
                //incorrect answer
                //Debug.Log("Code is not the same! ");
                // all buttons flash red
                ToggleButtonClicks(false);
                FlashAllButtons(flashCount, flashColorIncorrect);
                Invoke("FlashPuzzleAnswer", 1.5f);
            }

            currentInput.Clear();

            
        }

        private bool CheckCorrect()
        {
            for (int i = 0; i < currentInput.Count; i++)
            {
                if (i >= code.Count)
                {
                    AudioSource.PlayClipAtPoint(incorrectClip, transform.localPosition);
                    return false;
                    

                }
                //Debug.Log("Checking input (" + currentInput[i] + ") against actual (" + code[i]);
                if (currentInput[i] != code[i])
                {
                    AudioSource.PlayClipAtPoint(incorrectClip, transform.localPosition);
                    return false;
                }
            }

            currPuzzle.unlocked = true;
            AudioSource.PlayClipAtPoint(correctClip, transform.localPosition);
            return true;
        }

        private void FlashPuzzleAnswer()
        
        {
            GameData.freezePlayer = true;
            ToggleButtonClicks(false);

            foreach (var i in code)
            {
                //Debug.Log(i + " | " + keypadButtons[i - 1].gameObject.name);
                Image btnImg = keypadButtons[i - 1].GetComponent<Image>();
                Vector3 originalScale = btnImg.gameObject.transform.localScale;
                LeanTween.scale(btnImg.gameObject, originalScale * 1.15f, 0.5f)
                    .setEase(LeanTweenType.easeInOutQuad)
                    .setLoopPingPong(1)
                    .setDelay(i * (flashDuration + delayBtFlashes))
                    .setOnComplete(() =>
                    {
                        LeanTween.scale(btnImg.gameObject, originalScale, 0.5f)
                        .setEase(LeanTweenType.easeInOutQuad);
                    });
                LeanTween.value(btnImg.gameObject, 1f, 0.35f, flashDuration)
                    .setEase(LeanTweenType.easeInOutQuad)
                    .setLoopPingPong(1)
                    .setDelay(i * (flashDuration + delayBtFlashes))
                    .setOnUpdate((float alpha) =>
                    {
                        // Update button color alpha
                        Color currentColor = btnImg.color;
                        currentColor.a = alpha;
                        btnImg.color = currentColor;
                    })
                    .setOnComplete(() =>
                    {
                        if (i == code[code.Count - 1])
                        {
                            ToggleButtonClicks(true);
                        }

                    });
            }

        }
        public void PlayClickSound()
        {
            AudioSource.PlayClipAtPoint(clickClip, transform.localPosition);
        }
        private void FlashAllButtons(int flashCount, Color btnFlashColor)
        {
            ToggleButtonClicks(false);
            for (int i = 0; i < flashCount; i++)
            {
                foreach (Button button in keypadButtons)
                {
                    Image buttonImage = button.GetComponent<Image>();

                    // Flash animation using LeanTween
                    LeanTween.value(buttonImage.gameObject, originalBtnColor, btnFlashColor, flashDuration)
                        .setEase(LeanTweenType.easeInOutQuad)
                        .setOnUpdate((Color color) =>
                        {
                                // Update button color during the flash
                                buttonImage.color = color;
                        })
                        .setOnComplete(() =>
                        {
                                // Return to the original color after the flash
                                LeanTween.value(buttonImage.gameObject, btnFlashColor, originalBtnColor, flashDuration)
                                        .setEase(LeanTweenType.easeInOutQuad)
                                        .setOnUpdate((Color color) =>
                                {
                                        // Update button color
                                        buttonImage.color = color;
                                });
                            if (i == code[code.Count - 1])
                            {
                                ToggleButtonClicks(true);
                            }


                        })
                        .setDelay(i * (flashDuration + delayBtFlashes));
                }
            }

            //ToggleButtonClicks(true);
        }

        private void ToggleButtonClicks(bool canPress)
        {
            foreach (Button btn in keypadButtons)
            {
                btn.interactable = canPress;
                btnInteractable = canPress;
                //Debug.Log("Button " + btn.name + " is interactable = " + btn.interactable);
            }
        }

        private void Initalize()
        {
            if (keypadButtons.Count == 0)
            {
                foreach (Transform child in transform)
                {
                    if (child.TryGetComponent<Button>(out Button btn))
                    {
                        //Debug.Log(child.gameObject.name + " | " + btn.gameObject.name);
                        keypadButtons.Add(btn);
                        btn.interactable = false;
                    }
                }
            }


            // assign all the variables from the curr Scriptable Obj to their respective variables
            //code = currPuzzle.code;
            currPuzzle = GameData.currKeypadPuzzle;
            
            code = currPuzzle.code;
            //if (unlocksDoorIDs.Count > 0)
            //{
            //    unlocksDoorIDs.Clear();
            //}
            unlocksDoorIDs = currPuzzle.doorIDs;
            codeID = currPuzzle.codeID;


            switch (currPuzzle.difficulty)
            {
                case KeypadPuzzleTriggerSO.puzzleDifficulty.easy:
                    flashDuration = 0.30f;
                    delayBtFlashes = 0.42f;
                    break;
                case KeypadPuzzleTriggerSO.puzzleDifficulty.medium:
                    flashDuration = 0.25f;
                    delayBtFlashes = 0.32f;
                    break;
                case KeypadPuzzleTriggerSO.puzzleDifficulty.hard:
                    flashDuration = 0.15f;
                    delayBtFlashes = 0.22f;
                    break;
            }

            AddKeyCodeToDictionary();
            //FlashPuzzleAnswer();
            RunInstructions();
            //Invoke("FlashPuzzleAnswer", puzzleStartDelay);
            
        }

        public void RunInstructions()
        {

            if (currPuzzle.unlocked)
            {
                List<string> finishedDialouge = new List<string> {"You have already solved this puzzle"};
                List<string> finishedSpeakers = new List<string> { speaker };
                List<Sprite> spriteSpeaker = new List<Sprite> { speakerSprite }; 
                StartDialouge(finishedDialouge, finishedSpeakers, spriteSpeaker);
            }
            else {

                StartDialouge(dialogueList, speakerList, speakerSprites);
            }
            StartCoroutine(StartPuzzle());
            //string instr = "A pattern will flash, repeat it, and submit it to prove your wits. If you can't even do this how will you keep your composure in front of Hestia!";
            //string instr2 = "Click to Press Buttons";
            //GUITextManager.instance.PrintToGUI(instr, instr2,currPuzzle.triggerSprite);

            //StartCoroutine(DeactivateGUI());
        }

        public void StartDialouge(List<string> dialouge, List<string> speakers, List<Sprite> sprites)
        {
            //List<Sprite> speakers = new List<Sprite>();
            //speakers.Add(quest.questSprite);
            dialogueManager.SetActive(true);
            dialogueManager.GetComponent<DialogueManager>().ReceiveStartReadyDialogue(dialouge.ToArray(), speakers.ToArray(), sprites);
        }

        IEnumerator StartPuzzle()
        {
            while (dialogueManager != null && dialogueManager.activeSelf)
            {
                yield return null;
            }
            if (!currPuzzle.unlocked)
            {
                FlashPuzzleAnswer();
            }
            else
            {
                OnReturn();
            }
        }

        IEnumerator DeactivateGUI()
        {
            yield return new WaitForSeconds(5f);
            GUITextManager.instance.SetActive(false);
            FlashPuzzleAnswer();
        }

        IEnumerator LoadPuzzle()
        {
            while (GameData.currKeypadPuzzle == null)
            {
                Debug.Log("waiting on puzzle to load through GameData (" + GameData.currKeypadPuzzle + ")");
                yield return null;
            }
            //once done run the puzzle
            Initalize();
        }

        private void AddKeyCodeToDictionary()
        {
            
            GameData.AddKeyCodeToDictionary(codeID, code);
        }


        private void populateKeyCode()
        {
            Debug.Log("populating codes for " + codeID);

            code = GameData.GetPuzzleCodeList(codeID);

            //Debug.Log(code.Count);
        }

        IEnumerator WaitForKeyCodesDictionary()
        {
            while (GameData.keypadPuzzleCodes.Count == 0)
            {
                Debug.Log("still waiting " + GameData.keypadPuzzleCodes.Count);
                yield return null;
            }
            populateKeyCode();
        }


    }
}
