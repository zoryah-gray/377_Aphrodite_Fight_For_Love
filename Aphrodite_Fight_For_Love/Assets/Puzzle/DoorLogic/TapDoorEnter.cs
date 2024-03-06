using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class TrapDoorEnter : MonoBehaviour
    {
        public TrapDoor scrpt;
        public bool isEnter = true;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isEnter)
            {
                if (!scrpt.inArea && !scrpt.unlockedConditionFulfilled && collision.gameObject.name == "Player")
                {
                    Debug.Log("--> entering from OUTSIDE");
                    GameEvents.current.TrapDoorTriggerEnter(scrpt.id);
                }
                else if (scrpt.inArea && scrpt.unlockedConditionFulfilled && collision.gameObject.name == "Player")
                {
                    Debug.Log("--> entering from INSIDE");
                    GameEvents.current.TrapDoorTriggerEnter(scrpt.id);
                    scrpt.inArea = false;
                }
                
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (isEnter)
            {
                if (collision.gameObject.name == "Player" && !scrpt.unlockedConditionFulfilled)
                {
                    Debug.Log("--> CLOSE DOOR");
                    GameEvents.current.TrapDoorTriggerExit(scrpt.id);
                    //scrpt.inArea = true;
                }
            }
            else
            {
                scrpt.inArea = true;
            }

        }


    }
}
