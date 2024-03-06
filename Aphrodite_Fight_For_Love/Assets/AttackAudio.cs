using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class AttackAudio : MonoBehaviour
    {
        public AudioClip meleeClip;
        void PlayMeleeSound()
        {
            AudioSource.PlayClipAtPoint(meleeClip, transform.localPosition);
        }
    }
}
