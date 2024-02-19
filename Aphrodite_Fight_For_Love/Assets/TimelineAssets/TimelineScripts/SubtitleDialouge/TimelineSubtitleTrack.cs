using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using TMPro;

namespace AphroditeFightCode
{
    [TrackBindingType(typeof(TextMeshProUGUI))]
    [TrackClipType(typeof(TimelineSubtitleClip))]
    public class TimelineSubtitleTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<TimelineSubtitleTrackMixer>.Create(graph, inputCount);
        }
    }
}
