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
                //if (enterBoss)
                //{
                //    Invoke("EnterBossScene", 0.75f);
                //}
                //if (SceneManager.GetActiveScene().name == "level2")
                //{
                //    Invoke("EnterNextLevel", 0.75f);
                //}
                Debug.Log("collided in " + SceneManager.GetActiveScene().name + "scene");
                if (SceneManager.GetActiveScene().name == "Level1")
                {
                    GameData.playerHeath = 60f;
                    SceneManager.LoadScene("HestiaBossFight");
                    SceneManager.UnloadSceneAsync("Level1");
                }
                else if (SceneManager.GetActiveScene().name == "HestiaBossFight")
                {
                    GameData.playerHeath = 60f;
                    SceneManager.LoadScene("level2");
                }
                else if (SceneManager.GetActiveScene().name == "level2")
                {
                    GameData.playerHeath = 60f;
                    SceneManager.LoadScene("HadesBossCombined");
                }
                else if (SceneManager.GetActiveScene().name == "HadesBossCombined")
                {
                    SceneManager.LoadScene("EndCredits");
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
            if (GameData.onLevel == 2)
            {
                EnterScene("HadesBossTiles");
            }
        }

        public void EnterNextLevel()
        {
            if (GameData.onLevel == 1)
            {
                EnterScene("level2");
            }
        }
    }
}
