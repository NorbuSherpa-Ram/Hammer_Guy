using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core.Utility;

namespace Core.Character
{
    public class PlayerInteraction : MonoBehaviour
    {

        public float interactRange =.5f;
        public LayerMask layerToCheck;
  

        IInteractable interactable ;

        public void Interact()
        {
            interactable = GetInteractable();
            if(interactable != null)
            {
                interactable.Interact();
            }
        }
        IInteractable GetInteractable()
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, interactRange, layerToCheck);
            if (col != null)
            {
                //Transform  destinationn = col.GetComponent<Door>().GetDestinationDoor();
             //   teleportTo = destinationn;
                return col.GetComponent<IInteractable>();
            }
            return null;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, interactRange);
        }
    }
}
