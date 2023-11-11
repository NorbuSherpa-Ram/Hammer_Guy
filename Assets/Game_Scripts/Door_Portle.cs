using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;
namespace Core.Utility
{

    public class Door_Portle : MonoBehaviour, IInteractable
    {
        private DoorManager doorManager;

        [SerializeField] private string destinationPortleName;
        [SerializeField] public string myName;

        [SerializeField] private Animator myAnimator;
        private Door_Portle currentDestinationDoor;

        public static event Action OnDoorEnter;
        public static event Action OnDoorExit;

  

        private void Start()
        {
            doorManager = FindObjectOfType<DoorManager>();
        }

        public void Interact()
        {
            doorManager.SetDestinationDoor(destinationPortleName);
            OpenAnimation();
            Invoke(nameof(EnterDoor), myAnimator.GetCurrentAnimatorStateInfo(0).length); //wait for door to open // then player enter
        }

        private void EnterDoor()
        {
            OnDoorEnter?.Invoke(); // Start Player movemnet animation//Play player enter animation
            Invoke(nameof(CloseDoor), doorManager.GetPlayerAnimationLength()+.5f);// Wait for player to animation to finish before closing door 
        }

        private void CloseDoor()
        {
            CloseAnimation();
            StartCoroutine(SetPlayerPosition()); // telepor player once it closed 
        }

        IEnumerator SetPlayerPosition( )
        {
            currentDestinationDoor = doorManager.CheckDestinationDoor();
            float waitForTime = .5f; //Wait for second after close 
            yield return new WaitForSeconds(waitForTime);
            doorManager.player.transform.position = currentDestinationDoor.transform.position;
            currentDestinationDoor.OpenAnimation();
            Invoke(nameof(ExitDoor), myAnimator.GetCurrentAnimatorClipInfo(0).Length + .5f);//Wait to open Door before player start exiting 
           
        }
        void ExitDoor()
        {
            OnDoorExit?.Invoke();
            //    currentDestinationDoor.CloseAnimation();
            Invoke(nameof(CloseDestinationDoor), doorManager.GetPlayerAnimationLength() + .5f);//Wait for player to exit 
        }
        public void CloseDestinationDoor() => currentDestinationDoor.CloseAnimation();
        public void OpenAnimation() => myAnimator.SetTrigger("Open");
        public void CloseAnimation() => myAnimator.SetTrigger("Close");
    }
}