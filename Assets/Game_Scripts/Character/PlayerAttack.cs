using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using Core.Combat;
namespace Core.Character

{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField]
        private float damage = 1;

        public float attackRange = .5f;
        public Transform AttackPoint;
        public LayerMask layerToCheck;

        public void DealDamage()
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll (AttackPoint.position, attackRange, layerToCheck);
            foreach(Collider2D hitobj in hit)
            {
                hitobj.GetComponent<Destructable>().OnAttackHit(damage, transform.position );
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
        }
    }

}
