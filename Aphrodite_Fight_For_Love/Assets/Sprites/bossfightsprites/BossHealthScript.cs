using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AphroditeFightCode
{
    public class BossHealthScript : MonoBehaviour
    {
        public Image hb;
        public float bossHealth = 100f;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void BossTakeDamage(float damage)
        {
            bossHealth -= damage;
            hb.fillAmount = bossHealth / 100f;
        }


    }
}
