using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Character;
namespace Core.Combact.Enemy
{
    public class PigController : MonoBehaviour
    {
        [SerializeField ]
        private Rigidbody2D myRb;
        public Animator myAnim;
        public Collider2D  myCollider;

        [SerializeField]
        private float moveSpeed ;

        void FixedUpdate()
        {
            myRb.velocity = new Vector2(moveSpeed*transform.localScale.x, myRb.velocity.y);
        }
        public void Die()
        {
            myAnim.SetTrigger("Dead");
            myCollider.enabled  = false;
            myRb.bodyType = RigidbodyType2D.Static  ;
        }
    }
}
