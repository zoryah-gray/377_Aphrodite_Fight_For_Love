using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class CompositeInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<GameObject> interactableObjects;

        public void Interact()
        {
            foreach (var interactableObj in interactableObjects)
            {
                var interactable = interactableObj.GetComponent<IInteractable>();
                if (interactable == null) continue;
                interactable.Interact();
            }
        }
    }
}
