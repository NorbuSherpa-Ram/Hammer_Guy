using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Character;

namespace Core.Hazard
{
    public class Hazzard : MonoBehaviour
    {
        [SerializeField]
        private float damage = 1;
        private void OnCollisionEnter2D(Collision2D other)
        {
            GameObject  hitObject = other.gameObject;
            if (hitObject.CompareTag("Player"))
            {
                PlayerController playerController;
                float damageValue = hitObject.transform.position.x - transform.position.x < 0 ? -damage : damage; 

                hitObject.TryGetComponent<PlayerController> (out playerController );
                if (playerController != null) { playerController.ApplyDamage(damageValue); }
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            GameObject hitObject = other.gameObject;
            if (hitObject.CompareTag("Player"))
            {
                PlayerController playerController;
                float damageValue = hitObject.transform.position.x - transform.position.x < 0 ? -damage : damage;

                hitObject.TryGetComponent<PlayerController>(out playerController);
                if (playerController != null) { playerController.ApplyDamage(damageValue); }
            }
        }

    }
}
