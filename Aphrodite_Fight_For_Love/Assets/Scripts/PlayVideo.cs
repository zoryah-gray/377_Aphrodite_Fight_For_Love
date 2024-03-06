using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

namespace AphroditeFightCode
{
    public class PlayVideo : MonoBehaviour
    {
        public RawImage rawImage;
        public VideoPlayer videoPlayer;
        public GameObject videoGO;
        public AudioSource audioSource;
        public AudioClip backgroundAudio;
        public GameObject btn;
        public GameObject btnQ;
        public GameObject titleScren;
        public GameObject title;


        void Start()
        {
            // Play the video when the scene loads
            PlayVideoClip();
        }

        void PlayVideoClip()
        {
            // Set the video to be displayed on the RawImage
            videoPlayer.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            rawImage.texture = videoPlayer.targetTexture;
            audioSource.clip = backgroundAudio;

            // Play the video
            videoPlayer.Play();
            
            audioSource.Play();

            // Set up a callback to load the next scene when the video finishes
            videoPlayer.loopPointReached += OnVideoFinished;
        }

        void OnVideoFinished(VideoPlayer vp)
        {
            // Load the next scene when the video finishes
            videoGO.SetActive(false);
            btn.SetActive(true);
            btnQ.SetActive(true);
            title.SetActive(true);
            titleScren.SetActive(true);
        }

        public void PlayAgain()
        {
            GameData.playerHeath = 60f;
            SceneManager.LoadScene("TitleScreen");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
