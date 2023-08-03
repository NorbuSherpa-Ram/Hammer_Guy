using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;
namespace Core.Utility
{
    public class Door : MonoBehaviour, IInteractable
    {
        public GameObject Indecator;

        [SerializeField]
        private Transform doorDestination;


        //Trigegr by Feedback Unity  Event //Sub PlayerController
        public static event Action OnDoorEnter;
        public static event Action OnDoorExit;

        public Animator myAnimator;
        //Where this Door leade 
           //Getting data by Player Interaction// to teleport play to destination 
        public Transform  GetDestinationDoor()
        {
            return doorDestination;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Indecator.SetActive(true);
            }
        }    
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Indecator.SetActive(false);
            }
        }

        //Player Enter Door 
        public void Interact()
        {
            myAnimator.SetTrigger("Open");
            OnDoorEnter?.Invoke();
        }

        // Player Controller Teleport() used to play Exiting Animation To Trigger it  
        public void ExitDoor()
        {
            myAnimator.SetTrigger("Open");
           // DOVirtual.DelayedCall(.4f, () => { OnDoorExit?.Invoke(); });  //Finish Door Opening animation 
            DOVirtual.DelayedCall(myAnimator.GetCurrentAnimatorClipInfo(0).Length -.5f , () => { OnDoorExit?.Invoke(); });  //Finish Door Opening animation 
        }
        //Fire event After Playing animation //Through Feedback //sub-by PlayerController //Play Door In Animation 
    }
}
