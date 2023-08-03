using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Core.Character
{
    public class HItable : MonoBehaviour
    {

        public enum HitType
        {
            None,
            Inflate,
            Push,
            Color
        }
        public HitType hitType = HitType.None;

        public ParticleSystem CustomHitEffect;
        public bool disableCustomEffect = false;

        public SpriteRenderer mySpriteRenderer;
        private Material defaultMaterial;
        public Material hitMaterial;


        private float baseScale;

        public AudioClip hitClip;

        public virtual void Start()
        {
            defaultMaterial = mySpriteRenderer.material;
            baseScale = transform.localScale.y;
        }
        public virtual void OnAttackHit(float _damage, Vector2  _playerPos)
        {
            if (hitType == HitType.Inflate)
            {
                float ran = Random.Range(.5f, baseScale);
                transform.DOScaleY(ran, 0.15f);
                DOVirtual.DelayedCall(0.15f, () =>
                {
                    transform.DOScaleY(baseScale, 0.15f);
                });
                //Play with Scale 
            }
            else if (hitType == HitType.Push)
            {
                int direction = transform.position.x - _playerPos.x < 0 ? -1 : 1;
                float ran = Random.Range(.5f, 1f)*direction ;
                transform.DOMoveX(transform.position.x + ran, 0.2f);
                // KnockBack
            }
            else if (hitType == HitType.Color)
            {
                //Changre Material 
                mySpriteRenderer.material = hitMaterial;
                StartCoroutine(ResetMaterial(0.2f));
                //ResetMateials 
            }  

            if (!disableCustomEffect)
            {
                if(CustomHitEffect!=null)
                {
                // player Effects 
                }
            }
            if(hitClip !=null)
            {
                //play sound 
            }
        }

        IEnumerator ResetMaterial(float _delay )
        {
            yield return new WaitForSeconds(_delay);
            mySpriteRenderer.material = defaultMaterial;
        }
    }
}
