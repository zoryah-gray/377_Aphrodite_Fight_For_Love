using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AphroditeFightCode
{
    public class TimelineSubtitleManager : MonoBehaviour
    {
        public void StartDialouge(string currentText, TextMeshProUGUI dialouge)
        {
            StartCoroutine(DisplayLine(currentText, dialouge));
        }

        private IEnumerator DisplayLine(string line, TextMeshProUGUI dialouge)
        {
            dialouge.text = "";
            foreach (char letter in line.ToCharArray())
            {
                dialouge.text += letter;
                yield return new WaitForSeconds(0.04f);
            }
        }
    }
}
