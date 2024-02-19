using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

namespace AphroditeFightCode
{
    public class TimelineSubtitleClip : PlayableAsset
    {
        public string dialougeText;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<TimelineSubtitleBehaviour>.Create(graph);

            TimelineSubtitleBehaviour dialougeBehaviour = playable.GetBehaviour();
            dialougeBehaviour.dialougeText = dialougeText;

            return playable;
        }
    }
}
