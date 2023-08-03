using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Utility
{
    public class LookForward : MonoBehaviour
    {
        public LayerMask layerToCheck;
        public float radius = .3f;
        public Transform checkPoint;

        public bool canCollide ;

        private void Update()
        {
            Flip();
        }

        public void Flip()
        {
            // is Touching Ground 
            if(Physics2D.OverlapCircle(checkPoint.position , radius,layerToCheck) == canCollide )
            {
                transform.localScale = new Vector2(transform.localScale.x==1?-1: 1, 1);
            }
        }



        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(checkPoint.position, radius);
        }
    }
}
