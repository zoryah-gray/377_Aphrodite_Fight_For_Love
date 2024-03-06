using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace AphroditeFightCode
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera CinemachineVirtualCamera;
        //[SerializeField]private float shakeIntensity = 2f;
        private float shakeDuration = 0.4f;

        private float timer;
        private CinemachineBasicMultiChannelPerlin _cineperlin;

        void Awake()
        {
            CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        void Start()
        {
            StopShake();
        
        }

        public void ShakeCamera(float shakeIntensity)
        {
            CinemachineBasicMultiChannelPerlin _cineperlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            _cineperlin.m_AmplitudeGain = shakeIntensity;
            timer = shakeDuration;
        }

        public void StopShake()
        {
            CinemachineBasicMultiChannelPerlin _cineperlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            _cineperlin.m_AmplitudeGain = 0f;
            timer = 0f;
        }

        void Update()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    StopShake();
                }
            }
        }
    }
}
