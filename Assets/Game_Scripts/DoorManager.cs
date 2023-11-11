using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Core.Character;
namespace Core.Utility
{
    public class DoorManager : MonoBehaviour
    {
        public string destinationDoorName;
        [SerializeField] Door_Portle [] doorPortal;
        public PlayerController player { get; private set; }
        private void Start()
        {
            doorPortal = FindObjectsOfType<Door_Portle>();
            player = FindObjectOfType<PlayerController>();
        }
        public void SetDestinationDoor(string _destinationDoor)
        {
            this.destinationDoorName = _destinationDoor;
        }
        public Door_Portle  CheckDestinationDoor()
        {
            foreach (Door_Portle d in doorPortal)
            {
                if(d.myName == destinationDoorName)
                {
                    return d;
                }
            }
            return null;
        }
        public float GetPlayerAnimationLength()
        {
            return player.playerAnim.GetCurrentAnimatorStateInfo(0).length;
        }
    }
}