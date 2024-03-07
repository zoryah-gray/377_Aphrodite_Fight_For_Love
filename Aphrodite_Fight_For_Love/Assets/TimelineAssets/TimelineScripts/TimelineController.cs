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

        [Header("Timelines and Directors")]
        public List<PlayableDirector> playableDirectors;
        public Dictionary<int, PlayableDirector> playableDirectorsLog = new Dictionary<int, PlayableDirector>();

        public List<TimelineAsset> timelines;
        public List<TimelineState> timelineStates;

        PlayableDirector currPlayableDirector;

        public enum TimelineState
        {
            TimelineStartScreen,
            TimelineCutscene1,
            TimelineCutsene2,
        }

        
        public enum CurrentScene
        {
            TitleScreen,
            Tutorial,
            HestiaBossFight,
            HadesBossCombined
        }
        [Header("Scene Manager")]
        public List<string> sceneNames;
        public string activeScene;
        private CurrentScene currScene;
        public int sceneID;

        [Header("Signal Info")]
        [SerializeField] private double loopStartTime;
        [SerializeField] private bool inLoop = false;
        private PlayerInputs input = null;
        [SerializeField] private bool paused = false;
        [SerializeField] private bool goToNextDialouge = false;


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
            DetermineScene();
            
        }

        private void OnSkipPerformed(InputAction.CallbackContext val)
        {
            if (paused)
            {
                TogglePlay();
            }
            if (goToNextDialouge)
            {
                SkipToNextDialouge();
            }
            //Debug.Log("Skip Perfomred -- skipping current cut scene sequence");
        }

        private void OnStartPerformed(InputAction.CallbackContext val)
        {
            if (!GameData.gameStarted && inLoop)
            {
                Debug.Log("Starting Game From Start Menu");
                EndLoop(0);
                PlayFromTimeline(1);
                GameData.gameStarted = true;
                inLoop = false;
                input.Timeline.Start.performed -= OnStartPerformed;
                DetermineDirector(0);
                
            }
        }

        public void DetermineScene()
        {
            activeScene = SceneManager.GetActiveScene().name;

            if (System.Enum.TryParse<CurrentScene>(activeScene, out CurrentScene sceneType))
            {
                currScene = sceneType;
            }
            else
            {
                // Parsing failed, handle the error or provide a default value
                Debug.LogError("Failed to convert string to SceneType");
            }

            switch (currScene)
            {
                case CurrentScene.TitleScreen:
                    sceneID = 0;
                    PlayFromTimeline(0);
                    break;
                case CurrentScene.Tutorial:
                    sceneID = 2;
                    PlayFromTimeline(2);
                    break;
                case CurrentScene.HestiaBossFight:
                    sceneID = 3;
                    PlayFromTimeline(3);
                    break;
                case CurrentScene.HadesBossCombined:
                    sceneID = 4;
                    PlayFromTimeline(4);
                    break;
            }

        }

        public void SkipNextScene()
        {
            SceneManager.LoadScene("Level1");
        }
        public void LoadScene(int idx)
        {
            string sceneName = sceneNames[idx];
            SceneManager.LoadScene(sceneName);
        }

        private void DetermineDirector(int playableDirIdx)
        {
            if (playableDirectors.Count <= playableDirIdx || playableDirIdx < 0)
            {
                // not an existing index set to the last playableDirector
                currPlayableDirector = playableDirectors[playableDirectors.Count - 1];
            }
            else
            {
                currPlayableDirector = playableDirectors[playableDirIdx];
            }
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
            DetermineDirector(playableDirIdx);
            currPlayableDirector.Stop();

        }

        public void StartLoop(int playableDirIdx)
        {
            DetermineDirector(playableDirIdx);
            loopStartTime = currPlayableDirector.time;
        }

        public void inLoopSignal()
        {
            inLoop = true;
        }

        public void RestartLoop(int playableDirIdx)
        {
            DetermineDirector(playableDirIdx);
            currPlayableDirector.time = loopStartTime;
        }

        public void ContinueSignal()
        {
            // Signal Order: Continue (AFTER the fade IN of the dialouge
            ///             -> Pause (at the end of a dialouge BEFORE the fade OUT)
            ///             -> Next (at the start of the next dialouge, but BEFORE the fade IN)
            Debug.Log("(1) Continue: Can now click Space to Continue");
            goToNextDialouge = true;
            paused = false;
        }

        public void Pause(int playableDirIdx)
        {
            DetermineDirector(playableDirIdx);
            currPlayableDirector.Pause();
            Debug.Log("(2) Paused: Can now click Space to Continue");
            paused = true;

        }

        public void TogglePlay()
        {
            currPlayableDirector.Play();
            Debug.Log("Unpaused -- Playing!");
            paused = false;
        }

        public void SkipToNextDialouge()
        {
            Debug.Log("(3) Skip: Skipped to Next Dialouge");
            goToNextDialouge = false;
            paused = false;
            DetermineDirector(1);
            double currTime = currPlayableDirector.time;
            //double currentTime = currPlayableDirector.time;

            

            //TimelineAsset timelineAsset = (TimelineAsset)currPlayableDirector.playableAsset;

            //foreach (TrackAsset track in timelineAsset.GetOutputTracks())
            //{
            //    if (track is SignalEmitter signalEmitter)
            //    {
            //        foreach(TimelineClip clip in track.GetClips())
            //        {
            //            if (clip.asset is )
            //        }
            //    }
            //}

            // Iterate through the tracks to find the nearest "next" signal
            //foreach (PlayableBinding binding in currPlayableDirector.playableAsset.outputs)
            //{
            //    if (binding.sourceObject is TrackAsset track)
            //    {
            //        foreach (TimelineClip clip in track.GetClips())
            //        {

            //            if (clip.asset is SignalReceiver signalReceiver) {
            //                if (clip.asset is SignalAsset signalAsset && signalAsset.name == "Next")
            //                {
            //                    currPlayableDirector.time = clip.end;
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //}

            //foreach (PlayableBinding binding in currPlayableDirector.playableAsset.outputs)
            //{
            //    binding.
            //    if (binding.streamName == "Signals" && binding.outputType == typeof(SignalReceiver))
            //    {
            //        SignalReceiver signalReceiver = (SignalReceiver)binding.sourceObject;

            //        // Check if the "next" signal is received
            //        if (signalReceiver.GetSignal().name == "next")
            //        {
            //            // Set the director's time to the end of the "next" signal
            //            currPlayableDirector.time = currPlayableDirector.playableAsset.duration;
            //            return;
            //        }
            //    }
            //}


        }



    }
}
