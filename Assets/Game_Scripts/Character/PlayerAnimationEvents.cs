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
        public void PlayerEnterDoor() => playerController.MakeInvisiable();
    }
}
