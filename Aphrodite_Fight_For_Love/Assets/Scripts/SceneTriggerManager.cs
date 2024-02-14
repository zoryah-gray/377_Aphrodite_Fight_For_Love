using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AphroditeFightCode
{
    public class SceneTriggerManager : MonoBehaviour
    {
        public bool enterBoss = true;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                if (enterBoss)
                {
                    Invoke("EnterBossScene", 2f);
                }
            }
        }

        public void EnterScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void EnterBossScene()
        {
            if (GameData.onLevel == 1)
            {
                EnterScene("HestiaBossFight");
            }
        }

        //public void EnterNextLevel()
        //{
        //    if (GameData.onLevel == 1)
        //    {
        //        //goto level 2
        //    }
        //}
    }
}
