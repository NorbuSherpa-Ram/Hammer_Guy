using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core.Character;

namespace Core.Utility
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        [SerializeField] PlayerAttack playerAttack;
        [SerializeField] PlayerController  playerController ;


        public void OnAttackEvent()
        {
            playerAttack.DealDamage();
        }
        public void OnDoorEnter()
        {
            playerController.Teleport();
        }
        public void OnEnterDoor()
        {
            playerController.TurnSpriteRenderer(false);
        }
    }
}
