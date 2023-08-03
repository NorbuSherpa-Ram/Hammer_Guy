using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Character
{

    public class Destructable : HItable 
    {
        public float health;
        private float currentHealth;
        bool invincible;

       // [Tooltip ("If health reach 0 or less ")]
        public UnityEvent  OnHealthZero;

        public override void Start()
        {
            base.Start();
            currentHealth = health;
            invincible = false;
        }
        public override void OnAttackHit(float _damage, Vector2 _playerPos )
        {
            if (currentHealth <= 0|| invincible ) return;
            DealDamage(_damage);
            base.OnAttackHit(_damage, _playerPos );
        }

        private void DealDamage(float _damage)
        {
            currentHealth -= _damage;
            if (currentHealth <= 0)
            {
                OnHealthZero?.Invoke();
            }
        }
        public void Revive()
        {
            currentHealth = health;
        }
    }

}

