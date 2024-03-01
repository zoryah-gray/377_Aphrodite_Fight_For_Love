using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class HintGiver : MonoBehaviour
    {
        [Header("Dialogue")]
        [SerializeField] private GameObject dialogueManager;
        private List<string> dialogueList = new List<string>();
        private List<string> speakerList = new List<string>();
        private bool completedDialouge = false;
        private List<Sprite> speakers;

        void Start()
        {
        
        }

        
        void Update()
        {
        
        }

        public void StartDialouge(List<string> dialogueList, List<string> speakerList)
        {
            //List<Sprite> speakers = new List<Sprite>();
            //speakers.Add(quest.questSprite);
            dialogueManager.SetActive(true);
            dialogueManager.GetComponent<DialogueManager>().ReceiveStartReadyDialogue(dialogueList.ToArray(), speakerList.ToArray(), speakers);
        }
    }
}
