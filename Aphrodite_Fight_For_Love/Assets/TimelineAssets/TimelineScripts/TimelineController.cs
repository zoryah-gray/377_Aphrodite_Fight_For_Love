using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace AphroditeFightCode
{
    public class TimelineController : MonoBehaviour
    {
        public List<PlayableDirector> playableDirectors;
        public Dictionary<int, PlayableDirector> playableDirectorsLog = new Dictionary<int, PlayableDirector>();

        public List<TimelineAsset> timelines;
        public List<TimelineState> timelineStates;

        public List<string> sceneNames;

        public enum TimelineState
        {
            TimelineStartScreen,
            TimelineCutscene1,
            TimelineCutsene2,
        }

        [SerializeField] private double loopStartTime;
        private PlayerInputs input = null;

        private void Awake()
        {
            input = new PlayerInputs();
            input.Timeline.Enable();
        }

        private void OnEnable()
        {
            input.Timeline.Enable();
            input.Timeline.Skip.performed += OnSkipPerformed;
            input.Timeline.Start.performed += OnStartPerformed;

        }
        private void OnDisable()
        {
            input.Timeline.Disable();
            input.Timeline.Skip.performed -= OnSkipPerformed;
            input.Timeline.Start.performed -= OnStartPerformed;
        }

        private void Start()
        {
            PlayFromTimeline(0);
        }

        private void OnSkipPerformed(InputAction.CallbackContext val)
        {
            Debug.Log("Skip Perfomred -- skipping current cut scene sequence");
        }

        private void OnStartPerformed(InputAction.CallbackContext val)
        {
            Debug.Log("Starting Game From Start Menu");
            EndLoop(0);
            //SceneManager.LoadScene("Level1");
            PlayFromTimeline(1);
        }

        public void LoadScene(int idx)
        {
            string sceneName = sceneNames[idx];
            SceneManager.LoadScene(sceneName);
        }



        public void Play()
        {
            foreach (var playableDirector in playableDirectors)
            {
                playableDirector.Play();
            }
        }

        public void PlayFromTimeline(int idx)
        {
            TimelineAsset selectedTimeline;

            if (timelines.Count <= idx || idx < 0)
            {
                // not an existing index set to the last timeline
                selectedTimeline = timelines[timelines.Count - 1];
            }
            else
            {
                selectedTimeline = timelines[idx];
                TimelineState timelineState = timelineStates[idx];

                switch (timelineState)
                {
                    case TimelineState.TimelineStartScreen:
                        Debug.Log("Playing Start Screen Timeline");
                        break;
                    case TimelineState.TimelineCutscene1:
                        Debug.Log("Playing Cut Scene 1  Timeline");
                        break;
                    case TimelineState.TimelineCutsene2:
                        Debug.Log("Playing Cut Scene 2 Timeline");
                        break;
                }
                selectedTimeline = timelines[idx];
                


            }
            Debug.Log("playing timeline: " + selectedTimeline.name);
            //playing from only one playable director but can be changed later
            playableDirectors[idx].Play(selectedTimeline);
        }

        public void EndLoop(int playableDirIdx)
        {
            PlayableDirector selectedPlayableDirector;
            if (playableDirectors.Count <= playableDirIdx || playableDirIdx < 0)
            {
                // not an existing index set to the last playableDirector
                selectedPlayableDirector = playableDirectors[playableDirectors.Count - 1];
            }
            else
            {
                selectedPlayableDirector = playableDirectors[playableDirIdx];
            }
            selectedPlayableDirector.Stop();
            //selectedPlayableDirector.SendSignal("LoopSignal");
        }

        public void StartLoop(int playableDirIdx)
        {
            PlayableDirector selectedPlayableDirector;
            if (playableDirectors.Count <= playableDirIdx || playableDirIdx < 0)
            {
                // not an existing index set to the last playableDirector
                selectedPlayableDirector = playableDirectors[playableDirectors.Count - 1];
            }
            else
            {
                selectedPlayableDirector = playableDirectors[playableDirIdx];
            }

            Debug.Log(selectedPlayableDirector.time);
            loopStartTime = selectedPlayableDirector.time;
        }

        public void RestartLoop(int playableDirIdx)
        {
            PlayableDirector selectedPlayableDirector;
            if (playableDirectors.Count <= playableDirIdx || playableDirIdx < 0)
            {
                // not an existing index set to the last playableDirector
                selectedPlayableDirector = playableDirectors[playableDirectors.Count - 1];
            }
            else
            {
                selectedPlayableDirector = playableDirectors[playableDirIdx];
            }

            selectedPlayableDirector.time = loopStartTime;
            Debug.Log(selectedPlayableDirector.time);
        }
    }
}
