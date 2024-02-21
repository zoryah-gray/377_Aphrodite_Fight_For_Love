using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class ColliderPickUp : MonoBehaviour
    {
        [SerializeField] private QuestKeyController ctlr;
        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.name == "Player")
            {
                Debug.Log("PickUp Key");
                ctlr.key.AddToQuest(ctlr.key.quest);
                ctlr.DestroyKey();

            }
        }
    }
}
