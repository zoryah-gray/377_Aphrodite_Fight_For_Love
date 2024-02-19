using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

namespace AphroditeFightCode
{
    public class TimelineSubtitleTrackMixer : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            TextMeshProUGUI text = playerData as TextMeshProUGUI;
            string currentText = "";
            float currentAlpha = 0f;
            if (!text) return;
            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);

                // current one we're working on
                if (inputWeight > 0f)
                {
                    ScriptPlayable<TimelineSubtitleBehaviour> inputPlayable = (ScriptPlayable<TimelineSubtitleBehaviour>)playable.GetInput(i);
                    TimelineSubtitleBehaviour input = inputPlayable.GetBehaviour();
                    currentText = input.dialougeText;
                    currentAlpha = inputWeight;
                }
            }

            // start coroutine
            text.text = currentText;
            //StartCoroutine(DisplayLine(currentText));
            text.color = new Color(1, 1, 1, currentAlpha);
            
        }

        
    }
}
